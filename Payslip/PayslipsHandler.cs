using System;
using System.Collections.Generic;
using Payslip.Interfaces;
using PayslipTests;

namespace Payslip
{
    public class PayslipsHandler
    {
        private readonly IInputParser _csvParser;
        private readonly IPayslipCalculator _payslipCalculator;

        public PayslipsHandler(IInputParser csvParser, IPayslipCalculator payslipCalculator)
        {
            _csvParser = csvParser;
            _payslipCalculator = payslipCalculator;
          
        }
        
        public List<Payslip> CreateAllPayslips()
        {
            var payslips = new List<Payslip>();
            
            while (true)
            {
                var userInfo = _csvParser.GetNextUserInputInformation();
                if (userInfo != null)
                    payslips.Add(_payslipCalculator.GeneratePayslip(userInfo));
                else 
                    break;
            }

            return payslips;
        }
    }
}