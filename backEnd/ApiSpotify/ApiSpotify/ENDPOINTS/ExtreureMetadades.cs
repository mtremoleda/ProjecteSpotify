using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using TagLib;
using System.IO;
using System.Threading.Tasks;
using TagLib.Aac;

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

                ConcurrentBag<Canco> canconsProcessades = new ConcurrentBag<Canco>();

                var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };

                // Transformem la col·lecció a una llista per a poder-la iterar en Parallel.ForEach
                var fileList = files.ToList();

                

        }

    }
}
