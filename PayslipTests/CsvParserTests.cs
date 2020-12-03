using System;
using System.IO;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class CsvParserTests
    {
        [Fact]
        public void GivenCsvFileShouldParseIntoUserInformation()
        {
            var csvParser = new CsvParser( new FileReader(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv")));
            var userInformation = csvParser.GetNextUserInputInformation();
            
            Assert.Equal("David",userInformation.Name);
            Assert.Equal("Rudd",userInformation.Surname);
            Assert.Equal(60050,userInformation.Salary);
            Assert.Equal(9,userInformation.SuperRate);
            Assert.Equal("01, March",userInformation.StartDate);
            Assert.Equal("31, March",userInformation.EndDate);

        }
    }
}