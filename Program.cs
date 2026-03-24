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
            ReadFile(fileName);
        }

        static void ReadFile(string fileName)
        {
            StreamReader file = new StreamReader(fileName, Encoding.UTF8);

            List<Account> accounts = new List<Account>();

            while (!file.EndOfStream)
            {
                string[] line = file.ReadLine().Split(';');
                accounts.Add(new Account(line[0], line[1], decimal.Parse(line[2])));
            }
        }
    }
}
