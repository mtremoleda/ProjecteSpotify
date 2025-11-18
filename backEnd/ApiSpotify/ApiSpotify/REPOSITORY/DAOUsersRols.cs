using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSpotify.REPOSITORY
{
    public class DAOUsersRols
    {
        public static void Insert(DatabaseConnection dbConn, UsersRols relacio)
        {
            dbConn.Open();

            string sql = @"INSERT INTO UsersRols (Id, UserId, RolId)
                           VALUES (@Id, @UserId, @RolId)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", relacio.Id);
            cmd.Parameters.AddWithValue("@UserId", relacio.UserId);
            cmd.Parameters.AddWithValue("@RolId", relacio.RolId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");

            dbConn.Close();
        }

        public static List<UsersRols> GetAll(DatabaseConnection dbConn)
        {
            List<UsersRols> relacions = new();
            dbConn.Open();

            string sql = "SELECT Id, RolId, PermisosId FROM UsersRols";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                relacions.Add(new UsersRols
                {
                    Id = reader.GetGuid(0),
                    UserId = reader.GetGuid(1),
                    RolId = reader.GetGuid(2)
                });
            }

            dbConn.Close();
            return relacions;
        }

        public static UsersRols? GetById(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = "SELECT Id, UserId, RolId FROM UsersRols WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            UsersRols? relacio = null;

            if (reader.Read())
            {
                relacio = new UsersRols
                {
                    Id = reader.GetGuid(0),
                    UserId = reader.GetGuid(1),
                    RolId = reader.GetGuid(2)
                };
            }

            dbConn.Close();
            return relacio;
        }

        public static void Update(DatabaseConnection dbConn, UsersRols rel)
        {
            dbConn.Open();

            string sql = @"UPDATE UsersRols
                           SET UsersRols = @UsersRols,
                               UserId = @UserId,
                           WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", rel.Id);
            cmd.Parameters.AddWithValue("@UserId", rel.UserId);
            cmd.Parameters.AddWithValue("@RolId", rel.RolId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila actualitzada.");

            dbConn.Close();
        }

        public static bool Delete(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = @"DELETE FROM UsersRols WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            dbConn.Close();

            return rows > 0;
        }
    }
}
