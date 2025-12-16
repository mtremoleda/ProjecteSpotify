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
            var resultNom = ValidateNom(req.Nom);
            if (!resultNom.IsOk)
                return resultNom;

            var resultContrasenya = ValidateContrasenya(req.Contrasenya);
            if (!resultContrasenya.IsOk)
                return resultContrasenya;

            return Result.Ok();
        }

        private static Result ValidateNom(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                return Result.Failure(
                    "El nom és obligatori",
                    "NOM_OBLIGATORI"
                );
            }

            if (nom.Length > NOM_MAX_LONGITUD)
            {
                return Result.Failure(
                    $"El nom no pot superar els {NOM_MAX_LONGITUD} caràcters",
                    "NOM_MASSA_LLARG"
                );
            }

            return Result.Ok();
        }

        private static Result ValidateContrasenya(string contrasenya)
        {
            if (string.IsNullOrWhiteSpace(contrasenya))
            {
                return Result.Failure(
                    "La contrasenya és obligatòria",
                    "CONTRASENYA_OBLIGATORIA"
                );
            }

            if (contrasenya.Length < CONTRASENYA_MIN_LONGITUD ||
                contrasenya.Length > CONTRASENYA_MAX_LONGITUD)
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
