using System;
using System.IO;
using PayslipRound2;
using Xunit;

namespace PayslipTests
{
    public class PayslipGeneratorTests
    {
        [Fact]
        public void GivenUserInformationShouldGeneratePayslip()
        {
            var csvParser = new CsvParser(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
            var userInformation = csvParser.GetUserInformation();
            
            var payslipGenerator = new PayslipGenerator();
            var payslip = payslipGenerator.GeneratePayslip(userInformation);
            
            
            Assert.Equal("David Rudd",payslip.Fullname);
            Assert.Equal("01 March - 31 March", payslip.PayPeriod);
            Assert.Equal(5004, payslip.GrossIncome);
            Assert.Equal(922, payslip.IncomeTax);
            Assert.Equal(4082, payslip.NetIncome);
            Assert.Equal(450, payslip.Super);
        }
    }
}