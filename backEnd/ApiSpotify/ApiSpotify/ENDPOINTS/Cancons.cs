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
        public static void MapProductEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            app.MapGet("/canco", () =>
            {
                List<Canco> cancons = DAOCanco.GetAll(dbConn);
                return Results.Ok(cancons);
            });

            app.MapGet("/canco/{id}", (Guid id) =>
            {
                Canco? canco = DAOCanco.GetById(dbConn, id);

                return product is not null
                    ? Results.Ok(product)
                    : Results.NotFound(new { message = $"Product with Id {id} not found." });
            });


        }
    }
}
