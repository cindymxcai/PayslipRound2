﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Payslip
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var fileReader = new FileReader(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/input.csv"));
             var fileWriter = new FileWriter(Path.Combine(Environment.CurrentDirectory, "../../../../Payslip/output.csv"));

             var consoleWriter = new ConsoleWriter();
             var csvParser = new CsvParser(fileReader);
             
            
            var calculationsHandler = new MonthlyCalculationsHandler();
            var monthValidator = new DateValidator();
            var payslipGenerator = new PayslipCalculator(calculationsHandler, monthValidator);
            var payslipHandler = new PayslipsHandler(csvParser, payslipGenerator);
            var payslips = payslipHandler.CreateAllPayslips();

            foreach (var payslip in payslips)
            {
                fileWriter.WritePayslipInformation(payslip);
                consoleWriter.WritePayslipInformation(payslip);
            }
        }
    }
}