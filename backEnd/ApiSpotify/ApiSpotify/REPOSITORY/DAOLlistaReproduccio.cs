﻿using ApiSpotify.MODELS;
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

        public static List<LlistaReproduccio> GetAll(DatabaseConnection dbConn)
        {
            List<LlistaReproduccio> llistes = new();
            dbConn.Open();

            string sql = "SELECT Id, IdUsuari, Nom FROM LlistaReproduccio";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                llistes.Add(new LlistaReproduccio
                {
                    Id = reader.GetGuid(0),
                    IdUsuari = reader.GetGuid(1),
                    Nom = reader.GetString(2)
                });
            }

            dbConn.Close();
            return llistes;
        }

        public static LlistaReproduccio? GetById(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = "SELECT Id, IdUsuari, Nom FROM LlistaReproduccio WHERE Id = @Id FROM LlistaReproduccio";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            LlistaReproduccio? llista = null;

            if (reader.Read())
            {
                llista = new LlistaReproduccio
                {
                    Id = reader.GetGuid(0),
                    IdUsuari = reader.GetGuid(1),
                    Nom = reader.GetString(2)
                };
            }

            dbConn.Close();
            return llista;
        }

        public static void Update(DatabaseConnection dbConn, LlistaReproduccio llista)
        {
            dbConn.Open();

            string sql = @"UPDATE LlistaReproduccio
                           SET Nom = @Nom,
                               IdUsuari = @IdUsuari,
                               UsuariId = @UsuariId
                           WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", llista.Id);
            cmd.Parameters.AddWithValue("@Nom", llista.Nom);
            cmd.Parameters.AddWithValue("@IdUsuari", llista.IdUsuari);
            cmd.Parameters.AddWithValue("@Usuari", llista.Usuari);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} fila actualitzada.");

            dbConn.Close();
        }

        public static bool Delete(DatabaseConnection dbConn, Guid id)
        {
            dbConn.Open();

            string sql = @"DELETE FROM LlistaReproduccio WHERE Id = @Id";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);

            int rows = cmd.ExecuteNonQuery();

            dbConn.Close();

            return rows > 0;
        }
    }
}