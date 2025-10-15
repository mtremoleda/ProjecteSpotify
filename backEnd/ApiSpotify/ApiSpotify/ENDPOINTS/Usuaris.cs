using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;

namespace ApiSpotify.ENDPOINTS
{
    public static class Usuaris
    {
        public static void MapUsuarisEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapGet("/usuaris", () =>
            {
                List<Usuari> usuaris = DAOUsuari.GetAll(dbConn);
                return Results.Ok(usuaris);
            });

            app.MapGet("/usuaris/{id}", (Guid id) =>
            {
                Usuari? usuari = DAOUsuari.GetById(dbConn, id);
                return usuari is not null
                    ? Results.Ok(usuari)
                    : Results.NotFound(new { message = $"Usuari amb Id {id} no trobat." });
            });

            app.MapPost("/usuaris", (UsuariRequest req) =>
            {
                Usuari usuari = new Usuari
                {
                    Id = Guid.NewGuid(),
                    Nom = req.Nom,
                    Contrasenya = req.Contrasenya,
                    Salt = req.Salt
                };

                DAOUsuari.Insert(dbConn, usuari);
                return Results.Created($"/usuaris/{usuari.Id}", usuari);
            });

        

        }
    }
}

public record UsuariRequest(string Nom, string Contrasenya, string Salt);

