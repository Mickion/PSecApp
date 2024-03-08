using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Abstractions
{
    public interface IFileValidatorService
    {
        /// <summary>
        /// Check if file has any data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool IsFileEmpty(string file);

        /// <summary>
        /// Only files that have not been downloaded already should be downloaded
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsFileAlreadyDownloaded(string path);

        /// <summary>
        /// Only files that have not been downloaded already should be downloaded
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        bool IsFileAlreadyDownloaded(string destinationFolder, string destinationFileName);

        /// <summary>
        /// Only files that have not been processed should be added to the table
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool IsFilesAlreadyProcessed(string fileName);
    }
}
