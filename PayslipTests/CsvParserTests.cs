using Moq;
using Payslip;
using Payslip.Interfaces;
using Xunit;

namespace PayslipTests
{
    public class CsvParserTests
    {
        [Fact]
        public void GivenCsvFileShouldParseIntoUserInformation()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(f => f.ReadFileLine()).Returns(new [] {"David","Rudd","60050","9%","01 March – 31 March "});
            var csvParser = new CsvParser(fileReader.Object);
            var userInformation = csvParser.GetNextUserInputInformation();
            
            Assert.Equal("David",userInformation.Name);
            Assert.Equal("Rudd",userInformation.Surname);
            Assert.Equal(60050,userInformation.Salary);
            Assert.Equal(9,userInformation.SuperRate);
            Assert.Equal("01, March",userInformation.StartDate);
            Assert.Equal("31, March",userInformation.EndDate);
        }

        [Fact]
        public void GivenNothingToReadShouldReturnNull()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(f => f.ReachedEnd()).Returns(true);
            var csvParser = new CsvParser( fileReader.Object);
            var user = csvParser.GetNextUserInputInformation();
            Assert.Null(user);

        }
    }
}