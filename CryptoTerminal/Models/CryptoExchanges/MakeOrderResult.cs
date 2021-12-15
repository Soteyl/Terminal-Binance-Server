namespace CryptoTerminal.Models.CryptoExchanges
{
    public class MakeOrderResult
    {
        public MakeOrderResult(int orderId, bool success, string? message = null)
        {
            OrderId = orderId;
            Success = success;
            Message = message;
        }

        public int OrderId { get; set; }

        public bool Success { get; set; }

        public string? Message { get; set; }
    }
}