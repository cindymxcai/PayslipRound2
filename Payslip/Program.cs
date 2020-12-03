using System;
using System.IO;

namespace PayslipRound2
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvParser = new CsvParser(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
            var userInformation = csvParser.GetUserInformation();

            Console.WriteLine($"Hello {userInformation.Name} {userInformation.Surname}!");
        }
    }
}