namespace Payslip
{
    public interface ICalculationsHandler
    {
        int CalculateIncomeTax(int salary);
        int CalculateGrossIncome(int salary);
        int CalculateNetIncome(int salary, int incomeTax);
        int CalculateSuper(int grossIncome, int superRate);

    }
}