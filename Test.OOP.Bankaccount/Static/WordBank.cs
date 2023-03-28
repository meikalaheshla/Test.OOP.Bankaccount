using System;
using TEST.OOP.BankAccount.Interfaces;

namespace TEST.OOP.BankAccount.Static
{
    static class WordBank
    {
        public static bool Transfer(CommertialBank from, CommertialBank to, FIATDespositRequest data)
        {
            if (from.CentralBank is ISwiftSystem && to.CentralBank is ISwiftSystem)
            {
                return true;
            }
            else
            {
                if (from.CentralBank is not ISwiftSystem)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"The Source bank {from.Name}  from {from.Country} is not in the Swift System. " +
                        $"It's prpbably under Sanction! ");
                    Console.ResetColor();
                }
                if (to.CentralBank is not ISwiftSystem)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"The destination bank {to.Name}  from {to.Country} is not in the Swift System. " +
                        $"It's prpbably under Sanction! ");
                    Console.ResetColor();

                }
                return false;
            }
        }

    }

}

