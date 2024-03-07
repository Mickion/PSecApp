using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Enums
{
    public enum FileProcessingStages
    {
        StartOfDownload,
        FileDownloadedSuccessfully,
        StartOfProcessing,
        FileProcessedSuccessfully,
    }
}
