using ApiSpotify.DTO;
using ApiSpotify.Common; // Aquí tens el Result

namespace ApiSpotify.VALIDATIONS
{
    public static class UsuariValidator
    {
        public static Result Validate(UsuariRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Nom))
            {
                return Result.Failure("El nom és obligatori", "NOM_OBLIGATORI");
            }

            if (req.Nom.Length > 50)
            {
                return Result.Failure("El nom no pot superar els 50 caràcters", "NOM_MASSA_LLARG");
            }

            if (string.IsNullOrWhiteSpace(req.Contrasenya))
            {
                return Result.Failure("La contrasenya és obligatòria", "CONTRASENYA_OBLIGATORIA");
            }

            if (req.Contrasenya.Length < 8 || req.Contrasenya.Length > 14)
            {
                return Result.Failure("La contrasenya ha de tenir entre 8 i 14 caràcters", "CONTRASENYA_INVALIDA");
            }

            return Result.Ok();
        }
    }
}
