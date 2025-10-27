using ApiSpotify.MODELS;

namespace ApiSpotify.DTO;

public record UsuariResponse(Guid Id, string Nom)
{
    public static UsuariResponse FromUsuari(Usuari usuari)
    {
        return new UsuariResponse(usuari.Id, usuari.Nom);
    }
}
