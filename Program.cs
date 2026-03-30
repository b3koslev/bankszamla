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
            List<Account> accounts = new List<Account>();
            List<string> logs = new List<string>();

            string fileName = "szamlak.txt";
            ReadFile(fileName, accounts);
            BankDashboard(accounts);
        }

        static void ReadFile(string fileName, List<Account> accounts)
        {
            StreamReader file = new StreamReader(fileName, Encoding.UTF8);


            while (!file.EndOfStream)
            {
                string[] line = file.ReadLine().Split(';');
                accounts.Add(new Account(line[0], line[1], decimal.Parse(line[2]), 0));
            }
        }

        static void CreateMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==== Bankszámla ====");
            Console.ResetColor();
            Console.WriteLine("1. Számlák megtekintése");
            Console.WriteLine("2. Befizetés");
            Console.WriteLine("3. Kifizetés");
            Console.WriteLine("4. Utalás");
            Console.WriteLine("5. Hitelkeret módosítása");
            Console.WriteLine("6. Kilépés");
            Console.WriteLine();
            Console.Write("Választott menüpont: ");
        }

        static void BankDashboard(List<Account> accounts, List<string> logs)
        {
            CreateMenu();
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewAccounts(accounts, logs);
                    break;
                case 2:
                    Deposit(accounts, logs);
                    break;
            }
        }

        static void ViewAccounts(List<Account> accounts, List<string> logs)
        {
            foreach (Account account in accounts)
            {
                Console.WriteLine(account.ToString());
            }
        }

        static void Deposit(List<Account> accounts, List<string> logs)
        {
            Console.Write("Adja meg, hogy melyik számlára szeretne pénzt feltölteni: ");
            string accountNumber = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Adja meg a befizetés összegét: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            DateTime date = DateTime.Now;

            foreach (Account account in accounts)
            {
                if (account.GetAccountNumber() == accountNumber)
                {
                    if (account.Deposit(amount))
                    {
                        Console.WriteLine("A befizetés sikeres!");
                        logs.Add($"{account.GetAccountNumber()};{date};Befizetés;{account.GetBalance()}");
                    }
                    else
                    {
                        Console.WriteLine("A befizetés sikertelen!");
                    }
                }
            }
        }

        static void WithDraw(List<Account> accounts, List<string> logs)
        {
            Console.Write("Adja meg, hogy melyik számlára szeretne pénzt feltölteni: ");
            string accountNumber = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Adja meg a befizetés összegét: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            DateTime date = DateTime.Now;

            foreach (Account account in accounts)
            {
                if (account.GetAccountNumber() == accountNumber)
                {
                    if (account.WithDraw(amount))
                    {
                        Console.WriteLine("A kifizetés sikeres!");
                        logs.Add($"{account.GetAccountNumber()};{date};Kifizetés;{account.GetBalance()}");
                    }
                    else
                    {
                        Console.WriteLine("A kifizetés sikertelen!");
                    }
                }
            }
        }
    }
}
