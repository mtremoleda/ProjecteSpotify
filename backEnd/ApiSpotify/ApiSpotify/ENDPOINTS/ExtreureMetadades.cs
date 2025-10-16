using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ApiSpotify.ENDPOINTS
{
    public static class ExtreureMetadades
    {
        public static void MapExtreureMetadadesEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // POST /cancons/upload
            app.MapPost("/cancons/upload", async ([FromForm] IFormFileCollection files) =>
            {
                if (files == null || files.Count == 0)
                    return Results.BadRequest(new { message = "No s'ha rebut cap fitxer." });

                // Col·lecció segura per a fils per guardar resultats
                ConcurrentBag<Canco> canconsProcessades = new ConcurrentBag<Canco>();

                // Processar amb un màxim de 2 fils
                var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };

            });
        }
    
    }
}
