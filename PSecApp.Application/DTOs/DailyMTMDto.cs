using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.DTOs
{
    public class DailyMTMDto
    {
        public string FileDate { get; set; } = string.Empty;

        public string Contract { get; set; } = string.Empty;

        public string ExpiryDate { get; set; } = string.Empty;

        public string Classification { get; set; } = string.Empty;

        public string Strike { get; set; } = string.Empty;

        public string CallPut { get; set; } = string.Empty;

        public string MTMYield { get; set; } = string.Empty;

        public string MarkPrice { get; set; } = string.Empty;

        public string SpotRate { get; set; } = string.Empty;

        public string PreviousMTM { get; set; } = string.Empty;

        public string PreviousPrice { get; set; } = string.Empty;

        public string PremiumOnOption { get; set; } = string.Empty;

        public string Volatility { get; set; } = string.Empty;

        public string Delta { get; set; } = string.Empty;

        public string DeltaValue { get; set; } = string.Empty;

        public string ContractsTraded { get; set; } = string.Empty;

        public string OpenInterest { get; set; } = string.Empty;
    }
}
