using System;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class DateValidatorTests
    {
        [Fact]
        public void GivenValidDateShouldFormatDate()
        {
            var dateValidator = new DateValidator();
            var formattedDate = dateValidator.FormatDate("01, March", "31, March");
            Assert.Equal("01 March - 31 March", formattedDate);
        }
        
        [Theory]
        [InlineData("40, March", "31, March")]
        [InlineData("-1, March", "32, March")]
        [InlineData("16, Jan", "31, March")]
        [InlineData("40, January", "30, February")]

        public void GivenInValidDateShouldThrowException(string startDate, string endDate)
        {
            var dateValidator = new DateValidator();
            Assert.Throws<Exception>(() => dateValidator.FormatDate(startDate, endDate));

        }
    }
}