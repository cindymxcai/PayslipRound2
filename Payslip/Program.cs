using System;
using System.IO;

namespace Payslip
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var fileReader = new FileReader();
            var csvParser = new CsvParser(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"), fileReader);
            var payslipGenerator = new PayslipGenerator(csvParser);
            var payslip = payslipGenerator.GeneratePayslip();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Hello {payslip.Fullname}! Here is your payslip: ");
            Console.ResetColor();
         
            
            Console.WriteLine($"For Pay Period {payslip.PayPeriod}");
            Console.WriteLine($"Gross Income: {payslip.GrossIncome}");
            Console.WriteLine($"Income Tax: {payslip.IncomeTax}");
            Console.WriteLine($"Net Income: {payslip.NetIncome}");
            Console.WriteLine($"Super: {payslip.Super}");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Thank you for using MYOB!");
        }
    }
}