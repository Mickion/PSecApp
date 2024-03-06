using PSecApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Entities
{
    /// <summary>
    /// Entity for DailyMTM report
    /// </summary>
    public class TradedContracts
    {
        public DateTime FileDate { get; set; } = DateTime.Now;

        public string Contract { get; set; } = string.Empty;

        /// <summary>
        /// Total contracts traded for each contract
        /// </summary>
        public int TotalContractsTraded { get; set; } = 0;

        /// <summary>
        /// Percentage of each daily traded contracts.
        /// </summary>
        public float TotalContractsTradedPerc { get; set; }

    }
}
