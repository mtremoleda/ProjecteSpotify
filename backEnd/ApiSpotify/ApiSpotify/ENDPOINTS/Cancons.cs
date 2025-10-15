using ApiSpotify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;

namespace ApiSpotify.ENDPOINTS
{
    public static class EndpointCancons
    {
        public static void MapCancoEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // GET ALL
            app.MapGet("/cancons", () =>
            {
                List<Canco> cancons = DAOCanco.GetAll(dbConn);
                return Results.Ok(cancons);
            });

            // GET BY ID
            app.MapGet("/cancons/{id}", (Guid id) =>
            {
                Canco? canco = DAOCanco.GetById(dbConn, id);

                return canco is not null
                    ? Results.Ok(canco)
                    : Results.NotFound(new { message = $"Canco with Id {id} not found." });
            });

            // POST /cancons
            app.MapPost("/InsertCancons", (CancoRequest req) =>
            {
                Canco canco = new Canco
                {
                    Id = Guid.NewGuid(),
                    Titol = req.Titol,
                    Artista = req.Artista,
                    Album = req.Album,
                    Durada = req.Durada
                };

                DAOCanco.Insert(dbConn, canco);

                return Results.Created($"/cancons/{canco.Id}", canco);



            });
        }
    }

}

    // DTO pel request
    public record CancoRequest(String Titol, string Artista, string Album, decimal Durada);
