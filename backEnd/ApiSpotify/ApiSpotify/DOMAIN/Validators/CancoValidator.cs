
using ApiSpotify.Common;
using ApiSpotify.DOMAIN.Entities;
using ApiSpotify.MODELS;

namespace ApiSpotify.DOMAIN.Validators;
public static class CancoValidator
{
    public static Result Validate(CancoEntity cancoEntity)
    {
        //Validació del client
        if (cancoEntity.Titol == null)
            return Result.Failure("El titol no pot ser null", "TITUL_NULL");

        return Result.Ok();
    }
}