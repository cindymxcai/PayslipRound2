using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace PayslipRound2
{
    public class CsvParser
    {
        private TextFieldParser _textFieldParser;

        private string[] Fields { get; }

        public CsvParser(string path)
        {
            Fields = SplitRow(path).ToArray();
        }

        private IEnumerable<string> SplitRow(string path)
        {
            _textFieldParser = new TextFieldParser(path) {TextFieldType = FieldType.Delimited};
            _textFieldParser.SetDelimiters(",");
            return _textFieldParser.ReadFields();
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