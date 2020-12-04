using System;
using System.Collections.Generic;

namespace Payslip
{
    public class MonthlyCalculationsHandler : ICalculationsHandler
    {
        private readonly List<TaxBracket> _taxBrackets;

        public MonthlyCalculationsHandler()
        {
            _taxBrackets = new List<TaxBracket>
            {
                new TaxBracket(0, 18200, 0, 0),
                new TaxBracket(18201, 37000, 0,0.19),
                new TaxBracket(37001, 87000, 3572, 0.325 ),
                new TaxBracket(87001, 180000, 19822, 0.37 ),
                new TaxBracket(180001, Int32.MaxValue, 54232,0.45 )
            };
        }

        public int CalculateIncomeTax(int salary)
        {
            foreach (var bracket in _taxBrackets)
            {
                if (!bracket.InBracket(salary)) continue;
                var extraFee =  Math.Round((salary - bracket.LowerBound) * bracket.Rate);
                return Convert.ToInt32(Math.Round(bracket.FlatFee + extraFee) / 12);
            }
            
            throw new Exception("Could not calculate Income Tax for given salary");

        }

        public int CalculateSuper(int superRate, int grossIncome)
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