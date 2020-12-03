namespace Payslip.Interfaces
{
    public interface IFileReader
    {
        string[] ReadFileLine(string path);
    }
}