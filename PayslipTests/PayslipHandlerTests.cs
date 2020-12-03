using System;
using System.IO;
using System.Linq;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class PayslipHandlerTests
    {
        [Fact]
        public void GivenUserInformationShouldGeneratePayslip()
        {
            var fileReader = new FileReader(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
            var csvParser = new CsvParser(fileReader);

            var payslipGenerator = new PayslipGenerator();

            var payslipHandler = new PayslipsHandler(csvParser, payslipGenerator);
            var payslips = payslipHandler.CreateAllPayslips();
            var davidPayslip = payslips[0];
            
            
            Assert.Equal("David Rudd",davidPayslip.Fullname);
            Assert.Equal("01 March - 31 March", davidPayslip.PayPeriod);
            Assert.Equal(5004, davidPayslip.GrossIncome);
            Assert.Equal(922, davidPayslip.IncomeTax);
            Assert.Equal(4082, davidPayslip.NetIncome);
            Assert.Equal(450, davidPayslip.Super);

            var ryanPayslip = payslips[1];
            
            Assert.Equal("Ryan Chen",ryanPayslip.Fullname);
            Assert.Equal("01 March - 31 March", ryanPayslip.PayPeriod);
            Assert.Equal(10000, ryanPayslip.GrossIncome);
            Assert.Equal(2669, ryanPayslip.IncomeTax);
            Assert.Equal(7331, ryanPayslip.NetIncome);
            Assert.Equal(1000, ryanPayslip.Super);
        }
    }
}