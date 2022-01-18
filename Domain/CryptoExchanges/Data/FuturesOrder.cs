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

        public string Symbol { get => _symbol; set => _symbol = value; }

        public decimal Amount { get => _amount; set => _amount = value; }

        public OrderType OrderType { get => _type; set => _type = value; }

        public OrderSide OrderSide { get => _side; set => _side = value; }

        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value;  }

        public long? OrderId { get => _id; set => _id = value; } 

        public decimal? Price { get => (OrderType == OrderType.Limit) ? _price : null; set => _price = value; }

        public TimeInForce? TIF { get => (OrderType == OrderType.Limit) ? _tif : null; set => _tif = value; }

        public PositionSide PositionSide { get => _positionSide; set => _positionSide = value; }

        public bool ReduceOnly { get => _reduceOnly; set => _reduceOnly = value; }
    }
}
