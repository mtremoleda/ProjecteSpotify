using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiSpotify.REPOSITORY
{
    public class DAOLlistaReproduccioCanco
    {
        public static void Insert(DatabaseConnection dbConn, LlistaReproduccioCanco relacio)
        {
            dbConn.Open();

            string sql = @"INSERT INTO Playlist_song (Id, IdCanco, IdLlista)
                           VALUES (@Id, @IdCanco, @IdLlista)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", relacio.Id);
            cmd.Parameters.AddWithValue("@IdCanco", relacio.IdCanco);
            cmd.Parameters.AddWithValue("@IdLlista", relacio.IdLlista);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");

            dbConn.Close();
        }

        public static List<LlistaReproduccioCanco> GetAll(DatabaseConnection dbConn)
        {
            List<LlistaReproduccioCanco> relacions = new();
            dbConn.Open();

            string sql = "SELECT Id, IdCanco, IdLlista FROM Playlist_song";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                relacions.Add(new LlistaReproduccioCanco
                {
                    Id = reader.GetGuid(0),
                    IdCanco = reader.GetGuid(1),
                    IdLlista = reader.GetGuid(2)
                });
            }

            dbConn.Close();
            return relacions;
        }

        public static LlistaReproduccioCanco? GetById(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = "SELECT Id, IdCanco, IdLlista FROM Playlist_song WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            LlistaReproduccioCanco? relacio = null;

            if (reader.Read())
            {
                relacio = new LlistaReproduccioCanco
                {
                    Id = reader.GetGuid(0),
                    IdCanco = reader.GetGuid(1),
                    IdLlista = reader.GetGuid(2)
                };
            }

            dbConn.Close();
            return relacio;
        }
    }
}
