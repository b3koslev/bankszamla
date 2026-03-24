using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bankszamla
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "szamlak.txt";
            List<Account> accounts = ReadFile(fileName);
            BankDashboard(accounts);
        }

        static List<Account> ReadFile(string fileName)
        {
            StreamReader file = new StreamReader(fileName, Encoding.UTF8);

            List<Account> accounts = new List<Account>();

            while (!file.EndOfStream)
            {
                string[] line = file.ReadLine().Split(';');
                accounts.Add(new Account(line[0], line[1], decimal.Parse(line[2])));
            }

            return accounts;
        }

        static void CreateMenu()
        {
            Console.WriteLine("==== Bankszámla ====");
            Console.WriteLine("1. Számlák megtekintése");
            Console.WriteLine("2. Befizetés");
            Console.WriteLine("3. Kifizetés");
            Console.WriteLine("4. Utalás");
            Console.WriteLine("5. Kilépés");
            Console.WriteLine();
            Console.Write("Választott menüpont: ");
        }

        static void BankDashboard(List<Account> accounts)
        {
            CreateMenu();
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewAccounts(accounts);
                    break;
            }
        }

        static void ViewAccounts(List<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                Console.WriteLine(account.ToString());
            }
        }
    }
}
