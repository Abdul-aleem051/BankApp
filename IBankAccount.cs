using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankApp
{
    public interface IBankAccount
    {
        void Deposit(float amount);
        void Withdraw(float amount);
        void Transfer(BankAccount receiver, float amount);
    }
}