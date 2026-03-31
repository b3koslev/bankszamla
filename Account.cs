using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankszamla
{
    internal class Account
    {
        private string accountNumber;
        private string name;
        private decimal balance;
        private double creditLimit;

        public Account(string accountNumber, string name, decimal balance, decimal creditLimit)
        {
            this.accountNumber = accountNumber;
            this.name = name;
            this.balance = balance;
            this.creditLimit = 0;
        }

        public string GetAccountNumber()
        {
            return accountNumber;
        }

        public string GetName() 
        { 
            return name; 
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public double GetCreditLimit()
        {
            return creditLimit;
        }

        public bool Deposit(decimal amount)
        {
            balance += amount;
            return true;
        }

        public bool WithDraw(decimal amount, double creditLimit)
        {
            if (amount > balance + (decimal) creditLimit)
            {
                return false;
            }
            else
            {
                balance -= amount;
                return true;
            }
        }

        public bool Transfer(int amount, Account account, double creditLimit)
        {
            if (amount < balance + (decimal) creditLimit)
            {
                balance -= amount;
                account.Deposit(amount);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeCreditLimit(double creditLimit)
        {
            if (creditLimit < 0)
            {
                return false;
            }
            else
            {
                this.creditLimit = creditLimit;
                return true;
            }
        }

        public override string ToString()
        {
            return $"Számlaszám: {GetAccountNumber()} - Név: {GetName()} - Egyenleg: {GetBalance()} Ft - Hitelkeret összege: {GetCreditLimit()}";
        }
    }
}
