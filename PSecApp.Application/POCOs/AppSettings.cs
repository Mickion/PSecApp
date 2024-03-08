using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.POCOs
{
    /// <summary>
    /// Options pattern
    /// </summary>
    public class AppSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DownloadFromUri { get; set; } = string.Empty;

        public string DestinationFolder { get; set; } = string.Empty;
    }
}
