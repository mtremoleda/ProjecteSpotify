using ApiSpotify.Services;
using Microsoft.AspNetCore.Identity.Data;
using ApiSpotify.REPOSITORY;
using ApiSpotify.MODELS;
using Microsoft.AspNetCore.Mvc;

namespace ApiSpotify.ENDPOINTS
{
    public static class login
    {
        public static void MaploginEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapPost("/login", ([FromBody] LoginRequest req) =>
            {
                // 1. Obtener usuario por nombre
                Usuari user = DAOUsuari.GetByNom(dbConn, req.nom);

                if (user == null)
                    return Results.Unauthorized();

                // 2. Validar contraseña usando hash + salt
                bool valid = PasswordHelper.VerifyPassword(
                    req.Password,
                    user.Contrasenya,
                    user.Salt
                );

                if (!valid)
                    return Results.Unauthorized();

                return Results.Ok(new { message = "Login correcte", userId = user.Id });
            });

        }
    }
}
