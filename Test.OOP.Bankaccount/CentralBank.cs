using TEST.OOP.BankAccount.Abstract;
using TEST.OOP.BankAccount.Static;

namespace TEST.OOP.BankAccount
{
    class CentralBank : Bank
    {
        public int _interestRate;
        public bool CheckTransfer(Bank from, Bank to, FIATDespositRequest data)
        {
            if (from.Country == to.Country)
            {
                return from.Transfer(to, data);
            }
            else
            {
                if (WordBank.Transfer((CommertialBank)from, (CommertialBank)to, data))
                {
                    return true;
                }
                return false;
            }
        }
        const decimal _maxInterestTax = 5;
        public CentralBank(string name, string Country, int MaxInterestRate) : base(name, Country)
        {

        }
    }

}

