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
    public static class EndpointRol
    {
        public static void MapRolEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // GET ALL
            app.MapGet("/rols/{id}/permisos", (Guid id) =>
            {
                List<RolPermisos> rolpermisos = DAORolPermisos.GetAll(dbConn, id);
                return Results.Ok(rolpermisos);
            });

            

            // POST
            app.MapPost("/rols/{id}/permisos", ([FromBody] RolPermisosRequest req, Guid id) =>
            {
                Console.WriteLine($"rolid, {id}");

                RolPermisos rolpermisos = new RolPermisos
                {
                    Id = Guid.NewGuid(),
                    RolId = id,
                    PermisosId = req.PermisosId
                    
                };

                DAORolPermisos.Insert(dbConn, rolpermisos);

                List<RolPermisos> lrp = DAORolPermisos.GetAll(dbConn, id);
                Console.WriteLine($"/rols/{id}/permisos");
                return Results.Ok(lrp);



            });

           

            app.MapDelete("/rolpermisos/{id}", (Guid id) =>
                DAORolPermisos.Delete(dbConn, id) ? Results.NoContent() : Results.NotFound());

        }
    }
}

// DTO pel request
public record RolPermisosRequest(Guid RolId, Guid PermisosId);