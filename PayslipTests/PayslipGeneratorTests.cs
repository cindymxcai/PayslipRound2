using System;
using System.IO;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class PayslipGeneratorTests
    {
        [Fact]
        public void GivenUserInformationShouldGeneratePayslip()
        {
            var fileReader = new FileReader(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
            var csvParser = new CsvParser(fileReader);

            var payslipGenerator = new PayslipGenerator();

            var payslipHandler = new PayslipsHandler(csvParser, payslipGenerator);
            var payslip = payslipHandler.CreateAllPayslips()[0];
            
            
            Assert.Equal("David Rudd",payslip.Fullname);
            Assert.Equal("01 March - 31 March", payslip.PayPeriod);
            Assert.Equal(5004, payslip.GrossIncome);
            Assert.Equal(922, payslip.IncomeTax);
            Assert.Equal(4082, payslip.NetIncome);
            Assert.Equal(450, payslip.Super);
        }
    }
}