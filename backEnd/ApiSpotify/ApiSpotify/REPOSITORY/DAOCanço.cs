using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSpotify.REPOSITORY
{
    public class DAOCanço
    {
        public static void Insert(DatabaseConnection dbConn, Canco canco)
        {
            dbConn.Open();

            string sql = @"INSERT INTO Canco (Id, Titol, Artista, Album, Durada)
                          VALUES (@Id, @titol, @artista, @album, @durada)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", canco.Id);
            cmd.Parameters.AddWithValue("@Titol", canco.Titol);
            cmd.Parameters.AddWithValue("@Artista", canco.Artista);
            cmd.Parameters.AddWithValue("@Album", canco.Album);
            cmd.Parameters.AddWithValue("@Durada", canco.Durada);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");
            dbConn.Close();
        }

        public static List<Canco> GetAll(DatabaseConnection dbconn) 
        {
            List<Canco> canco = new();

            dbconn.Open();

            string sql = "SELECT Id, Titol, Artista, Album, Durada FROM Canco";

            using SqlCommand cmd = new SqlCommand(sql, dbconn.sqlConnection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                canco.Add(new Canco
                {
                    Id = reader.GetGuid(0),
                    Titol = reader.GetString(1),
                    Artista = reader.GetString(2),
                    Album = reader.GetString(3),
                    Durada = reader.GetDecimal(64),
                });
            }

            dbconn.Close();
            return canco;
        }

        public static Canco GetById(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();
            string sql = "SELECT Id, Titol, Artista, Album, Durada FROM Canco";

            using SqlCommand cmd = new SqlCommand( sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader(); 
            Canco? canco = null;

            if (reader.Read())
            {
                canco = new Canco
                {
                    Id = reader.GetGuid(0),
                    Titol = reader.GetString(1),
                    Artista = reader.GetString(2),
                    Album = reader.GetString(3),
                    Durada = reader.GetDecimal(64),
                };
            };

            dbConn.Close();
            return canco;
        }
    }
}
