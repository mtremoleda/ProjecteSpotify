using ApiSpotify.Services;
using Microsoft.AspNetCore.Identity.Data;
using ApiSpotify.REPOSITORY;
using ApiSpotify.MODELS;
using Microsoft.AspNetCore.Mvc;

namespace ApiSpotify.ENDPOINTS
{
    public static class loginendpoint
    {
        public static void MaploginEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapPost("/login", ([FromBody] login req) =>
            {
                // Buscar usuari per nom
                Usuari user = DAOUsuari.GetByNom(dbConn, req.nom);

                if (user == null)
                    return Results.Unauthorized();

                // Validar contrasenya
                bool valid = PasswordHelper.VerifyPassword(
                    req.contrasenya,  // <- contrasenya enviada del WPF
                    user.Contrasenya, // <- hash de la BD
                    user.Salt         // <- salt de la BD
                );

                if (!valid)
                    return Results.Unauthorized();

                return Results.Ok(new { message = "Login correcte", userId = user.Id });
            });

        }
    }
}
