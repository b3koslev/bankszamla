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
            Dictionary<string, int> startBalances = ReadFile(fileName, accounts);
            BankDashboard(accounts, logs, startBalances);
        }

        static Dictionary<string, int> ReadFile(string fileName, List<Account> accounts)
        {
            StreamReader file = new StreamReader(fileName, Encoding.UTF8);

            Dictionary<string, int> startBalances = new Dictionary<string, int>();

            while (!file.EndOfStream)
            {
                string[] line = file.ReadLine().Split(';');
                startBalances.Add(line[0], int.Parse(line[2]));
                accounts.Add(new Account(line[0], line[1], decimal.Parse(line[2]), 0));
            }

            return startBalances;
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

        static void BankDashboard(List<Account> accounts, List<string> logs, Dictionary<string, int> startBalances)
        {
            int choice = 0;
            while (choice != 6)
            {
                CreateMenu();
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ViewAccounts(accounts, logs);
                        Console.WriteLine("Nyomjon meg egy gombot a kilépéshez...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Deposit(accounts, logs);
                        Console.WriteLine("Nyomjon meg egy gombot a kilépéshez...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        WithDraw(accounts, logs);
                        Console.WriteLine("Nyomjon meg egy gombot a kilépéshez...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Transfer(accounts, logs);
                        Console.WriteLine("Nyomjon meg egy gombot a kilépéshez...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        ChangeCreditLimit(accounts, logs, startBalances);
                        Console.WriteLine("Nyomjon meg egy gombot a kilépéshez...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 6:
                        FileWrite(accounts, logs);
                        break;
                }
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
            Console.Write("Adja meg, hogy melyik számláról szeretne pénzt kivenni: ");
            string accountNumber = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Adja meg a kifizetés összegét: ");
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

        static void Transfer(List<Account> accounts, List<string> logs) 
        {
            Console.Write("Adja meg, hogy melyik számláról szeretne utalni: ");
            string accountNumber1 = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Adja meg, hogy melyik számlára szeretne pénzt kivenni: ");
            string accountNumber2 = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Adja meg az utalás összegét: ");
            int amount = int.Parse(Console.ReadLine());
            Console.WriteLine();

            foreach (Account account1 in accounts)
            {
                if (account1.GetAccountNumber() == accountNumber1)
                {
                    foreach (Account account2 in accounts)
                    {
                        if (account2.GetAccountNumber() == accountNumber2)
                        {
                            account1.Transfer(amount, account2);
                        }
                    }
                }
            }
        }

        static void ChangeCreditLimit(List<Account> accounts, List<string> logs, Dictionary<string, int> startBalances)
        {
            Console.Write("Adja meg, hogy melyik számlához szeretne hitelkeretet módosítani: ");
            string accountNumber = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Adja meg a módosított hitelkeret összegét: ");
            double amount = double.Parse(Console.ReadLine());
            Console.WriteLine();

            DateTime date = DateTime.Now;

            foreach (Account account in accounts)
            {
                if (account.GetAccountNumber() == accountNumber)
                {
                    foreach (string startBalance in startBalances.Keys)
                    {
                        if (startBalance == account.GetAccountNumber())
                        {
                            if (amount < startBalances[startBalance] * 0.2)
                            {
                                account.ChangeCreditLimit(amount);
                                Console.WriteLine("A hitelkeret módosítása sikeres!");
                                logs.Add($"{account.GetAccountNumber()};{date};Hitelkeret módosítása;{account.GetBalance()}");
                            }
                            else
                            {
                                Console.WriteLine("A hitelkeret módosítása sikertelen!");
                            }
                        }
                    }
                }
            }
        }

        static void FileWrite(List<Account> accounts, List<string> logs)
        {
            foreach (Account account in accounts)
            {
                StreamWriter file = new StreamWriter($"{account.GetAccountNumber()}.txt", false, Encoding.UTF8);

                file.WriteLine("Számlaszám;Időpont;Művelet;Egyenleg");

                foreach (string log in logs)
                {
                    if (log.Contains(account.GetAccountNumber()))
                    {
                        file.WriteLine(log);
                    }
                }
                file.Close();
            }
        }
    }
}
