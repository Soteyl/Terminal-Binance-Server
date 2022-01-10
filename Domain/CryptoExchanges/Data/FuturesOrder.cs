namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    using Enums;

    public class FuturesOrder
    {
        private string _symbol;
        private decimal _amount;
        private decimal? _price;
        private long? _id;
        private OrderType _type;
        private OrderSide _side;
        private PositionSide _positionSide;
        private DateTime _createdDate;
        private TimeInForce? _tif;
        private bool _reduceOnly;
        
        public FuturesOrder(string symbol, decimal amount, OrderSide orderSide, OrderType orderType, PositionSide positionSide, DateTime? date, long? id = null, decimal? price = null, TimeInForce? tif = null, bool? reduceOnly = false)
        {
            _tif = tif;
            _price = price;
            _symbol = symbol;
            _amount = amount;
            _side = orderSide;
            _type = orderType;
            _positionSide = positionSide;
            _reduceOnly = reduceOnly ?? false;
            _createdDate = date ?? DateTime.Now;
        }

        public string Symbol { get => _symbol; }

        public decimal Amount { get => _amount; }

        public OrderType OrderType { get => _type; }

        public OrderSide OrderSide { get => _side; }

        public DateTime CreatedDate { get => _createdDate; }

        public long? Id { get => _id; } 

        public decimal? Price { get => (OrderType == OrderType.Limit) ? _price : null; }

        public TimeInForce? TIF { get => (OrderType == OrderType.Limit) ? _tif : null; }

        public PositionSide PositionSide { get => _positionSide; }

        public bool ReduceOnly { get => _reduceOnly; }
    }
}
