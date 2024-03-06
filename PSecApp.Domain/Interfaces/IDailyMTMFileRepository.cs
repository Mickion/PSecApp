using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Interfaces
{
    /// <summary>
    /// Persist processed files to a database.
    /// If any files are deleted from the download folder, 
    /// </summary>
    public interface IDailyMTMFileRepository
    {
        /// <summary>
        /// Save file that has been downloaded.
        /// </summary>
        /// <param name="dailyMTMFile"></param>
        /// <returns></returns>
        bool MarkFileAsDownloaded(DailyMTMFile dailyMTMFile);

        /// <summary>
        /// Flag a downloaded file after processing
        /// </summary>
        /// <param name="dailyMTMFile"></param>
        /// <returns></returns>
        bool MarkFileAsProcessed(DailyMTMFile dailyMTMFile);

        /// <summary>
        /// Return all the un-processed files
        /// </summary>
        /// <returns></returns>
        List<DailyMTMFile> GetAllUnprocessedFiles();

        /// <summary>
        /// Return all downloaded files
        /// </summary>
        /// <returns></returns>
        List<DailyMTMFile> GetAllDownloadedFiles();

        /// <summary>
        /// Get a file by it ID
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        DailyMTMFile GetFileById(string uniqueId);
    }
}
