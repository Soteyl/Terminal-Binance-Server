namespace CryptoTerminal.Models.CryptoExchanges
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