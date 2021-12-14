namespace CryptoTerminal.Models.CryptoExchanges
{
    public class Order
    {
        private const string _pairDelimiter = "/";

        private string _firstCoin;

        private string _secondCoin;

        private decimal _amount;

        private decimal? _price;

        private OrderSide _orderSide;

        private OrderType _orderType;

        private DateTime _dateTime;

        public Order(string pair, decimal amount, decimal? price, OrderSide orderSide, OrderType orderType)
        {
            string[] coins = pair.Split(_pairDelimiter);
            _firstCoin = coins[0];
            _secondCoin = coins[1];
            _amount = amount;
            _price = price;
            _orderSide = orderSide;
            _orderType = orderType;
        }

        public Order(string pair, decimal amount, decimal? price, OrderSide orderSide, OrderType orderType, DateTime dateTime) 
            : this(pair, amount, price, orderSide, orderType)
        {
            _dateTime = dateTime;
        }

        public string Pair => _firstCoin + _pairDelimiter + _secondCoin;

        public string FirstCoin => _firstCoin;

        public string SecondCoin => _secondCoin;

        public decimal Amount => _amount;

        public decimal? Price => _price;

        public OrderSide OrderSide => _orderSide;

        public OrderType OrderType => _orderType;

        public DateTime DateTime => _dateTime;
    }
}
