using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using TEST.OOP.BankAccount;


namespace TEST.OOP.BankAccount
{
    internal class Program
    {

        static void Main(string[] args)
        {



            CentralBank RussianCentralBank = new CentralBank("Russia Central Bank", "Russia", 5);
            CentralBank BancaDitalia = new SwiftCentralBank("Banca D'Italia", "Italia", 3);


            CommertialBank SberBank = new CommertialBank("SberBank", "Russia", RussianCentralBank);
            CommertialBank Unicredit = new CommertialBank("Unicredit", "Italy", BancaDitalia);

            // Crea un conto Corrente e deposita dentro un dei soldi 
            SberBank.CreateAccount("Vladimir Putin", "NO DATA");
            SberBank.DepositFiat(100000M);
            SberBank.DepositCrypto(4);
            SberBank.InvestInStock(1);

            // Crea un conto Corrente con saldo zero
            Unicredit.CreateAccount("Bruno Ferreira", "FRBBRIIM394NFNNF");


            // Stampa Saldo iniziale dei due conti  
            Console.WriteLine("-------------------------------------- SALDO INIZIALE -------------------");

            Console.WriteLine($" L'account di Vladimir Putin ha un credito di :  {SberBank.account.Balance}");
            Console.WriteLine($" L'account di Bruno Ferreira ha un credito di :  {Unicredit.account.Balance}");
            Console.WriteLine("-------------------------------------------------------------------------------");


            bool result = SberBank.Transfer(Unicredit, new FIATDespositRequest() { _amount = 1000M, _accountfrom = 5548485187, _accountTo = 1112355477 });

            if (!result)
            {
                Console.WriteLine("Amount not Transfered! ");
                return;// Fine Programma! 
            }


            // Stampa Saldo Fianale dei due conti  
            Console.WriteLine("-------------------------------------- SALDO FINALE -------------------");

            Console.WriteLine($" L'account di Vladimir Putin ha un credito di :  {SberBank.account.Balance}");
            Console.WriteLine($" L'account di Bruno Ferreira ha un credito di :  {Unicredit.account.Balance}");
            Console.WriteLine("-------------------------------------------------------------------------------");


            Console.ResetColor();

            // Show After before Transfer  

            Console.ReadLine();


        }
    }

}

