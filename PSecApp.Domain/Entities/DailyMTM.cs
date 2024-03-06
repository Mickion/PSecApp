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
        public DateTime FileDate { get; set; } = DateTime.Now;

        public string Contract { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; } = DateTime.Now;

        public string Classification { get; set; } = string.Empty;

        public float Strike { get; set; } = 0.00f;

        public string CallPut { get; set; } = string.Empty;

        public float MTMYield { get; set; } = 0.00f;

        public float MarkPrice { get; set; } = 0.00f;

        public float SpotRate { get; set; } = 0.00f;

        public float PreviousMTM { get; set; } = 0.00f;

        public float PreviousPrice { get; set; } = 0.00f;

        public float PremiumOnOption { get; set; } = 0.00f;

        public float Volatility { get; set; } = 0.00f;

        public float Delta { get; set; } = 0.00f;

        public float DeltaValue { get; set; } = 0.00f;

        public float ContractsTraded { get; set; } = 0.00f;

        public float OpenInterest { get; set; } = 0.00f;

    }
}
