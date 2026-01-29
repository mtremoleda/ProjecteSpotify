using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using TagLib;
using System.IO;
using System.Threading.Tasks;

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

                var opcions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 2
                };

                var llistaFitxers = files.ToList();

                await Task.Run(() =>
                {
                    Parallel.ForEach(llistaFitxers, opcions, file =>
                    {
                        try
                        {
                            string rutaFitxer = SaveFile(file).Result;

                            using var tagFile = TagLib.File.Create(rutaFitxer);
                            var tag = tagFile.Tag;
                            var propietats = tagFile.Properties;

                            var canco = new Canco
                            {
                                Id = Guid.NewGuid(),
                                Titol = string.IsNullOrWhiteSpace(tag.Title)
                                    ? Path.GetFileNameWithoutExtension(file.FileName)
                                    : tag.Title,
                                Artista = (tag.Performers != null && tag.Performers.Length > 0)
                                    ? tag.Performers[0]
                                    : "Desconegut",
                                Album = string.IsNullOrWhiteSpace(tag.Album)
                                    ? "Desconegut"
                                    : tag.Album,
                                Durada = (decimal)propietats.Duration.TotalSeconds,

                            };

                            canconsProcessades.Add(canco);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processant {file.FileName}: {ex.Message}");
                        }
                    });
                });

                return Results.Ok(canconsProcessades);
            }).DisableAntiforgery();
        }


        // FALTA canviar el nom per tal de que el nom sigui l'id de la canço i tambe falta que es guardi a la base de dades la ruta relativa de la carpeta UPLOADS/...
        public static async Task<string> SaveFile(IFormFile fitxer, Guid cancoId)
        {
            string UPLOADS = Path.Combine(Directory.GetCurrentDirectory(), "UPLOADS");

            if (!Directory.Exists(UPLOADS))
                Directory.CreateDirectory(UPLOADS);

            string extensio = Path.GetExtension(fitxer.FileName); // .mp3
            string nomFitxer = $"{cancoId}{extensio}";
            string rutaCompleta = Path.Combine(UPLOADS, nomFitxer);

            using (FileStream stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await fitxer.CopyToAsync(stream);
            }

            // 👉 retornem RUTA RELATIVA
            return Path.Combine("UPLOADS", nomFitxer);
        }


    }
}
