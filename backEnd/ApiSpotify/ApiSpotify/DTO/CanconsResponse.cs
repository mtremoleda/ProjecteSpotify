
//Get
using ApiSpotify.MODELS;

namespace ApiSpotify.DTO;


public record CanconsResponse(Guid Id, string Titol, string Artista, string Album, Decimal? Durada)
{
    // Guanyem CONTROL sobre com es fa la conversió

    public static CanconsResponse FromProduct(Canco canco)   // Conversió de model a response
    {
        return new CanconsResponse(canco.Id, canco.Titol, canco.Artista, canco.Album, canco.Durada);
    }
}


