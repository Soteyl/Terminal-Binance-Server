namespace CryptoTerminal.Models.CryptoExchanges
{
    public class AdjustLeverageResult
    {
        public AdjustLeverageResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }

        public string? Message { get; set; }
    }


}
}