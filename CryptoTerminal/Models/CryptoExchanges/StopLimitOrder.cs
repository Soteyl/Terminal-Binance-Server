namespace CryptoTerminal.Models.CryptoExchanges
{
    public class StopOrder : SpotOrder
    {
        private decimal _triggerPrice;

        public StopOrder(string pair, decimal amount, decimal price, OrderSide orderSide, OrderType orderType, decimal triggerPrice)
            : base(pair, amount, orderSide, orderType, price)
        {
            _triggerPrice = triggerPrice;
        }

        public StopOrder(string pair, decimal amount, decimal price, OrderSide orderSide, OrderType orderType, DateTime dateTime, decimal triggerPrice)
            : base(pair, amount, orderSide, orderType, price, dateTime)
        {
            _triggerPrice = triggerPrice;
        }

        public decimal TriggerPrice => _triggerPrice;
    }
}
