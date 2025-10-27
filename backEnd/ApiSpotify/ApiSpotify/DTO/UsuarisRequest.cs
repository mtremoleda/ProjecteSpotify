using ApiSpotify.MODELS;

namespace ApiSpotify.DTO;

public record UsuariRequest(string Nom, string Contrasenya)
{
    public Usuari ToUsuari(Guid id, string salt)
    {
        return new Usuari
        {
            Id = id,
            Nom = Nom,
            Contrasenya = Contrasenya,
            Salt = salt
        };
    }
}
