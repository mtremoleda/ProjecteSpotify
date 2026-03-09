using ApiSpotify.Common;
using ApiSpotify.DOMAIN.Entities;
using ApiSpotify.DTO;
using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using ApiSpotify.DOMAIN.Validators;
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
                List<Canco> cancons = DAOCanco.GetAll(dbConn);
                List<CanconsResponse> canconsResponses = new List<CanconsResponse>();
                

                foreach (Canco canco in cancons)
                {
                    CancoEntity cancoEntity = CancoMapper.ToDomain(canco);
                    canconsResponses.Add(CanconsResponse.FromProduct(cancoEntity));

                }

                return Results.Ok(canconsResponses);

            });

            //app.MapGet("/products", (IProductRepo productADO, int? total) =>
            //{
            //    int limit = total ?? 20;

            //    List<ProductEntity> products = productADO.GetAll(limit);
            //    List<ProductResponse> productsResponse = new List<ProductResponse>();
            //    foreach (ProductEntity productEntity in products)
            //    {
            //        Product product = ProductMapper.ToDomain(productEntity);
            //        productsResponse.Add(ProductResponse.FromProduct(product));
            //    }

            //    return Results.Ok(productsResponse);
            //}).WithTags("Products");

            // GET BY ID
            app.MapGet("/cancons/{id}", (Guid id) =>
            {
                Canco? canco = DAOCanco.GetById(dbConn, id);
                CancoEntity cancoEntity = CancoMapper.ToDomain(canco);

                return canco is not null
                    ? Results.Ok(CanconsResponse.FromProduct(cancoEntity))
                    : Results.NotFound(new { message = $"Canco with Id {id} not found." });
            });


            // POST /cancons
            app.MapPost("/Cancons", ([FromBody] CancoRequest req) =>
            {

                /* Usuari usuari = DAOUsuari.GetByIdWithRol(dbConn, userId);
                 if (usuari == null) return Results.NotFound(new { message = "Usuari no trobat." });

                 if (!PermisosHelper.UsuariTePermis(usuari, "AfegirCanco"))
                     return Results.Forbid();*/
                Guid Id = Guid.NewGuid();
                CancoEntity cancoEntity = req.ToCanco(Id);
                Result result = CancoValidator.Validate(cancoEntity);
                if (!result.IsOk)
                {
                    return Results.BadRequest(new
                    {
                        error = result.ErrorCode,
                        message = result.ErrorMessage
                    });
                }

                Canco canco = CancoMapper.ToEntity(cancoEntity);
                DAOCanco.Insert(dbConn, canco);


                return Results.Created($"/cancons/{canco.Id}", canco);

            });

            app.MapPut("/cancons/{id}", (Guid Id, CancoRequest req) =>
            {
                var existing = DAOCanco.GetById(dbConn, Id);
                if (existing == null)
                    return Results.NotFound();

                /* Usuari usuari = DAOUsuari.GetByIdWithRol(dbConn, userId);
                 if (usuari == null) return Results.NotFound(new { message = "Usuari no trobat." });

                 bool potEditar = PermisosHelper.UsuariTePermis(usuari, "EditarCanco") ||
                                  (usuari.Rol.Nom == "Artista" && existing.Artista == usuari.Nom);

                 if (!potEditar) return Results.Forbid();*/

                CancoEntity cancoEntity = req.ToCanco(Id);
                Result result = CancoValidator.Validate(cancoEntity);
                if (!result.IsOk)
                {
                    return Results.BadRequest(new
                    {
                        error = result.ErrorCode,
                        message = result.ErrorMessage
                    });
                }

                Canco cancoUpdt = CancoMapper.ToEntity(cancoEntity);
                DAOCanco.Update(dbConn, cancoUpdt);
                return Results.Ok(cancoUpdt);

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
    //public record CancoRequest(String Titol, string Artista, string Album, decimal Durada);
