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
    }
}
