using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiSpotify.ENDPOINTS
{
    public static class LlistareproduccioCancoEndpoints
    {
        public static void MapLlistaReproduccioCancoEndpoints(
            this WebApplication app,
            DatabaseConnection dbConn)
        {
            // GET: Obtener canciones de una playlist
            app.MapGet("/llistes-reproduccio/{id}/cancons", (Guid id) =>
            {
                var cancons =
                    DAOLlistaReproduccioCanco.GetCanconsByLlistaId(dbConn, id);

                return Results.Ok(cancons);
            });

            // POST: Añadir canción a una playlist
            app.MapPost("/llistes-reproduccio/{playlistId}/cancons", (Guid playlistId, AddSongToPlaylistRequest request) =>
            {
                try
                {
                    var playlistSong = new PlaylistSong
                    {
                        IdSong = request.canco_id,
                        IdPlaylist = playlistId
                    };

                    DAOLlistaReproduccioCanco.AddSongToPlaylist(dbConn, playlistSong);

                    return Results.Ok(new { message = "Canción añadida a la playlist exitosamente" });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // DELETE: Eliminar canción de una playlist
            app.MapDelete("/llistes-reproduccio/{playlistId}/cancons/{songId}", (Guid playlistId, Guid songId) =>
            {
                try
                {
                    DAOLlistaReproduccioCanco.RemoveSongFromPlaylist(dbConn, playlistId, songId);

                    return Results.Ok(new { message = "Canción eliminada de la playlist exitosamente" });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });
        }
    }

    // Modelo para la petición POST
    public class AddSongToPlaylistRequest
    {
        public Guid canco_id { get; set; }
    }
}