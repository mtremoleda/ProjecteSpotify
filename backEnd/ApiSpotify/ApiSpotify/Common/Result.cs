namespace ApiSpotify.Common
{
    public class Result
    {
        public bool IsOk { get; }
        public string? ErrorMessage { get; }
        public string? ErrorCode { get; }

        private Result(bool isOk, string? errorMessage = null, string? errorCode = null)
        {
            IsOk = isOk;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public static Result Ok() => new Result(true);

        public static Result Failure(string errorMessage, string? errorCode = null) =>
            new Result(false, errorMessage, errorCode);
    }
}
