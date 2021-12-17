﻿namespace CryptoTerminal.Models.CryptoExchanges
{
    public class SpotOrder : ICloneable
    {
        private const string _pairDelimiter = "/";

        private string _firstCoin;

        private string _secondCoin;

        private decimal _amountFirst;

        private decimal _price;

        private OrderSide _orderSide;

        private OrderType _orderType;

        private DateTime _dateTime;

        public SpotOrder(string pair, decimal amountFirst, decimal price, OrderSide orderSide, OrderType orderType)
        {
            string[] coins = pair.Split(_pairDelimiter);
            _firstCoin = coins[0];
            _secondCoin = coins[1];
            _amountFirst = amountFirst;
            _price = price;
            _orderSide = orderSide;
            _orderType = orderType;
        }

        public SpotOrder(string pair, decimal amount, decimal price, OrderSide orderSide, OrderType orderType, DateTime dateTime) 
            : this(pair, amount, price, orderSide, orderType)
        {
            _dateTime = dateTime;
        }

        public string Pair => _firstCoin + _pairDelimiter + _secondCoin;

        public string FirstCoin => _firstCoin;

        public string SecondCoin => _secondCoin;

        public decimal AmountFirst => _amountFirst;

        public decimal Price => _price;

        public OrderSide OrderSide => _orderSide;

        public OrderType OrderType => _orderType;

        public DateTime DateTime => _dateTime;

        public override bool Equals(object? obj)
        {
            if (obj == null) 
                return false;

            SpotOrder order = obj as SpotOrder;
            
            if (order == null) 
                return false;
            else 
                return order.DateTime == DateTime && 
                    order.Pair == Pair && 
                    order.AmountFirst == AmountFirst && 
                    order.Price == Price &&
                    order.OrderSide == OrderSide && 
                    order.OrderType == OrderType;
        }

        public object Clone()
        {
            return new SpotOrder(Pair, AmountFirst, Price, OrderSide, OrderType);
        }

    }
}
