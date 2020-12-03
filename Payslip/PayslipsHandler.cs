using System.Collections.Generic;
using Payslip.Interfaces;

namespace Payslip
{
    public class PayslipsHandler
    {
        private readonly IInputParser _csvParser;
        private readonly PayslipGenerator _payslipGenerator;

        public PayslipsHandler(IInputParser csvParser, PayslipGenerator payslipGenerator)
        {
            _csvParser = csvParser;
            _payslipGenerator = payslipGenerator;
        }
        
        public List<Payslip> CreateAllPayslips()
        {
            var payslips = new List<Payslip>();
            
            while (true)
            {
                var userInfo = _csvParser.GetNextUserInputInformation();

                if (userInfo == null)
                    break;

                var payslip = _payslipGenerator.GeneratePayslip(userInfo);

                payslips.Add(payslip);
            }

            return payslips;
        }
    }
}