using System;

namespace Payslip
{
    public class ConsoleWriter
    {
        public void WritePayslipInformation(Payslip payslip)
        {
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("______________________________");
            Console.ResetColor();
            
        }
    }
}