using System;
using System.Reflection;
using TEST.OOP.BankAccount.Abstract;
using TEST.OOP.BankAccount.Utils;

namespace TEST.OOP.BankAccount
{
    class CommertialBank : Bank
    {
        private CentralBank _centralBank;
        Account _account;
       //sostistuisco il singolo account con un array di account 
        Account[] _accounts = new Account[0];
      

        public Account account { get => account; }
        public CentralBank CentralBank { get => _centralBank; }
        public CommertialBank(string Name, string Country, CentralBank Bank) : base(Name, Country)
        {
            _centralBank = Bank;
            _country = Country;
            _name = Name;
            _code = new Random().Next(10000, 1000000);

        }
        public void addAccount(Account account)
        {

                Account[] accountsExtended = new Account[_accounts.Length + 1];
                Array.Copy(_accounts, accountsExtended, _accounts.Length);
                _accounts = accountsExtended;
                _accounts[_accounts.Length - 1] = account;
            
        }
                public void CreateAccount(string ClientName, string ClientCF)
        {
            var account = new Account(ClientName, ClientCF, this);
            
            
        }
        // FUNZIONALITà REMOVE SENZA RESIZE
        public void removeAccount(long accountNumber) 
        {
            var account = Array.Find(_accounts, account => account.AccountNumber == accountNumber);
            var index = Array.IndexOf(_accounts,account);
            Account[] accountsReduced = new Account[_accounts.Length - 1];

            for (int i = 0, j = 0; i < _accounts.Length; i++)
            {
                if (_accounts[i] != _accounts[index])
                {
                    accountsReduced[j] = _accounts[i];
                    j++;
                }
            }
            Array.Copy(_accounts, accountsReduced, _accounts.Length);
            _accounts = accountsReduced;
        }
        public override bool Transfer(Bank to, FIATDespositRequest data)
        {

            // CommertialBank transferFrom = (CommertialBank) from;
            CommertialBank transferTo = (CommertialBank)to;

            if (this._centralBank.CheckTransfer(this, transferTo, data))
            {
                /*  
                   Prima di procedere con il versamento, verificare che l'ammontare da accreditare è stato effettivamente scalato dal conto del versatore
                   Quindi  avere una copia dello stato del conto prima di scalere i soldi per  poter confrontare che il prelievo è andato a buon fine.

                 */


                // stato conto prima
                
                this._account.WithdrawFIAT(data._amount);
                // confronto le due cifr dopo il prelievo. 
                Utility.GetAccountInfo(ConsoleColor.Red, this, false, data);

                transferTo._account.DepositFIAT(data._amount);
                Utility.GetAccountInfo(ConsoleColor.Green, transferTo, true, data);


                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"The  amount {data._amount} from the account {data._accountfrom} from the Bank {this.Name} to " +
                    $"account {data._accountTo} of from the Bank {to.Name} has been made! ");
                Console.ResetColor();

                return true;
            }
            return false;

        }
                
         //DA GESTIRE PER UNO SPECIFICO ACCOUNT NELL'ARRAY
         
        public void DepositFiat(decimal Amount)
        {
            // Check Client // è biondo! 
           _account.DepositFIAT(Amount);
        }
        public void DepositCrypto(decimal Amount)
        {
            _account.DepositCrypto(Amount);
        }
        public void InvestInStock(decimal Amount)
        {
            _account.InvestInStoks(Amount);
        }
        public void WithdrawFIAT(decimal Amount)
        {
            _account.WithdrawFIAT(Amount);
        }
        public void WithdrawCrypto(decimal Amount)
        {
            this._account.WithdrawCrypto(Amount);
        }
        public void SellStoks(decimal Amount)
        {
            this._account.SellStoks(Amount);
        }
        public class Account
        {
            CommertialBank _commertialBank;
            public Client _client;
            public long AccountNumber { get; }
            Fiat _fiat;
            Crypto _crypto;
            Stock _stocks;
            decimal _interests;
            //decimal _balance;


            // public decimal Amount { get { return _fiat.AmountInEuro + _crypto.AmountInEuro + _stocks.AmountInEuro; } }
            public decimal Balance { get { return CalcAmount() + calcInterests(); } }
            

            public Account(string ClientName, string ClientCF, CommertialBank commertialBank)
            {
                _commertialBank = commertialBank;
                _client = new Client(ClientName, ClientCF, this);
                AccountNumber = new Random().Next(10000, 1000000);
                _fiat = new Fiat(this);
                _crypto = new Crypto(this);
                _stocks = new Stock(this);
            }
            decimal calcInterests()
            {
                return _interests = (CalcAmount() / 100) * _commertialBank.CentralBank._interestRate;
            }
            decimal CalcAmount()
            {
                return _fiat.AmountInEuro + _crypto.AmountInEuro + _stocks.AmountInEuro;
            }
            public void DepositFIAT(decimal Amount)
            {
                this._fiat.EuroAmount += Amount;
            }
            public void DepositCrypto(decimal Amount)
            {
                this._crypto.CryptoAmount += Amount;
            }
            public void InvestInStoks(decimal Amount)
            {
                this._stocks.StockAmount += Amount;
            }
            public void WithdrawFIAT(decimal Amount)
            {
                this._fiat.EuroAmount -= Amount;
            }
            public void WithdrawCrypto(decimal Amount)
            {
                this._crypto.CryptoAmount -= Amount;
            }
            public void SellStoks(decimal Amount)
            {
                this._stocks.StockAmount -= Amount;
            }

            public class Client
            {
                string _name;
                string _cf;
                Account _account;
                long _clientId;
                public Client(string ClientName, string ClientCF, Account account)
                {
                    _cf = ClientCF;
                    Name = ClientName;
                    _account = account;
                    _clientId = new Random().Next(10000, 100000);
                }

                public string Name { get => _name; set => _name = value; }
                public long ClientId { get => _clientId; }
            }
            public abstract class Asset
            {
                Account _account;
                public abstract decimal AmountInEuro { get; }
                public Asset(Account Account)
                {
                    _account = Account;
                }

            }
            public class Fiat : Asset
            {
                public decimal EuroAmount;
                public decimal GbpAmount;


                decimal _euroPrice = 1;
                decimal _gbpPrice = 0.89M;
                public override decimal AmountInEuro { get => EuroAmount + (GbpAmount * _gbpPrice); } // Converti in EURO. Per esempio, se ho altre FIAT come Dollari, Yen , Sterline 
                // public decimal FiatAmount { get => _fiatAmount; set => _fiatAmount = value; }
                public Fiat(Account Account) : base(Account)
                {

                }
            }
            public class Crypto : Asset
            {
                decimal _cryptoAmount;
                decimal _cryptoPrice = 28000;
                public override decimal AmountInEuro { get => _cryptoAmount * _cryptoPrice; }
                public decimal CryptoAmount { get => _cryptoAmount; set => _cryptoAmount = value; }

                public Crypto(Account Account) : base(Account)
                {

                }
            }
            public class Stock : Asset
            {
                decimal _stockAmount;
                decimal _stockPrice = 500;
                public override decimal AmountInEuro { get => _stockAmount * _stockPrice; }
                public decimal StockAmount { get => _stockAmount; set => _stockAmount = value; }
                public Stock(Account Account) : base(Account)
                {

                }
            }
        }
    }



}

