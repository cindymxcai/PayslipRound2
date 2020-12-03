using System.IO;
using Payslip.Interfaces;

namespace Payslip
{
    public class FileWriter : IFileWriter
    {
        private readonly string _outputPath;

        public FileWriter(string outputPath)
        {
            _outputPath = outputPath;
            File.Delete(outputPath);
        }
        public void WritePayslipInformation(Payslip payslip)
        {
            using var file = File.AppendText(_outputPath);
            var newPayslipRow = $"{payslip.Fullname}, {payslip.PayPeriod}, {payslip.GrossIncome}, {payslip.IncomeTax}, {payslip.NetIncome}, {payslip.Super}";
            file.WriteLine(newPayslipRow);
        }
    }
}