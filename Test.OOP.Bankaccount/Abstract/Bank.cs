namespace TEST.OOP.BankAccount.Abstract
{
    abstract class Bank
    {
        protected long _code; //> -> BANK
        protected string _name;
        protected string _country;


        public string Name { get => _name; }
        public string Country { get => _country; }

        public Bank(string name, string Country)
        {
            _name = name;
            _country = Country;

        }

        public virtual bool Transfer(Bank to, FIATDespositRequest data)
        {
            return false;
        }
    }

}

