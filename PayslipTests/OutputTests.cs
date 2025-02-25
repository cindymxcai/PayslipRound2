using System;
using System.IO;
using Payslip;
using Xunit;
using slip = Payslip.Payslip;

namespace PayslipTests
{
    public class OutputTests
    {
        [Fact]
        public void GivenTextShouldWriteToCsvFile()
        {
            var fileWriter = new FileWriter(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/output.csv"));
            var payslip = new slip {Fullname = "David Rudd"};
            fileWriter.WritePayslipInformation(payslip);
            Assert.Contains("David Rudd",  File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/output.csv")));
        }
    }
}