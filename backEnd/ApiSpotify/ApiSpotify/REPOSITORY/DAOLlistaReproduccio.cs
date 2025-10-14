using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiSpotify.REPOSITORY
{
    public class DAOLlistaReproduccio
    {
        public static void Insert(DatabaseConnection dbConn, LlistaReproduccio llista)
        {
            dbConn.Open();

            string sql = @"INSERT INTO LlistaReproduccio (Id, IdUsuari, Nom)
                           VALUES (@Id, @IdUsuari, @Nom)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", llista.Id);
            cmd.Parameters.AddWithValue("@IdUsuari", llista.IdUsuari);
            cmd.Parameters.AddWithValue("@Nom", llista.Nom);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");

            dbConn.Close();
        }
    }
}