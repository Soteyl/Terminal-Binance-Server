namespace CryptoTerminal.Models.CryptoExchanges
{
    public class Transaction
    {
        private DateTime _dateTime;

        private string _pair;

        private OrderSide _side;

        private decimal _price;

        private decimal _amount;

        private decimal _fee;

        private string _feeCoin;

        private decimal _sum;

        public Transaction(DateTime dateTime, string pair, OrderSide side, decimal price, decimal amount, decimal fee, string feeCoin, decimal sum)
        {
            _dateTime = dateTime;
            _pair = pair;
            _side = side;
            _price = price;
            _amount = amount;
            _fee = fee;
            _feeCoin = feeCoin;
            _sum = sum;
        }

        public DateTime DateTime => _dateTime;

        public string Pair => _pair;

        public OrderSide Side => _side;

        public decimal Price => _price;

        public decimal Amount => _amount;

        public decimal Fee => _fee;

        public string FeeCoin => _feeCoin;

        public decimal Sum => _sum;
    }
}