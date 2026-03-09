
using ApiSpotify.Common;
using ApiSpotify.MODELS;

namespace ApiSpotify.DOMAIN.Validators;
public static class CancoValidator
{
    public static Result Validate(Canco canco)
    {
        //Validació del client
        if (canco.Titol == null)
            return Result.Failure("El titol no pot ser null", "TITUL_NULL");

        return Result.Ok();
    }
}