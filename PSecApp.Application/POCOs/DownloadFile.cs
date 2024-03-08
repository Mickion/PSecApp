using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.POCOs
{
    public class DownloadFile
    {
        public string DestinationFolder { get; set; } = string.Empty;

        public string DestinationFileName { get; set; } = string.Empty;

        public string SourceFileUri { get; set; } = string.Empty;

        public DateTime FileDate { get; set; } = DateTime.Now;

    }
}
