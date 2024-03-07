using PSecApp.Application.Exceptions;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Domain.Interfaces;
using System.IO;

namespace PSecApp.Application.Services.Implementations
{
    public class FileValidatorService : IFileValidatorService
    {
        private readonly IAuditFileRepository _auditFileRepository;
        public FileValidatorService(IAuditFileRepository auditFileRepository)
        {
            _auditFileRepository = auditFileRepository;
        }

        /// <summary>
        /// Validates if file contains data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsFileEmpty(string file)
        {
            if (new FileInfo(file).Length == 0)
                 throw new FileProcessingException("File is empty, processing ignored.");

            return (new FileInfo(file).Length == 0);
        }

        /// <summary>
        /// Validates if file already downloaded on destination folder & database
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        public bool IsFileAlreadyDownloaded(string destinationFolder, string destinationFileName)
        {
            // Only files that have not been downloaded already should be downloaded.
            if (this.IsFileAlreadyDownloaded(Path.Combine(destinationFolder, destinationFileName)));

            var chkDb = _auditFileRepository.GetAuditByFileNameAsync(destinationFileName);
            chkDb.Wait();

            if (chkDb != null)
            {
                if (chkDb.Result.DownloadedFlag)
                    throw new FileDownloadException("File has already been downloaded");
            }

            return false;
        }

        /// <summary>
        /// Validates if file already downloaded on destination folder only
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsFileAlreadyDownloaded(string path)
        {
            // Only files that have not been downloaded already should be downloaded.
            if (File.Exists(path))
                throw new FileDownloadException("File has already been downloaded.");

            return false;
        }

        /// <summary>
        /// Only files that have not been processed should be added to the table
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsFilesAlreadyProcessed(string fileName)
        {
            var chkDb = _auditFileRepository.GetAuditByFileNameAsync(fileName);
            chkDb.Wait();

            if (chkDb != null)
            {
                if (chkDb.Result.ProcessedFlag)                
                    throw new FileProcessingException("File has already been processed.");                
            }

            return false;
        }
    }
}
