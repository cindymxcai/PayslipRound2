using Microsoft.VisualBasic.FileIO;
using Payslip.Interfaces;

namespace Payslip
{
    public class FileReader : IFileReader
    {
        private readonly TextFieldParser _textFieldParser;

        public FileReader(string path)
        {
            _textFieldParser = new TextFieldParser(path) {TextFieldType = FieldType.Delimited};
            _textFieldParser.SetDelimiters(",");

        }

        public string[] ReadFileLine()
        { 
            return _textFieldParser.ReadFields();
        }

        public bool ReachedEnd()
        {
            return _textFieldParser.EndOfData;
        }
    }
}