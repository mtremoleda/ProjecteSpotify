using ApiSpotify.DTO;
using ApiSpotify.Common;

namespace ApiSpotify.VALIDATIONS
{
    public static class SongsValidator
    {
        public static Result Validate(CancoRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Titol))
            {
                return Result.Failure("El nom és obligatori", "NOM_OBLIGATORI");
            }

            if (req.Artista.Length > 50)
            {
                return Result.Failure("L'artista no pot superar els 50 caràcters", "NOM_MASSA_LLARG");
            }
            if (req.Album.Length > 50)
            {
                return Result.Failure("L'album no pot superar els 50 caràcters", "NOM_MASSA_LLARG");
            }

            if (req.Durada <= 0)
            {
                return Result.Failure("La durada no pot ser igual o mes petita a 0", "DURADA_MASSA_PETITA");
            }

            return Result.Ok();
        }
    }
}
