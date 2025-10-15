using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;

namespace ApiSpotify.ENDPOINTS
{
    public static class LlistesReproduccions
    {
        public static void MapLlistesReproduccionsEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {

            app.MapGet("/playlists", () =>
            {
                List<LlistaReproduccio> llistes = DAOLlistaReproduccio.GetAll(dbConn);
                return Results.Ok(llistes);
            });

            app.MapGet("/playlists/{id}", (Guid id) =>
            {
                LlistaReproduccio? llista = DAOLlistaReproduccio.GetById(dbConn, id);
                return llista is not null
                    ? Results.Ok(llista)
                    : Results.NotFound(new { message = $"Playlist amb Id {id} no trobada." });
            });

            app.MapPost("/playlists", (LlistaRequest req) =>
            {
                LlistaReproduccio llista = new LlistaReproduccio
                {
                    Id = Guid.NewGuid(),
                    IdUsuari = req.IdUser,
                    Nom = req.Nom
                };

                DAOLlistaReproduccio.Insert(dbConn, llista);
                return Results.Created($"/playlists/{llista.Id}", llista);
            });
        }
    }
}

public record LlistaRequest(Guid IdUser, string Nom);