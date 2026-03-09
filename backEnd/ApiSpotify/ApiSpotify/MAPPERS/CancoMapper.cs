using ApiSpotify.DOMAIN.Entities;
using ApiSpotify.MODELS;
using ApiSpotify.DOMAIN.Entities;
namespace ApiSpotify;

public static class CancoMapper
{

    public static CancoEntity ToDomain(Canco canco)
        => new CancoEntity(
            canco.Id,
            canco.Titol,
            canco.Artista,
            canco.Album,
            canco.Durada
        );
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