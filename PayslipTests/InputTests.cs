using System;
using System.IO;
using Payslip;
using Xunit;

namespace PayslipTests
{
    public class InputTests
    {
        [Fact]
        public void GivenPathShouldReadAllText()
        {
            var fileReader =
                new FileReader(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
            var lines = fileReader.ReadFileLine();
            Assert.Equal("David", lines[0]);
            Assert.Equal("Rudd", lines[1]);
            Assert.Equal("60050", lines[2]);
        }
    }
}