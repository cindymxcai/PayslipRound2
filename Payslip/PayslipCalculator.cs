using System;
using PayslipTests;

namespace Payslip
{
    public class PayslipCalculator : IPayslipCalculator
    {
        private readonly ICalculationsHandler _monthlyCalculator;
        private readonly DateValidator _dateValidator;

        public PayslipCalculator(ICalculationsHandler monthlyCalculator, DateValidator dateValidator)
        {
            _monthlyCalculator = monthlyCalculator;
            _dateValidator = dateValidator;
        }
        public virtual Payslip GeneratePayslip(User user)
        {
            try
            {
                var payslip = new Payslip();

                payslip.Fullname = $"{user.Name} {user.Surname}";
                payslip.GrossIncome = _monthlyCalculator.CalculateGrossIncome(user.Salary);
                payslip.IncomeTax = _monthlyCalculator.CalculateIncomeTax(user.Salary);
                payslip.NetIncome = _monthlyCalculator.CalculateNetIncome(payslip.GrossIncome, payslip.IncomeTax);
                payslip.Super = _monthlyCalculator.CalculateSuper(user.SuperRate, payslip.GrossIncome);
                try
                {
                    payslip.PayPeriod = _dateValidator.FormatDate(user.StartDate, user.EndDate);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Invalid month! {e.Message}");
                }

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