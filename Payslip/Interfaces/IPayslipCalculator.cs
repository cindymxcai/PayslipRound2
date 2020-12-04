using System.Collections.Generic;
using Payslip;

namespace PayslipTests
{
    public interface IPayslipCalculator
    {
        Payslip.Payslip GeneratePayslip(User user);
    }
}