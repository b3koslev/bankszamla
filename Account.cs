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
        private int creditLimit;

        public Account(string accountNumber, string name, decimal balance)
        {
            this.accountNumber = accountNumber;
            this.name = name;
            this.balance = balance;
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

        public int GetCreditLimit()
        {
            return creditLimit;
        }

        public override string ToString()
        {
            return $"{GetAccountNumber()} - {GetName()} - Egyenleg: {GetBalance()} Ft";
        }
    }
}
