

    //Post
using ApiSpotify.MODELS;
using ApiSpotify.DOMAIN.Entities;

using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ApiSpotify.DTO;

    public record CancoRequest(string Titol, string Artista, string Album, Decimal Durada)
    {
        

        public CancoEntity ToCanco(Guid IdCanco)   
        {

            CancoEntity cancoEntity = new CancoEntity();

                cancoEntity.Id = IdCanco;
                cancoEntity.Titol = Titol;
                cancoEntity.Artista = Artista;
                cancoEntity.Album = Album;
                cancoEntity.Durada = Durada;

                return cancoEntity;
        

        }
    }


