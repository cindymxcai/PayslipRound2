using System;
using System.IO;

namespace Payslip
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var fileReader = new FileReader(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
             var fileWriter = new FileWriter(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/output.csv"));

             var consoleWriter = new ConsoleWriter();
             var csvParser = new CsvParser(fileReader);
            var payslipGenerator = new PayslipGenerator();
            var payslipHandler = new PayslipsHandler(csvParser, payslipGenerator);
            var payslips = payslipHandler.CreateAllPayslips();

            foreach (var payslip in payslips)
            {
                fileWriter.WritePayslipInformation(payslip);
                consoleWriter.WritePayslipInformation(payslip);
            }
        }
    }
}