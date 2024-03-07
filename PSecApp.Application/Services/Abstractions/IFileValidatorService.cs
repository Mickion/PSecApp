using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Abstractions
{
    public interface IFileValidatorService
    {
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
        /// TODO: Only files that have not been processed should be added to the table
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool IsFilesAlreadyProcessed(string fileName);
    }
}
