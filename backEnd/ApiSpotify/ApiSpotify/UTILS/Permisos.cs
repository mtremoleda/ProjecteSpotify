using ApiSpotify.MODELS;


public static class PermisosHelper
{
    public static bool UsuariTePermis(Usuari usuari, string accio)
    {
        // Els rols que vam definir abans
        return usuari.Rol.Nom switch
        {
            "Administrador" => true, // té tots els permisos
            "GestorContingut" => accio == "AfegirCanco" || accio == "EditarCanco" || accio == "EliminarCanco",
            "Artista" => accio == "EditarPropiesCancons", // només pot editar les seves pròpies cançons
            "Usuari" => accio == "EscoltarMusica", // només pot escoltar
            _ => false
        };
    }
}