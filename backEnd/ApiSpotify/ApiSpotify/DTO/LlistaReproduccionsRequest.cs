using ApiSpotify.MODELS;

namespace ApiSpotify.DTO;

public record LlistaReproduccioRequest(Guid IdUsuari, string Nom)
{
    public LlistaReproduccio ToLlistaReproduccio(Guid id)
    {
        return new LlistaReproduccio
        {
            Id = id,
            IdUsuari = IdUsuari,
            Nom = Nom
        };
    }
}
