using SimpleBankApp;
using System;
using System.Collections.Generic;
using System.Linq;

List<BankAccount> accounts = new List<BankAccount>();
BankAccount loggedInAccount = null!;

Menu menu = new Menu();

while (true)
{
    Console.WriteLine("\n===== WELCOME TO BOLAK MICROFINANCE-BANK! =====");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. Log In");
    Console.WriteLine("3. Exit");

    try
    {
        int choice = int.Parse(Console.ReadLine()!);

        switch (choice)
        {
            case 1:
                CreateAccount();
                break;
            case 2:
                LogIn();
                break;
            case 3:
                Console.WriteLine("Exiting...");
                return;
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}


void CreateAccount()
{
    Console.Write("Enter username: ");
    string username = Console.ReadLine()!;
    Console.Write("Enter password: ");
    string password = Console.ReadLine()!;

    if (accounts.Any(acc => acc.Username == username))
    {
        Console.WriteLine("Account with that username already exists.");
    }
    else
    {
        BankAccount newAccount = new BankAccount(username, password);
        accounts.Add(newAccount);

        loggedInAccount = new BankAccount(username, password);
        Console.WriteLine($"Account created! Your account number is {loggedInAccount.AccountNumber}");
        Console.ReadKey();
        Console.WriteLine($"Account created successfully! A bonus of $200 has been credited to your balance.");
    }
}

void LogIn()
{
    Console.Write("Enter username: ");
    string username = Console.ReadLine()!;
    Console.Write("Enter password: ");
    string password = Console.ReadLine()!;

    loggedInAccount = accounts.FirstOrDefault(acc => acc.Username == username && acc.Password == password)!;

    if (loggedInAccount != null)
    {
        Console.WriteLine("Login successful!");
        AccountMenu();
    }
    else
    {
        Console.WriteLine("Invalid username or password.");
    }
}

void AccountMenu()
{
    Menu menu = new Menu();

    while (true)
    {
        menu.ShowMenu();
        try
        {
            int choice = int.Parse(Console.ReadLine()!);

            switch (choice)
            {
                case 1:
                    menu.ShowBalance(loggedInAccount!);
                    break;
                case 2:
                    menu.HandleDeposit(loggedInAccount!);
                    break;
                case 3:
                    menu.HandleWithdraw(loggedInAccount!);
                    break;
                case 4:
                    menu.HandleTransfer(loggedInAccount!, accounts);
                    break;
                case 5:
                    Console.WriteLine("Logging out...");
                    loggedInAccount = null!;
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

