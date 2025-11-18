using ApiSpotify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApiSpotify.MODELS;
using ApiSpotify.REPOSITORY;
using Microsoft.AspNetCore.Mvc;

namespace ApiSpotify.ENDPOINTS
{
    public static class EndpointRolPermisos
    {
        public static void MapRolPermisosEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // GET ALL
            app.MapGet("/rolpermisos", () =>
            {
                List<RolPermisos> rolpermisos = DAORolPermisos.GetAll(dbConn);
                return Results.Ok(rolpermisos);
            });

            

            // POST /cancons
            app.MapPost("/rolpermisos", ([FromBody] RolPermisosRequest req) =>
            {
                RolPermisos rolpermisos = new RolPermisos
                {
                    Id = Guid.NewGuid(),
                    RolId = req.RolId,
                    PermisosId = req.PermisosId
                    
                };

                DAORolPermisos.Insert(dbConn, rolpermisos);

                return Results.Created($"/rolpermisos/{rolpermisos.Id}", rolpermisos);



            });

           

            app.MapDelete("/rolpermisos/{id}", (Guid id) =>
                DAORolPermisos.Delete(dbConn, id) ? Results.NoContent() : Results.NotFound());

        }
    }
}

// DTO pel request
public record RolPermisosRequest(Guid RolId, Guid PermisosId);