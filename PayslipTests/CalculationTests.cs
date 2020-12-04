using System;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class CalculationTests
    {
        
        [Theory]
        [InlineData(0, 0)]
        [InlineData(120000, 10000)]
        [InlineData(60050, 5004)]
        [InlineData(144, 12)]
        
        public void GivenAnnualSalaryShouldCalculateGrossIncome(int salary, int expectedGrossIncome)
        {
            var calculationsHandler = new MonthlyCalculationsHandler();
            var grossIncome = calculationsHandler.CalculateGrossIncome(salary);
            Assert.Equal(expectedGrossIncome, grossIncome);
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
            var calculationsHandler = new MonthlyCalculationsHandler();
            var incomeTax = calculationsHandler.CalculateIncomeTax(salary);
            Assert.Equal(expectedTax, incomeTax);
        }

        [Theory]
        [InlineData(180003, 10481)]
        [InlineData(60050, 4082)]
        [InlineData(144000, 8591)]
        [InlineData(35000, 2651)]

        public void GivenGrossSalaryAndIncomeTaxShouldCalculateNetIncome(int salary, int expectedNetIncome)
        {
            var calculationsHandler = new MonthlyCalculationsHandler();
            var grossSalary = calculationsHandler.CalculateGrossIncome(salary);
            var incomeTax = calculationsHandler.CalculateIncomeTax(salary);
            var netIncome = calculationsHandler.CalculateNetIncome(grossSalary, incomeTax);
            Assert.Equal(expectedNetIncome, netIncome);
        }
        
        [Theory]
        [InlineData(180003, 9, 1350)]
        [InlineData(60050, 6, 300 )]
        [InlineData(144000, 2, 240)]
        [InlineData(35000, 10, 292)]
        
        public void GivenGrossIncomeAndSuperRateShouldCalculateSuper(int salary, int superRate, int expectedSuper)
        {
            var calculationsHandler = new MonthlyCalculationsHandler();
            var grossSalary = calculationsHandler.CalculateGrossIncome(salary);

            var super = calculationsHandler.CalculateSuper(grossSalary, superRate);
            Assert.Equal(expectedSuper, super);
        }

        [Fact]
        public void GivenInvalidSalaryShouldThrowException()
        {
            var calculationsHandler = new MonthlyCalculationsHandler();
            Assert.Throws<Exception>(() => calculationsHandler.CalculateIncomeTax(-1));
        }
    }
}