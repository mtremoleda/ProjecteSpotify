using ApiSpotify.DTO;
using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ApiSpotify.REPOSITORY
{
    public class DAOLlistaReproduccioCanco
    {
        public static List<LlistaReproduccioCancoResponse> GetCanconsByLlistaId(
            DatabaseConnection dbConn,
            Guid llistaId)
        {
            List<LlistaReproduccioCancoResponse> cancons = new();

            dbConn.Open();

            string sql = @"
                SELECT s.Id, s.titol, s.artista, s.album, s.durada
                FROM Playlist_song ps
                INNER JOIN Songs s ON ps.id_song = s.Id
                WHERE ps.id_playlist = @llistaId";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@llistaId", llistaId);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cancons.Add(new LlistaReproduccioCancoResponse
                {
                    Id = reader.GetGuid(0),
                    Titol = reader.GetString(1),
                    Artista = reader.GetString(2),
                    Album = reader.GetString(3),
                    Durada = reader.GetDecimal(4)
                });
            }

            dbConn.Close();
            return cancons;
        }

        // Nuevo método: Añadir canción a playlist
        public static void AddSongToPlaylist(DatabaseConnection dbConn, PlaylistSong playlistSong)
        {
            dbConn.Open();

            string sql = @"
                INSERT INTO Playlist_song (Id, id_song, id_playlist)
                VALUES (@Id, @IdSong, @IdPlaylist)";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", playlistSong.Id);
            cmd.Parameters.AddWithValue("@IdSong", playlistSong.IdSong);
            cmd.Parameters.AddWithValue("@IdPlaylist", playlistSong.IdPlaylist);

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }

        // Nuevo método: Eliminar canción de playlist
        public static void RemoveSongFromPlaylist(DatabaseConnection dbConn, Guid playlistId, Guid songId)
        {
            dbConn.Open();

            string sql = @"
                DELETE FROM Playlist_song
                WHERE id_playlist = @playlistId AND id_song = @songId";

            using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
            cmd.Parameters.AddWithValue("@playlistId", playlistId);
            cmd.Parameters.AddWithValue("@songId", songId);

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
    }
}