using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Models
{
    //TODO: Change to record or not?
    public class DownloadFile
    {
        public string DestinationFolder { get; set; } = string.Empty;

        public string DestinationFileName { get; set; } = string.Empty;

        public string SourceFileUri { get; set; } = string.Empty;

        public DateTime FileDate { get; set; } = DateTime.Now;

        //string sourceFile, string destinationFolder, string destinationFileName
    }
}
