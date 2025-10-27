

    //Post
using ApiSpotify.MODELS;


namespace ApiSpotify.DTO;

    public record ProductRequest(string Titol, string Artista, string Album, Decimal Durada)
    {
        // Guanyem CONTROL sobre com es fa la conversió

        public Canco ToProduct(Guid id)   // Conversió a model
        {
            return new Canco
            {
                Id = id,
                Titol = Titol,
                Artista = Artista,
                Album = Album,
                Durada = Durada
            };
        }
    }


