namespace CryptoTerminal.Models.CryptoExchanges
{
    public class MakeOrderResult
    {
        public MakeOrderResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string? Message { get; set; }
    }
}