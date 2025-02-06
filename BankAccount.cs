using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankApp
{
    public class BankAccount : IBankAccount
    {
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public float Balance { get; set; }

        public BankAccount(string username,string phoneNumber, string password)
        {
            Username = username;
            PhoneNumber = phoneNumber;
            //Random random = new Random();
            AccountNumber = phoneNumber.Trim();
            Password = password;
            Balance = 200.0f;
        }

        public void Deposit(float amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be greater than zero.");
            }
            Balance += amount;
        }

        public void Withdraw(float amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            }
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }
        }

        public void Transfer(BankAccount receiver, float amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Transfer amount must be greater than zero.");
            }
            if (Balance >= amount)
            {
                Withdraw(amount);
                receiver.Deposit(amount);
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for transfer.");
            }
        }
    }

}