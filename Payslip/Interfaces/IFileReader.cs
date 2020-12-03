namespace Payslip.Interfaces
{
    public interface IFileReader
    {
        string[] ReadFileLine();
        bool ReachedEnd();
    }
}