namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Results
{
    public class MakeGridResult
    {
        public MakeGridResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string? Message { get; set; }
    }
}