using ApiSpotify.MODELS; // Canvia-ho pel teu namespace real
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiSpotify.REPOSITORY
{
    public class DAOUsuari
    {
        public static void Insert(DatabaseConnection dbConn, Usuari usuari)
        {
            dbConn.Open();

            string sql = @"INSERT INTO Usuari (Id, Nom, Contrasenya, Salt)
                           VALUES (@Id, @Nom, @Contrasenya, @Salt)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", usuari.Id);
            cmd.Parameters.AddWithValue("@Nom", usuari.Nom);
            cmd.Parameters.AddWithValue("@Contrasenya", usuari.Contrasenya);
            cmd.Parameters.AddWithValue("@Salt", usuari.Salt);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila inserida.");

            dbConn.Close();
        }
    }
}