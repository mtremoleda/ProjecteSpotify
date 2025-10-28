using ApiSpotify.DTO;
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
                List<LlistaReproduccioResponse> responseList = new List<LlistaReproduccioResponse>();

                foreach (LlistaReproduccio llista in llistes)
                {
                    responseList.Add(LlistaReproduccioResponse.FromLlistaReproduccio(llista));
                }

                return Results.Ok(responseList);
            });

            app.MapGet("/playlists/{id}", (Guid id) =>
            {
                LlistaReproduccio? llista = DAOLlistaReproduccio.GetById(dbConn, id);

                return llista is not null
                    ? Results.Ok(LlistaReproduccioResponse.FromLlistaReproduccio(llista))
                    : Results.NotFound(new { message = $"Playlist amb Id {id} no trobada." });
            });

            app.MapPost("/playlists", (LlistaReproduccioRequest req) =>
            {
                LlistaReproduccio llista = req.ToLlistaReproduccio(Guid.NewGuid());

                DAOLlistaReproduccio.Insert(dbConn, llista);

                return Results.Created($"/playlists/{llista.Id}", LlistaReproduccioResponse.FromLlistaReproduccio(llista));
            });

            app.MapPut("/playlists/{id}", (Guid id, LlistaReproduccioRequest req) =>
            {
                var existing = DAOLlistaReproduccio.GetById(dbConn, id);
                if (existing == null)
                    return Results.NotFound(new { message = $"Playlist amb Id {id} no trobada." });

                LlistaReproduccio updated = req.ToLlistaReproduccio(id);

                DAOLlistaReproduccio.Update(dbConn, updated);

                return Results.Ok(LlistaReproduccioResponse.FromLlistaReproduccio(updated));
            });

            app.MapDelete("/playlists/{id}", (Guid id) =>
                DAOUsuari.Delete(dbConn, id)
                    ? Results.NoContent()
                    : Results.NotFound(new { message = $"Playlist amb Id {id} no trobada." }));
        }
    }
}

public record LlistaRequest(Guid IdUser, string Nom);