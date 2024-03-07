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
    public class DailyMTMFilesAudit: BaseEntity
    {

        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Flag if file already downloaded
        /// </summary>
        public bool DownloadedFlag { get; set; } = false;

        /// <summary>
        /// Time download started. Will help with performance matrix
        /// </summary>
        public DateTime? DownloadStartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Time download ended. Will help with performance matrix
        /// </summary>
        public DateTime? DownloadEndTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Location of downloaded file
        /// </summary>
        public string DownloadLocation { get; set; } = string.Empty;

        /// <summary>
        /// Holds any download exceptions
        /// </summary>
        public string DownloadError { get; set; } = string.Empty;

        /// <summary>
        /// Flag if file already processed
        /// </summary>
        public bool ProcessedFlag { get; set; } = false;

        /// <summary>
        /// Time download started. Will help with performance matrix
        /// </summary>
        public DateTime? ProcessingStartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Time download ended. Will help with performance matrix
        /// </summary>
        public DateTime? ProcessingEndTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Holds any download exceptions
        /// </summary>
        public string ProcessingError { get; set; } = string.Empty;

    }
}
