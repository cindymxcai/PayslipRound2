using Microsoft.VisualBasic.FileIO;
using Payslip.Interfaces;

namespace Payslip
{
    public class FileReader : IFileReader
    {
        private TextFieldParser _textFieldParser;

        public string[] ReadFileLine(string path)
        {
            _textFieldParser = new TextFieldParser(path) {TextFieldType = FieldType.Delimited};
            _textFieldParser.SetDelimiters(",");
            return _textFieldParser.ReadFields();
        }

    }
}