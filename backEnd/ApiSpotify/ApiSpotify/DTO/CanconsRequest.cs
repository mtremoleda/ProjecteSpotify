

    //Post
using ApiSpotify.MODELS;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ApiSpotify.DTO;

    public record CancoRequest(string Titol, string Artista, string Album, Decimal Durada)
    {
        // Guanyem CONTROL sobre com es fa la conversió

        public Canco ToCanco()   // Conversió a model
        {

            Canco cancoDomain = new Canco();

        
            cancoDomain.Titol = Titol;
            cancoDomain.Artista = Artista;
            cancoDomain.Album = Album;
            cancoDomain.Durada = Durada;

            return cancoDomain;
        

        }
    }


