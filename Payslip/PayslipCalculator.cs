using System;
using System.Collections.Generic;
using PayslipTests;

namespace Payslip
{
    public class PayslipCalculator : IPayslipCalculator
    {
        private readonly ICalculationsHandler _monthlyCalculator;

        public PayslipCalculator(ICalculationsHandler monthlyCalculator)
        {
            _monthlyCalculator = monthlyCalculator;
        }
        public virtual Payslip GeneratePayslip(User user)
        {
            try
            {
                var payslip = new Payslip
                {
                    Fullname = $"{user.Name} {user.Surname}",
                    GrossIncome = _monthlyCalculator.CalculateGrossIncome(user.Salary),
                    IncomeTax = _monthlyCalculator.CalculateIncomeTax(user.Salary)
                };

                payslip.NetIncome = _monthlyCalculator.CalculateNetIncome(payslip.GrossIncome, payslip.IncomeTax);

                payslip.Super = _monthlyCalculator.CalculateSuper(user.SuperRate, payslip.GrossIncome);

                payslip.PayPeriod = $"{user.StartDate.Replace(",", "")} - {user.EndDate.Replace(",", "")}";

                return payslip;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Could not calculate income tax, generated empty payslip. {e.Message}");
                return new Payslip();
            }
        }
    }
}