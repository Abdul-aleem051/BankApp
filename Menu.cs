using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankApp
{

    public sealed class Menu
    {
        public void ShowMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n====> MENU:");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Log Out");
            Console.ResetColor();
        }

        public void ShowBalance(BankAccount account)
        {
            Console.WriteLine($"Your current balance is: ${account.Balance}");
        }

        public void HandleDeposit(BankAccount account)
        {
            try
            {
                Console.Write("Enter amount to deposit: $");
                float amount = float.Parse(Console.ReadLine()!);
                account.Deposit(amount);
                Console.WriteLine($"Deposited ${amount}. Your new balance is: ${account.Balance}");
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void HandleWithdraw(BankAccount account)
        {
            try
            {
                Console.Write("Enter amount to withdraw: $");
                float amount = float.Parse(Console.ReadLine()!);
                account.Withdraw(amount);
                Console.WriteLine($"Withdrawn ${amount}. Your new balance is: ${account.Balance}");
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void HandleTransfer(BankAccount sender, List<BankAccount> accounts)
        {
            try
            {
                Console.Write("Enter the recipient username: ");
                string recipientAccountNumber = Console.ReadLine()!;


                BankAccount receiver = null!;
                foreach (var account in accounts)
                {
                    if (account.Username == recipientAccountNumber)
                    {
                        receiver = account;
                        break;
                    }
                }

                if (receiver == null)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Recipient not found.");
                    Console.ResetColor();
                    return;
                }

                Console.Write("Enter amount to transfer: $");
                if (!float.TryParse(Console.ReadLine(), out float amount) || amount <= 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Please enter a valid positive amount.");
                    Console.ResetColor();
                    return;
                }

                if (sender.Balance < amount)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Insuufficient funds for the transfer!!.");
                    Console.ResetColor();
                    return;
                }

                sender.Transfer(receiver, amount);
                Console.WriteLine($"Successfully transferred ${amount} to {recipientAccountNumber}. Your new balance is: ${sender.Balance}");
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

}