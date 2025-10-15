using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiSpotify.REPOSITORY
{
    public class DAOQualitatFitxer
    {
        public static void Insert(DatabaseConnection dbConn, QualitatFitxer fitxer)
        {
            dbConn.Open();

            string sql = @"INSERT INTO QualitatFitxer (Id, IdCanco, Format, Bitrate, Mida)
                           VALUES (@Id, @IdCanco, @Format, @Bitrate, @Mida)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", fitxer.Id);
            cmd.Parameters.AddWithValue("@IdCanco", fitxer.IdCanco);
            cmd.Parameters.AddWithValue("@Format", fitxer.Format);
            cmd.Parameters.AddWithValue("@Bitrate", fitxer.Bitrate);
            cmd.Parameters.AddWithValue("@Mida", fitxer.Mida);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");

            dbConn.Close();
        }
    }
}
