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

        public static List<QualitatFitxer> GetAll(DatabaseConnection dbConn)
        {
            List<QualitatFitxer> fitxers = new();
            dbConn.Open();

            string sql = "SELECT Id, IdCanco, Format, Bitrate, Mida FROM QualitatFitxer";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                fitxers.Add(new QualitatFitxer
                {
                    Id = reader.GetGuid(0),
                    IdCanco = reader.GetGuid(1),
                    Format = reader.GetString(2),
                    Bitrate = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    Mida = reader.IsDBNull(4) ? null : reader.GetDecimal(4)
                });
            }

            dbConn.Close();
            return fitxers;
        }

        public static QualitatFitxer? GetById(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = "SELECT Id, IdCanco, Format, Bitrate, Mida FROM QualitatFitxer WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            QualitatFitxer? fitxer = null;

            if (reader.Read())
            {
                fitxer = new QualitatFitxer
                {
                    Id = reader.GetGuid(0),
                    IdCanco = reader.GetGuid(1),
                    Format = reader.GetString(2),
                    Bitrate = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    Mida = reader.IsDBNull(4) ? null : reader.GetDecimal(4)
                };
            }

            dbConn.Close();
            return fitxer;
        }

        public static void Update(DatabaseConnection dbConn, QualitatFitxer qualitat)
        {
            dbConn.Open();

            string sql = @"UPDATE QualitatFitxer
                           SET
                               Bitrate = @Bitrate,
                               Format = @Format
                           WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", qualitat.Id);
            cmd.Parameters.AddWithValue("@Bitrate", qualitat.Bitrate);
            cmd.Parameters.AddWithValue("@Format", qualitat.Format);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila actualitzada.");

            dbConn.Close();
        }

    }
}
