using ApiSpotify.MODELS;

namespace ApiSpotify.DTO;

public record LlistaReproduccioResponse(Guid Id, Guid IdUsuari, string Nom)
{
    public static LlistaReproduccioResponse FromLlistaReproduccio(LlistaReproduccio llista)
    {
        return new LlistaReproduccioResponse(llista.Id, llista.IdUsuari, llista.Nom);
    }
}
