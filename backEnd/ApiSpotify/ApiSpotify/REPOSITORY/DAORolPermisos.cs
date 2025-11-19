using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiSpotify.REPOSITORY
{
    public class DAORolPermisos
    {
        public static void Insert(DatabaseConnection dbConn, RolPermisos relacio)
        {
            dbConn.Open();

            string sql = @"INSERT INTO RolPermisos (Id, RolId, PermisosId)
                           VALUES (@Id, @RolId, @PermisosId)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", relacio.Id);
            cmd.Parameters.AddWithValue("@RolId", relacio.RolId);
            cmd.Parameters.AddWithValue("@PermisosId", relacio.PermisosId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");

            dbConn.Close();
        }

        public static List<RolPermisos> GetAll(DatabaseConnection dbConn, Guid RolId)
        {
            List<RolPermisos> relacions = new();
            dbConn.Open();

            string sql = "SELECT Id, RolId, PermisosId FROM RolPermisos WHERE RolId = @RolId";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@RolId", RolId);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                relacions.Add(new RolPermisos
                {
                    Id = reader.GetGuid(0),
                    RolId = reader.GetGuid(1),
                    PermisosId = reader.GetGuid(2)
                });
            }

            dbConn.Close();
            return relacions;
        }

        public static RolPermisos? GetById(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = "SELECT Id, RolId, PermisosId FROM RolPermisos WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            RolPermisos? relacio = null;

            if (reader.Read())
            {
                relacio = new RolPermisos
                {
                    Id = reader.GetGuid(0),
                    RolId = reader.GetGuid(1),
                    PermisosId = reader.GetGuid(2)
                };
            }

            dbConn.Close();
            return relacio;
        }

        public static void Update(DatabaseConnection dbConn, RolPermisos rel)
        {
            dbConn.Open();

            string sql = @"UPDATE RolPermisos
                           SET PermisosId = @PermisosId,
                               RolId = @RolId,
                           WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", rel.Id);
            cmd.Parameters.AddWithValue("@RolId", rel.RolId);
            cmd.Parameters.AddWithValue("@PermisosId", rel.PermisosId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila actualitzada.");

            dbConn.Close();
        }

        public static bool Delete(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = @"DELETE FROM RolPermisos WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            dbConn.Close();

            return rows > 0;
        }
    }
}
