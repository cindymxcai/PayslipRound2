using System;
using System.IO;
using Xunit;

namespace PayslipTests
{
    public class CsvParserTests
    {
        [Fact]
        public void GivenCsvFileShouldParseIntoUserInformation()
        {
            var csvParser = new CsvParser(Path.Combine(Environment.CurrentDirectory, "../../../../PayslipRound2/input.csv"));
            var userInformation = csvParser.GetUserInformation();
            
            Assert.Equal("David",userInformation.Name);
            Assert.Equal("Rudd",userInformation.Surname);
            Assert.Equal(60050,userInformation.Salary);
            Assert.Equal(9,userInformation.SuperRate);
            Assert.Equal("01, March",userInformation.StartDate);
            Assert.Equal("31, March",userInformation.EndDate);

        }
    }
}