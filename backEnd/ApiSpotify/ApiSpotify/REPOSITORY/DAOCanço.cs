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

        public static List<Canco> () {


        //public static Canco GetById(DatabaseConnection dbConn, Guid id)
        //{
        //    dbConn.Open();
        //}
    }
}
