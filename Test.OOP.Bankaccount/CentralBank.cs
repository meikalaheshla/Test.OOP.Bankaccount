using System;
using TEST.OOP.BankAccount.Abstract;
using TEST.OOP.BankAccount.Static;
using static TEST.OOP.BankAccount.CommertialBank;

namespace TEST.OOP.BankAccount
{
    class CentralBank : Bank
    {
        public int _interestRate;
        CommertialBank[] _commertialBanks = new CommertialBank[0];
        

        public void addCommertialBank(CommertialBank commertialBank)

        {

                CommertialBank[] commertialBanksExtended = new CommertialBank [_commertialBanks.Length + 1];
                Array.Copy(_commertialBanks, commertialBanksExtended, _commertialBanks.Length);
                _commertialBanks = commertialBanksExtended;
                _commertialBanks[_commertialBanks.Length - 1] = commertialBank;
            
        }
        public void removeCommertialBank(CommertialBank commertialbank) 
        {
        }
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

