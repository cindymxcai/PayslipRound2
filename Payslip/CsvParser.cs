using System.Linq;
using Payslip.Interfaces;

namespace Payslip
{
    public class CsvParser : IInputParser
    {
        private string[] Fields { get; }

        public CsvParser(string path, IFileReader fileReader)
        {
            Fields = fileReader.ReadFileLine(path);
        }
        
        public User GetUserInformation()
        {
            var dateInputs = Fields[4].Split(" ");
            dateInputs.ToList().Remove("-");

            var userInfo = new User
            {
                Name = Fields[0],
                Surname = Fields[1],
                Salary = int.Parse(Fields[2]),
                SuperRate = int.Parse(Fields[3].Replace("%","")),
                StartDate = ParsePaymentStart(dateInputs),
                EndDate = ParsePaymentEnd(dateInputs)
            };
            
            return userInfo;
        }

        private static string ParsePaymentEnd(string[] dateInputs)
        {
            return $"{dateInputs[3]}, {dateInputs[4]}";
        }

        private static string ParsePaymentStart(string[] dateInputs)
        {
            return $"{dateInputs[0]}, {dateInputs[1]}";
        }
    }
}