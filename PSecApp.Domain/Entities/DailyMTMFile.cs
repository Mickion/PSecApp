using PSecApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Entities
{
    /// <summary>
    /// Entity for processed files.
    /// DailyMTMFile IS-A an Entity.
    /// </summary>
    public class DailyMTMFile: BaseEntity
    {
        /// <summary>
        /// FileDate
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Location of downloaded file
        /// </summary>
        public string FileLocation { get; set; } = string.Empty;

        /// <summary>
        /// Flag if file already downloaded
        /// </summary>
        public bool Downloaded { get; set; } = false;

        /// <summary>
        /// Flag if file already processed
        /// </summary>
        public bool Processed { get; set; } = false;
    }
}
