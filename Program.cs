using SimpleBankApp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

List<BankAccount> accounts = new List<BankAccount>();
BankAccount loggedInAccount = null!;

Menu menu = new Menu();

while (true)
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine("\n===== WELCOME TO BOLAK MICROFINANCE-BANK! =====");
    Console.WriteLine("1. Create Account");
    Console.WriteLine("2. Log In");
    Console.WriteLine("3. Exit");
    Console.ResetColor();

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
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"Error: {ex.Message}");
        Console.ResetColor();
    }
}
void CreateAccount()
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.Write("Enter fullname: ");
    string username = Console.ReadLine()!;

    Console.Write("Enter Phonenumber: ");
    string phoneNumber = Console.ReadLine()!;


    if (!string.IsNullOrWhiteSpace(phoneNumber))
    {
        ValidateContactPhoneNumber(phoneNumber);

    }


    Console.Write("Enter password: ");
    string password = ReadPassword();
    if (!string.IsNullOrWhiteSpace(password))
    {
        ValidatePassword(password);
    }
    Console.ResetColor();


    if (accounts.Any(acc => acc.Username == username))
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Account with this name already exists!.");
        Console.ResetColor();
    }
    else if (accounts.Any(acc => acc.PhoneNumber == phoneNumber))
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Account with this Phone number already exists.");
        Console.ResetColor();
    }
    else
    {
        BankAccount newAccount = new BankAccount(username, phoneNumber, password);
        accounts.Add(newAccount);

        loggedInAccount = new BankAccount(username, phoneNumber, password);
        Console.WriteLine($"Account created! Your account number is {loggedInAccount.AccountNumber}");
        Console.ReadKey();
        Console.WriteLine($"Account created successfully! A bonus of $200 has been credited to your balance.");
    }
}


void LogIn()
{
    Console.Write("Enter mobile number: ");
    string phoneNumber = Console.ReadLine()!;
    Console.Write("Enter password: ");
    string password = ReadPassword();

    loggedInAccount = accounts.FirstOrDefault(acc => acc.PhoneNumber == phoneNumber && acc.Password == password)!;

    if (loggedInAccount != null)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Login successful!");
        Console.WriteLine($"Welcome ");
        AccountMenu();
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Invalid username or password.");
        Console.ResetColor();
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
    }

}


string ReadPassword()
{
    char[] password = new char[0];
    int currentLength = 0;
    int maxLength = 6;

    while (currentLength < maxLength)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.Backspace)
        {
            if (currentLength > 0)
            {
                currentLength--;
                Console.Write("\b \b");
            }
        }

        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            break;
        }
        else
        {

            Array.Resize(ref password, currentLength + 1);
            password[currentLength] = keyInfo.KeyChar;
            currentLength++;


            Console.Write("*");
        }
    }

    Console.WriteLine(); 
    return new string(password);  
}


static void ValidateContactPhoneNumber(string phoneNumber)
{
    string phoneNumberPattern = @"^\d+$";

    if (!Regex.IsMatch(phoneNumber, phoneNumberPattern))
    {
        throw new Exception("Phone number cannot contain special character(s)");
    }

    if (phoneNumber?.Length < 11 || phoneNumber?.Length > 11)
    {
        throw new Exception("Phone number cannot be less or greater than 11 digits");
    }
}



static void ValidatePassword(string password)
{
    string passwordPattern = @"^\d+$";
    if(!Regex.IsMatch(password, passwordPattern))
    {
        throw new Exception("Password cannot contain special character(s)");
    }
    if(password?.Length < 6 || password?.Length > 6)
    {
        throw new Exception("Password must not be greater than or less than 6 digits");
    }
}

