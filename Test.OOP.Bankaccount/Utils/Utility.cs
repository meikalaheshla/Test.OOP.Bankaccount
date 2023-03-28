using System;

namespace TEST.OOP.BankAccount.Utils
{
    static class Utility
    {
        public static void GetAccountInfo(ConsoleColor consoleColor, CommertialBank bank, bool isDeposit, FIATDespositRequest data)
        {
            Console.WriteLine($"Account Number: {bank.account.AccountNumber}");
            Console.WriteLine($"Account Client: {bank.account._client.Name}");
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"Amount {(isDeposit ? "Deposited" : "Withdrawn")}: {data._amount}");
            Console.ResetColor();
            // Console.WriteLine($"Account Balance: {bank.account.Balance}");

            Console.WriteLine("-------------------------------------");
        }
    }

}

