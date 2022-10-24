namespace Ixcent.CryptoTerminal.Application.Status
{
    public static class ServerResponseCode
    {
        // Authentication
        public const string UserFailedToAuthorize = "USER_FAILED_TO_ATHORIZE";
        public const string UserAlreadyExists = "USER_ALREADY_EXISTS";
        public const string EmailAlreadyExists = "EMAIL_ALREADY_EXISTS";
        
        public const string MissingApiToken = "API_TOKEN_MISSING";
        public const string InvalidApiToken = "API_TOKEN_INVALID";

        public const string InternalError = "INTERNAL_ERROR";

        public const string UnknownError = "UNKNOWN_ERROR";
    }
}
