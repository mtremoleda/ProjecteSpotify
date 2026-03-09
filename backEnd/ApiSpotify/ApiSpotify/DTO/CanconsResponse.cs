
//Get
using ApiSpotify.MODELS;
using ApiSpotify.DOMAIN.Entities;

namespace ApiSpotify.DTO;


public record CanconsResponse(Guid Id, string Titol, string Artista, string Album, Decimal? Durada)
{
    

    public static CanconsResponse FromProduct(CancoEntity cancoEntity)   
    {
        return new CanconsResponse(cancoEntity.Id, cancoEntity.Titol, cancoEntity.Artista, cancoEntity.Album, cancoEntity.Durada);
    }
}


