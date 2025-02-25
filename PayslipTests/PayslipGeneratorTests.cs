using System;
using Moq;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class PayslipGeneratorTests
    {
        readonly Mock<ICalculationsHandler> _calculator = new Mock<ICalculationsHandler>();

        [Theory]
        [InlineData("David", "Rudd", "David Rudd")]
        [InlineData("Ryan", "Chen", "Ryan Chen")]

        public void GivenFirstAndLastNameShouldFormatIntoFullName(string firstName, string lastName, string expectedName)
        {
            var user = Mock.Of<User>( u => u.Name == firstName && u.Surname == lastName && u.Salary == 0 && u.EndDate == "" && u.StartDate == "" && u.SuperRate == 0);
            var payslipGenerator = new PayslipCalculator(_calculator.Object, new DateValidator());
            var  payslip = payslipGenerator.GeneratePayslip(user);
            Assert.Equal(expectedName, payslip.Fullname);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(120000, 10000)]
        [InlineData(60050, 5004)]
        [InlineData(144, 12)]
        
        public void GivenAnnualSalaryShouldCalculateGrossIncome(int salary, int expectedGrossIncome)
        {
            var user = Mock.Of<User>( u => u.Name == "" && u.Surname == "" && u.Salary == salary && u.EndDate == "" && u.StartDate == "" && u.SuperRate == 0);
            _calculator.Setup(c => c.CalculateGrossIncome(It.IsAny<int>())).Returns(expectedGrossIncome);
            var payslipGenerator = new PayslipCalculator(_calculator.Object, new DateValidator());
            var  payslip = payslipGenerator.GeneratePayslip(user);
            Assert.Equal(expectedGrossIncome, payslip.GrossIncome);
        }
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(120000, 2669)]
        [InlineData(60050, 922)]
        [InlineData(144000, 3409)]
        [InlineData(35000, 266)]
        [InlineData(180003, 4519)]
        
        public void GivenSalaryShouldCalculateIncomeTax(int salary, int expectedTax)
        {
            var user = Mock.Of<User>( u => u.Name == "" && u.Surname == "" && u.Salary == salary && u.EndDate == "" && u.StartDate == "" && u.SuperRate == 0);
            _calculator.Setup(c => c.CalculateIncomeTax(It.IsAny<int>())).Returns(expectedTax);
            var payslipGenerator = new PayslipCalculator(_calculator.Object, new DateValidator());
            var payslip = payslipGenerator.GeneratePayslip(user);
            Assert.Equal(expectedTax, payslip.IncomeTax);
        }

        [Theory]
        [InlineData(180003, 10481)]
        [InlineData(60050, 4082)]
        [InlineData(144000, 8591)]
        [InlineData(35000, 2651)]

        public void GivenGrossSalaryAndIncomeTaxShouldCalculateNetIncome(int salary, int expectedNetIncome)
        {
            var user = Mock.Of<User>( u => u.Name == "" && u.Surname == "" && u.Salary == salary && u.EndDate == "" && u.StartDate == "" && u.SuperRate == It.IsAny<int>());
            _calculator.Setup(c => c.CalculateNetIncome(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedNetIncome);
            var payslipGenerator = new PayslipCalculator(_calculator.Object, new DateValidator());
            var payslip = payslipGenerator.GeneratePayslip(user);
            Assert.Equal(expectedNetIncome, payslip.NetIncome);
        }
        
        [Theory]
        [InlineData(180003, 9, 1350)]
        [InlineData(60050, 6, 300 )]
        [InlineData(144000, 2, 240)]
        [InlineData(35000, 10, 292)]
        
        public void GivenGrossIncomeAndSuperRateShouldCalculateSuper(int salary, int superRate, int expectedSuper)
        {
            var user = Mock.Of<User>( u => u.Name == "" && u.Surname == "" && u.Salary == salary && u.EndDate == "" && u.StartDate == "" && u.SuperRate == superRate);
            _calculator.Setup(c => c.CalculateSuper(It.IsAny<int>(), It.IsAny<int>())).Returns(expectedSuper);
            var payslipGenerator = new PayslipCalculator(_calculator.Object, new DateValidator());
            var payslip = payslipGenerator.GeneratePayslip(user);
            Assert.Equal(expectedSuper, payslip.Super);
        }


        [Fact]
        public void GivenExceptionThrownShouldCreateEmptyPayslip()
        {
            var user = Mock.Of<User>( u => u.Name == "" && u.Surname == "" && u.Salary == -1 && u.EndDate == "" && u.StartDate == "" && u.SuperRate == 0);
            _calculator.Setup(c => c.CalculateIncomeTax(It.IsAny<int>())).Throws<Exception>();
            var payslipGenerator = new PayslipCalculator(_calculator.Object, new DateValidator());
            var payslip = payslipGenerator.GeneratePayslip(user);
            Assert.Null(payslip.Fullname);
            Assert.Equal(0, payslip.GrossIncome);
            Assert.Equal(0, payslip.IncomeTax);
            Assert.Equal(0, payslip.NetIncome);
            Assert.Equal(0, payslip.Super);
        }

    }
}