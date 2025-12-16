using ApiSpotify.Common;
using ApiSpotify.DTO;
using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using ApiSpotify.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiSpotify.ENDPOINTS
{
    public static class EndpointCancons
    {
        public static void MapCancoEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // GET ALL
            app.MapGet("/cancons", () =>
            {
                List<CanconsResponse> canconsResponses = new List<CanconsResponse>();
                List<Canco> cancons = DAOCanco.GetAll(dbConn);

                foreach (Canco c in cancons)
                {
                    canconsResponses.Add(CanconsResponse.FromProduct(c));

                }

                return Results.Ok(canconsResponses);

            });

            // GET BY ID
            app.MapGet("/cancons/{id}", (Guid id) =>
            {
                Canco? canco = DAOCanco.GetById(dbConn, id);

                return canco is not null
                    ? Results.Ok(CanconsResponse.FromProduct(canco))
                    : Results.NotFound(new { message = $"Canco with Id {id} not found." });
            });

            

            // POST /cancons
            app.MapPost("/Cancons", ([FromBody] CancoRequest req) =>
            {

                /* Usuari usuari = DAOUsuari.GetByIdWithRol(dbConn, userId);
                 if (usuari == null) return Results.NotFound(new { message = "Usuari no trobat." });

                 if (!PermisosHelper.UsuariTePermis(usuari, "AfegirCanco"))
                     return Results.Forbid();*/

                Result result = SongsValidator.Validate(req);
                if (!result.IsOk)
                {
                    return Results.BadRequest(new
                    {
                        error = result.ErrorCode,
                        message = result.ErrorMessage
                    });
                }

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

            app.MapPut("/cancons/{id}", (Guid id, CancoRequest req) =>
            {
                var existing = DAOCanco.GetById(dbConn, id);
                if (existing == null)
                    return Results.NotFound();

                /* Usuari usuari = DAOUsuari.GetByIdWithRol(dbConn, userId);
                 if (usuari == null) return Results.NotFound(new { message = "Usuari no trobat." });

                 bool potEditar = PermisosHelper.UsuariTePermis(usuari, "EditarCanco") ||
                                  (usuari.Rol.Nom == "Artista" && existing.Artista == usuari.Nom);

                 if (!potEditar) return Results.Forbid();*/

                Result result = SongsValidator.Validate(req);
                if (!result.IsOk)
                {
                    return Results.BadRequest(new
                    {
                        error = result.ErrorCode,
                        message = result.ErrorMessage
                    });
                }

                Canco updated = new Canco
                {
                    Id = id,
                    Titol = req.Titol,
                    Artista = req.Artista,
                    Album = req.Album,
                    Durada = req.Durada
                };

                DAOCanco.Update(dbConn, updated);
                return Results.Ok(updated);
            });

            //Delete
            app.MapDelete("/cancons/{id}", (Guid id, [FromQuery] Guid userId) =>
            {
                var canco = DAOCanco.GetById(dbConn, id);
                if (canco == null) return Results.NotFound();

              /*  Usuari usuari = DAOUsuari.GetByIdWithRol(dbConn, userId);
                if (usuari == null) return Results.NotFound(new { message = "Usuari no trobat." });

                if (!PermisosHelper.UsuariTePermis(usuari, "EliminarCanco"))
                    return Results.Forbid();*/

                return DAOCanco.Delete(dbConn, id) ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}

    // DTO pel request
    public record CancoRequest(String Titol, string Artista, string Album, decimal Durada);
