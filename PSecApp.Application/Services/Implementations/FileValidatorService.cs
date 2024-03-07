using PSecApp.Application.Services.Abstractions;
using System.IO;

namespace PSecApp.Application.Services.Implementations
{
    public class FileValidatorService : IFileValidatorService
    {
        /// <summary>
        /// Validates if file contains data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsFileEmpty(string file)
        {
            return (new FileInfo(file).Length == 0);
        }

        /// <summary>
        /// Validates if file already downloaded on destination folder
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        public bool IsFileAlreadyDownloaded(string destinationFolder, string destinationFileName)
        {
            // Only files that have not been downloaded already should be downloaded.
            return (File.Exists(Path.Combine(destinationFolder, destinationFileName)));
        }

        /// <summary>
        /// Validates if file already downloaded on destination folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsFileAlreadyDownloaded(string path)
        {
            // Only files that have not been downloaded already should be downloaded.
            return (File.Exists(path));
        }

        /// <summary>
        /// Only files that have not been processed should be added to the table
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsFilesAlreadyProcessed(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
