using ApiSpotify.Services;
using Microsoft.AspNetCore.Identity.Data;
using ApiSpotify.REPOSITORY;
using ApiSpotify.MODELS;
using Microsoft.AspNetCore.Mvc;
using ApiSpotify.UTILS;

namespace ApiSpotify.ENDPOINTS
{
    public static class loginendpoint
    {
        public static void MaploginEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapPost("/login", ([FromBody] login req, JswTokenService jwtService) =>
            {
                // Buscar usuari per nom
                Usuari user = DAOUsuari.GetByNom(dbConn, req.nom);
                List<Rol> rol = DAOUsersRols.GetUserRols(dbConn, user.Id);
                List<String> rols = new List<String>();

                foreach(Rol r in rol)
                {
                    rols.Add(r.Nom);
                }

                if (user == null)
                    return Results.Unauthorized();

                // Validar contrasenya
                bool valid = UtilsContrasenya.VerifyPassword(
                    req.contrasenya,  // <- contrasenya enviada del WPF
                    user.Contrasenya, // <- hash de la BD
                    user.Salt         // <- salt de la BD
                );

                if (!valid)
                    return Results.Unauthorized();


                return Results.Ok(jwtService.GenerateToken(
                userId: user.Id.ToString(),
                nom: user.Nom,
                issuer: "Spotify",
                rols: rols,
                audience: "public",
                lifetime: TimeSpan.FromHours(2)
                ));

                //return Results.Ok(new { message = "Login correcte", userId = user.Id });
            });

        }
    }
}
