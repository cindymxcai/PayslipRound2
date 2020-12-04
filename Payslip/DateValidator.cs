using System;
using System.Collections.Generic;
using System.Linq;

namespace Payslip
{
    public class DateValidator
    {
        private readonly Dictionary<string, int> _months = new Dictionary<string, int>
        {
            {"January", 31},
            {"February", 29},
            {"March", 31},
            {"April", 30},
            {"May", 31},
            {"June", 30},
            {"July", 31},
            {"August", 31},
            {"September", 30},
            {"October", 31},
            {"November", 30},
            {"December", 31}
        };
        public string FormatDate(string startDate, string endDate)
        {
            if (IsValidDate(startDate, endDate))
                return $"{startDate.Replace(",", "")} - {endDate.Replace(",", "")}";
            throw new Exception("Invalid date!!");
        }

        private bool IsValidDate(string startDate, string endDate)
        {
            var startMonth = startDate.Split(", ").Last();
            var startDay = int.Parse(startDate.Split(", ").First());
            var endMonth = endDate.Split(", ").Last();
            var endDay = int.Parse(endDate.Split(", ").First());


            if (!_months.ContainsKey(startMonth) || !_months.ContainsKey(endMonth)) return false;
            return _months.First(m => m.Key == startMonth).Value >= startDay && startDay > 0 
                   && _months.First(m => m.Key == endMonth).Value >= endDay && endDay > 0;
        }
    }
}