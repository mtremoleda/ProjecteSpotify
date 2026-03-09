using ApiSpotify.DOMAIN.Entities;
using ApiSpotify.MODELS;
namespace ApiSpotify;

public static class CancoMapper
{
    public static Canco ToEntity(CancoEntity cancoEntity)
        => new Canco
        {
            Id = cancoEntity.Id,
            Titol = cancoEntity.Titol,
            Artista = cancoEntity.Artista,
            Album = cancoEntity.Album,
            Durada = cancoEntity.Durada
            
        };
}