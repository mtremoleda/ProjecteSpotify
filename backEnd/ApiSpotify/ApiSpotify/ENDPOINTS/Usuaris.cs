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

        }
    }
}
