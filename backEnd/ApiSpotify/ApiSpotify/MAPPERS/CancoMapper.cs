using ApiSpotify.DOMAIN.Entities;
using ApiSpotify.MODELS;
namespace ApiSpotify;

public static class CancoMapper
{
    public static Canco ToEntity(Guid IdCanco, CancoEntity cancoEntity)
        => new Canco
        {
            Id = IdCanco,
            Titol = cancoEntity.Titol,
            Artista = cancoEntity.Artista,
            Album = cancoEntity.Album,
            Durada = cancoEntity.Durada
            
        };
}