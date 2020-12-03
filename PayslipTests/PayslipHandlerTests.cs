using Moq;
using Payslip;
using Payslip.Interfaces;
using Xunit;
using slip = Payslip.Payslip;

namespace PayslipTests
{
    public class PayslipHandlerTests
    {
        [Fact]
        public void GivenUserInformationShouldGeneratePayslip()
        {
            var csvParser = new Mock<IInputParser>();
            csvParser.SetupSequence(p => p.GetNextUserInputInformation())
                .Returns(new User {Name = "David", Surname = "Rudd", StartDate = "01 March", EndDate = "31 March", Salary = 60050, SuperRate = 9})
                .Returns(new User {Name = "Ryan", Surname = "Chen", StartDate = "01 March", EndDate = "31 March", Salary = 120000, SuperRate = 10});
            

            var payslipGenerator = new Mock<PayslipGenerator>();
            payslipGenerator.SetupSequence(p=>p.GeneratePayslip(It.IsAny<User>()))
                .Returns(new slip{Fullname = "David Rudd", PayPeriod = "01 March - 31 March", GrossIncome = 5004, IncomeTax = 922, NetIncome = 4082, Super = 450})
                .Returns(new slip{Fullname = "Ryan Chen", PayPeriod = "01 March - 31 March", GrossIncome = 10000, IncomeTax = 2669, NetIncome = 7331, Super = 1000});

            var payslipHandler = new PayslipsHandler(csvParser.Object, payslipGenerator.Object);
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