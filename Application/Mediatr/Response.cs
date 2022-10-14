using Ixcent.CryptoTerminal.Application.Status;

namespace Ixcent.CryptoTerminal.Application.Mediatr
{
    public class Response
    {
        public ResponseError? Error { get; set; }

        public bool IsSuccess => Error == null;

        #region patterns
        
        public static Response Success()
            => new();
        
        public static Response<TResponse> Success<TResponse>(TResponse response)
            => new() { Result = response };

        public static Response WithError(string status)
            => new() { Error = new() {  StatusCode = status } };
        
        public static Response<TResponse> WithError<TResponse>(string status)
            => new() { Error = new() { StatusCode = status } };
        
        #endregion
    }
    
    public class Response<T>: Response
    {
        public T? Result { get; set; }
    }
}