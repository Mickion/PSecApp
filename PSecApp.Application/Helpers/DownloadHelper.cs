using PSecApp.Application.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Helpers
{
    public static class DownloadHelper
    {

        /// <summary>
        /// Returns a dynamic list of file names to be downloaded.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<DownloadFile> GetDownloadFileNames(string source, string destination, int year)
        {
            string partialFileName = "_D_Daily MTM Report.xls";            
            List<DownloadFile> files = new();
            foreach (var day in DateTimeHelper.GetAllDaysForYear(year))
            {
                // Compose files names to download from JSE website
                // NB: My preferred solution was if JSE website had an API where we can filter files by year

                string fullFileName = day.ToString("yyyyMMdd") + partialFileName;

                files.Add(new DownloadFile()
                {
                    DestinationFolder = destination,
                    DestinationFileName = fullFileName,
                    SourceFileUri = source + "/" + fullFileName,
                    FileDate = day
                });
            }
            return files;
        }
    }
}
