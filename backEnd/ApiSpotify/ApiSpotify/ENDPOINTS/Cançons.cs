using Botiga.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiSpotify.ENDPOINTS
{
    public static class Cançons
    {
        public static void MapProductEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapGet("/cançons", () =>
            {
                List<Cançons> cançons = 
            });
        }
    }
}
