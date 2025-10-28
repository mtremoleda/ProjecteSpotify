using ApiSpotify.MODELS;
using System.Linq;

public static class PermisosHelper
{
    // accio ha de ser exactament el nom del permís de la BD
    public static bool UsuariTePermis(Usuari usuari, string accio)
    {
        // Si és Administrador, té tots els permisos
        if (usuari.Rol.Nom == "Administrador") return true;

        
        // Mirar si algun dels RolPermisos coincideix amb l'acció
        return usuari.Rol.RolPermisos != null && usuari.Rol.RolPermisos.Any(rp => rp.Permiso.Nom == accio);

    }
}