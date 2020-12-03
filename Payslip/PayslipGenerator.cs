using System;
using Payslip.Interfaces;

namespace Payslip
{
    public class PayslipGenerator 
    {
        private readonly IInputParser _csvParser;

        public PayslipGenerator(IInputParser csvParser)
        {
            _csvParser = csvParser;
        }

        public Payslip GeneratePayslip()
        {
            var userInformation = GetUserInformation();
            var payslip = new Payslip
            {
                Fullname = $"{userInformation.Name} {userInformation.Surname}",
                GrossIncome = CalculateGrossIncome(userInformation.Salary),
                IncomeTax = CalculateIncomeTax(userInformation.Salary)
            };

            payslip.NetIncome = CalculateNetIncome(payslip.GrossIncome, payslip.IncomeTax);

            payslip.Super = CalculateSuperAmount(userInformation.SuperRate, payslip.GrossIncome);

            payslip.PayPeriod =
                $"{userInformation.StartDate.Replace(",", "")} - {userInformation.EndDate.Replace(",", "")}";

            return payslip;
        }

        private User GetUserInformation()
        {
            return _csvParser.GetUserInformation();
        }

        private int CalculateIncomeTax(int salary)
        {
            double incomeTax = 0;

            if (salary >= 0 && salary <= 18200)
            {
                incomeTax = 0;
            }

            if (salary >= 18201 && salary <= 37000)
            {
                incomeTax = (((salary - 18200) * 0.19) / 12);
            }

            if (salary >= 37001 && salary <= 87000)
            {
                incomeTax = (3572 + (salary - 37000) * 0.325) / 12;
            }

            if (salary >= 87001 && salary <= 180000)
            {
                incomeTax = (19822 + (salary - 87000) * 0.37) / 12;
            }

            if (salary >= 180001)
            {
                incomeTax = (54232 + (salary - 180000) * 0.45) / 12;
            }

            return Convert.ToInt32(Math.Round(incomeTax, 0, MidpointRounding.AwayFromZero));
        }

        public int CalculateSuperAmount(int superRate, int grossIncome)
        {
            return Convert.ToInt32(Math.Round(grossIncome * (superRate / 100.0)));
        }

        public int CalculateNetIncome(int grossIncome, int incomeTax)
        {
            return grossIncome - incomeTax;
        }

        public int CalculateGrossIncome(int salary)
        {
            return Convert.ToInt32(Math.Round(salary / 12.0));
        }
    }
}