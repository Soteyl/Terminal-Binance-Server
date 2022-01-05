namespace Ixcent.CryptoTerminal.Domain.CryptoExchanges.Data
{
    using Enums;

    public class SpotOrder
    {
        private string _pair;

        private decimal _amountFirst;

        private decimal? _price;

        private OrderSide _orderSide;

        private OrderType _orderType;

        private DateTime? _dateTime;

        private TimeInForce? _timeInForce;

        public SpotOrder(string pair, decimal amountFirst, OrderSide orderSide, OrderType orderType, decimal? price = null, DateTime? dateTime = null, TimeInForce? timeInForce = null)
        {
            _pair = pair;
            _amountFirst = amountFirst;
            _price = price;
            _orderSide = orderSide;
            _orderType = orderType;
            _dateTime = dateTime;
            _timeInForce = timeInForce;
        }

        public string Pair { get => _pair; set => _pair = value; }

        public decimal AmountFirst { get => _amountFirst; set => _amountFirst = value; }

        public decimal? Price { get => (OrderType == OrderType.Limit) ? _price : null; set => _price = value; }

        public OrderSide OrderSide { get => _orderSide; set => _orderSide = value; }

        public OrderType OrderType { get => _orderType; set => _orderType = value; }

        public DateTime? DateTime { get => _dateTime; set => _dateTime = value; }

        public TimeInForce? TimeInForce { get => (OrderType == OrderType.Limit) ? _timeInForce : null; set => _timeInForce = value; }
    }
}
