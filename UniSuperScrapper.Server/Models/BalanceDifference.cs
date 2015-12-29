using System;
using UpSoft.UniSuperScrapper.DbAccess;

namespace UpSoft.UniSuperScrapper.ServiceLibrary.Models
{
    public class BalanceDifference
    {
        private Balance _lastBalance;
        private Balance _newBalance;
        public BalanceDifference(Balance lastBalance, Balance newBalance)
        {
            _lastBalance = lastBalance;
            _newBalance = newBalance;
        }

        public TransitionType TransitionType
        {
            get
            {
                if (_lastBalance.Amount > _newBalance.Amount)
                    return TransitionType.Decrease;

                if (_lastBalance.Amount < _newBalance.Amount)
                    return TransitionType.Increase;

                return TransitionType.NoChange;
            }
        }

        public decimal Amount
        {
            get { return _newBalance.Amount - _lastBalance.Amount; }
        }

        public decimal Percentage
        {
            get { return Amount/_lastBalance.Amount; }
        }
    }

    public enum TransitionType
    {
        NoChange,
        Increase,
        Decrease
    }
}