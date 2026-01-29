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
                    return Results.BadRequest("No s'ha rebut cap fitxer.");

                var tasques = files.Select(async file =>
                {
                    try
                    {
                        // 1️⃣ Generem ID de la cançó
                        Guid songId = Guid.NewGuid();

                        // 2️⃣ Guardem el fitxer amb nom GUID.ext
                        string rutaRelativa = await SaveFile(file, songId);

                        // 3️⃣ Llegim metadades
                        using var tagFile = TagLib.File.Create(
                            Path.Combine(Directory.GetCurrentDirectory(), rutaRelativa)
                        );

                        var tag = tagFile.Tag;
                        var props = tagFile.Properties;

                        // 4️⃣ Creem la cançó (sense URL)
                        var canco = new Canco
                        {
                            Id = songId,
                            Titol = string.IsNullOrWhiteSpace(tag.Title)
                                ? Path.GetFileNameWithoutExtension(file.FileName)
                                : tag.Title,
                            Artista = tag.Performers?.FirstOrDefault() ?? "Desconegut",
                            Album = string.IsNullOrWhiteSpace(tag.Album) ? "Desconegut" : tag.Album,
                            Durada = (decimal)props.Duration.TotalSeconds
                        };

                        // 5️⃣ Creem URLSong (AQUÍ va la ruta)
                        var urlSong = new URLSong
                        {
                            SongId = songId,
                            Url = rutaRelativa
                        };

                        // 6️⃣ Guardar a BD
                        // dbConn.Insert(canco);
                        // dbConn.Insert(urlSong);

                        return new { canco, urlSong };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processant {file.FileName}: {ex.Message}");
                        return null;
                    }
                });

                var resultat = (await Task.WhenAll(tasques))
                    .Where(r => r != null);

                return Results.Ok(resultat);
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
