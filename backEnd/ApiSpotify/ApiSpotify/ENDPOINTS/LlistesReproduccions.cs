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

        }
    }
}
