using ApiSpotify.MODELS;
using ApiSpotify.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using TagLib;

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

                await Task.Run(() =>
                {
                    Parallel.ForEach(files, options, file =>
                    {
                        try
                        {
                            using var stream = file.OpenReadStream();
                            var tagFile = TagLib.File.Create(new TagLib.StreamFileAbstraction(file.FileName, stream, stream));
                            var tag = tagFile.Tag;
                            var props = tagFile.Properties;

                            var canco = new Canco
                            {
                                Id = Guid.NewGuid(),
                                Titol = tag.Title ?? System.IO.Path.GetFileNameWithoutExtension(file.FileName),
                                Artista = tag.Performers.Length > 0 ? tag.Performers[0] : "Desconegut",
                                Album = tag.Album ?? "Desconegut",
                                Durada = (decimal)props.Duration.TotalSeconds
                            };

                        }
        }
    
    }
}
