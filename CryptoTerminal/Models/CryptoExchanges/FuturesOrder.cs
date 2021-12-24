namespace CryptoTerminal.Models.CryptoExchanges
{
    public class FuturesOrder
    {
        private string _symbol;
        private decimal _amount;
        private decimal? _price;
        private OrderType _type;
        private OrderSide _side;
        private DateTime _createdDate;
        private TimeInForce? _tif;
        private bool _reduceOnly;
        
        public FuturesOrder(string symbol, decimal amount, OrderSide orderSide, OrderType orderType, DateTime? date, decimal? price, TimeInForce? tif, bool? reduceOnly)
        {
            _symbol = symbol;
            _price = price;
            _amount = amount;
            _tif = tif;
            _reduceOnly = reduceOnly ?? false;
            _createdDate = date ?? DateTime.Now;
        }

        public string Symbol { get => _symbol; }

        public decimal Amount { get => _amount; }

        public OrderType OrderType { get => _type; }

        public OrderSide OrderSide { get => _side; }

        public DateTime CreatedDate { get => _createdDate; }

        public decimal? Price { get => (OrderType == OrderType.Limit) ? _price : null; }

        public TimeInForce? TIF { get => (OrderType == OrderType.Limit) ? _tif : null; }

        public bool ReduceOnly { get => _reduceOnly; }
    }
}
