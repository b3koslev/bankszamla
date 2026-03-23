using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankszamla
{
    internal class Account
    {
        private int accountNumber;
        private string name;
        private decimal balance;
        private int creditLimit;

        public Account(int accountNumber, string name, decimal balance)
        {
            this.accountNumber = accountNumber;
            this.name = name;
            this.balance = balance;
        }
    }
}
