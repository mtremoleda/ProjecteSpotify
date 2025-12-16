using ApiSpotify.DTO;
using ApiSpotify.Common;

namespace ApiSpotify.Validations
{
    public static class UsuariValidator
    {
        private const int NOM_MAX_LONGITUD = 50;
        private const int CONTRASENYA_MIN_LONGITUD = 8;
        private const int CONTRASENYA_MAX_LONGITUD = 14;

        public static Result Validate(UsuariRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Nom))
            {
                return Result.Failure("El nom és obligatori", "NOM_OBLIGATORI");
            }

            if (req.Nom.Length > NOM_MAX_LONGITUD)
            {
                return Result.Failure(
                    $"El nom no pot superar els {NOM_MAX_LONGITUD} caràcters",
                    "NOM_MASSA_LLARG"
                );
            }

            if (string.IsNullOrWhiteSpace(req.Contrasenya))
            {
                return Result.Failure("La contrasenya és obligatòria", "CONTRASENYA_OBLIGATORIA");
            }

            if (req.Contrasenya.Length < CONTRASENYA_MIN_LONGITUD ||
                req.Contrasenya.Length > CONTRASENYA_MAX_LONGITUD)
            {
                return Result.Failure(
                    $"La contrasenya ha de tenir entre {CONTRASENYA_MIN_LONGITUD} i {CONTRASENYA_MAX_LONGITUD} caràcters",
                    "CONTRASENYA_INVALIDA"
                );
            }

            return Result.Ok();
        }
    }
}

