using ApiSpotify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApiSpotify.MODELS;

namespace ApiSpotify.ENDPOINTS
{
    public static class Cancons
    {
        public static void MapProductEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapGet("/cançons", () =>
            {
                List<Canco> cancons = 
            });
        }
    }
}
