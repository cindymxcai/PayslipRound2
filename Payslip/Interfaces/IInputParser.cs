namespace Payslip.Interfaces
{
    public interface IInputParser
    {
        User GetUserInformation(string[] Fields);
        User GetNextUserInputInformation();
    }
}