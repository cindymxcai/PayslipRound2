using System.Linq;
using Payslip.Interfaces;

namespace Payslip
{
    public class CsvParser : IInputParser
    {
        private readonly IFileReader _fileReader;

        public CsvParser(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
        
        public User GetNextUserInputInformation()
        {
            if (_fileReader.ReachedEnd())
            {
                return null;
            }

            var fields = _fileReader.ReadFileLine();

            return GetUserInformation(fields);
             
        }      
        
        public User GetUserInformation(string[] fields)
        {
            var dateInputs = fields[4].Split(" ");
            dateInputs.ToList().Remove("-");

            var userInfo = new User
            {
                Name = fields[0],
                Surname = fields[1],
                Salary = int.Parse(fields[2]),
                SuperRate = int.Parse(fields[3].Replace("%","")),
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