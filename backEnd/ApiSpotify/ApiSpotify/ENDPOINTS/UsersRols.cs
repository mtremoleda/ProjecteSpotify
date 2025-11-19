using ApiSpotify.REPOSITORY;
using ApiSpotify.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSpotify.ENDPOINTS
{
    public static class EndointUsersRols
    {

        public static void MapUsersRolsEndpoints(this WebApplication app, DatabaseConnection dbConn)
        {
            // GET ALL
            app.MapGet("/usersrols", () =>
            {
                List<UsersRols> usersrols = DAOUsersRols.GetAll(dbConn);
                return Results.Ok(usersrols);
            });



            // POST
            app.MapPost("/usersrols", ([FromBody] UsersRolsRequest req) =>
            {
                UsersRols usersrols = new UsersRols
                {
                    Id = Guid.NewGuid(),
                    UserId = req.UserId,
                    RolId = req.RolId

                };

                DAOUsersRols.Insert(dbConn, usersrols);

                return Results.Created($"/usersrols/{usersrols.Id}", usersrols);



            });


            app.MapDelete("/usersrols/{id}", (Guid id) =>
                DAOUsersRols.Delete(dbConn, id) ? Results.NoContent() : Results.NotFound());

        }
    }
}

// DTO pel request
public record UsersRolsRequest(Guid UserId, Guid RolId);
