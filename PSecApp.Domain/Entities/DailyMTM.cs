using PSecApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Entities
{
    /// <summary>
    /// Entity for DailyMTM.
    /// DailyMTM IS-A an Entity.
    /// </summary>
    public class DailyMTM: BaseEntity
    {                
        public string Contract { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; } = DateTime.Now;

        public string Classification { get; set; } = string.Empty;

        public double Strike { get; set; } = 0.00d;

        public string CallPut { get; set; } = string.Empty;

        public double MTMYield { get; set; } = 0.00d;

        public double MarkPrice { get; set; } = 0.00d;

        public double SpotRate { get; set; } = 0.00d;

        public double PreviousMTM { get; set; } = 0.00d;

        public double PreviousPrice { get; set; } = 0.00d;

        public double PremiumOnOption { get; set; } = 0.00d;

        public double Volatility { get; set; } = 0.00d;

        public double Delta { get; set; } = 0.00d;

        public double DeltaValue { get; set; } = 0.00d;

        public double ContractsTraded { get; set; } = 0.00d;

        public double OpenInterest { get; set; } = 0.00d;

    }
}
