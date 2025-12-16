using ApiSpotify.Common;
using ApiSpotify.DTO;
using ApiSpotify.MODELS;
using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using ApiSpotify.Validations;



namespace ApiSpotify.ENDPOINTS
{
    public static class Usuaris
    {
        public static void MapUsuarisEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // GET /usuaris
            app.MapGet("/usuaris", () =>
            {
                List<Usuari> usuaris = DAOUsuari.GetAll(dbConn);
                List<UsuariResponse> usuariResponses = new List<UsuariResponse>();

                foreach (Usuari u in usuaris)
                {
                    usuariResponses.Add(UsuariResponse.FromUsuari(u));
                }

                return Results.Ok(usuariResponses);
            });

            app.MapGet("/usuaris/{id}", (Guid id) =>
            {
                Usuari? usuari = DAOUsuari.GetById(dbConn, id);

                return usuari is not null
                    ? Results.Ok(UsuariResponse.FromUsuari(usuari))
                    : Results.NotFound(new { message = $"Usuari amb Id {id} no trobat." });
            });

            app.MapPost("/usuaris", (UsuariRequest req) =>
            {
                Result result = UsuariValidator.Validate(req);
                if (!result.IsOk)
                {
                    return Results.BadRequest(result);
                }

                string salt = UTILS.UtilsContrasenya.GenerateSalt();
                string hashedPassword = UTILS.UtilsContrasenya.HashPassword(req.Contrasenya, salt);

                Usuari usuari = new Usuari
                {
                    Id = Guid.NewGuid(),
                    Nom = req.Nom,
                    Contrasenya = hashedPassword,
                };

                DAOUsuari.Insert(dbConn, usuari, salt);
                Console.WriteLine(usuari.Contrasenya);
                return Results.Created($"/usuaris/{usuari.Id}", usuari);
            });

            app.MapPut("/usuaris/{id}", (Guid id, UsuariRequest req) =>
            {
                var existing = DAOUsuari.GetById(dbConn, id);
                if (existing == null)
                    return Results.NotFound(new { message = $"Usuari amb Id {id} no trobat." });

                Result result = UsuariValidator.Validate(req);
                if (!result.IsOk)
                {
                    return Results.BadRequest(new
                    {
                        error = result.ErrorCode,
                        message = result.ErrorMessage
                    });
                }

                string newSalt = UTILS.UtilsContrasenya.GenerateSalt();
                string hashedPassword = UTILS.UtilsContrasenya.HashPassword(req.Contrasenya, newSalt);

                Usuari updated = new Usuari
                {
                    Id = id,
                    Nom = req.Nom,
                    Contrasenya = hashedPassword,
                    Salt = newSalt
                };

                DAOUsuari.Update(dbConn, updated);
                return Results.Ok(UsuariResponse.FromUsuari(updated));
            });

            app.MapDelete("/usuaris/{id}", (Guid id) =>
                DAOUsuari.Delete(dbConn, id)
                    ? Results.NoContent()
                    : Results.NotFound(new { message = $"Usuari amb Id {id} no trobat." }));
        }
    }
}
    


public record UsuariRequest(string Nom, string Contrasenya);

