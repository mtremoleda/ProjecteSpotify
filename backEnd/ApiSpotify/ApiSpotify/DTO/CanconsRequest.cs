

    //Post
using ApiSpotify.MODELS;
using ApiSpotify.DOMAIN.Entities;

using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ApiSpotify.DTO;

    public record CancoRequest(string Titol, string Artista, string Album, Decimal Durada)
    {
        

        public CancoEntity ToCanco(Guid Id)   
        {

            CancoEntity cancoEntity = new CancoEntity(Id, Titol, Artista, Album, Durada);

            return cancoEntity;
        

        }
    }


