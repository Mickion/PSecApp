﻿using PSecApp.Application.Models;
using PSecApp.Application.Helpers;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Domain.Interfaces;
using PSecApp.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PSecApp.Application.Services.Implementations
{
    /// <summary>
    /// Download files & nothing else (SRP).
    /// Call other services for any other functionality that is outside of what this service does.
    /// </summary>
    public class FileDownloadService : IFileDownloadService
    {        
        //TODO : Inject
        private HttpClient _httpClient = new HttpClient();
        private readonly IFileValidatorService _fileValidatorService;
        private readonly IAuditService _auditService;
        public FileDownloadService(IFileValidatorService fileValidatorService)
        {
            _fileValidatorService = fileValidatorService;
        }

        /// <summary>
        /// Downloads a file from source to destination
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        public async Task DownloadFilesAsync(string sourceFile, string destinationFolder, string destinationFileName)
        {
            
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            string path = Path.Combine(destinationFolder, destinationFileName);

            // Only files that have not been downloaded already should be downloaded.
            if (_fileValidatorService.IsFileAlreadyDownloaded(path)) return;

            using (Stream fileStream = await _httpClient.GetStreamAsync(sourceFile))
            {                    
                using (FileStream outputFileStream = new FileStream(path, FileMode.CreateNew))
                {
                    await fileStream.CopyToAsync(outputFileStream);


                }
            }
        }

        private void AuditFileDownload(bool isDownloadCompleted, string destinationFileName, string path="")
        {
            DailyMTMFilesAudit audit = new DailyMTMFilesAudit()
            {
                //[FileDate] date NOT NULL,
                FileName = destinationFileName,
                DownloadedFlag = isDownloadCompleted,
                DownloadStartTime = (!isDownloadCompleted) ? DateTime.Now : null,
                DownloadEndTime = (isDownloadCompleted) ? DateTime.Now : null,
                DownloadLocation = path,
                DownloadError varchar(250),
                [ProcessedFlag] bit NOT NULL,
                [ProcessingStartTime] date,
                [ProcessingEndTime] date,
                [ProcessingError] varchar(250)
            }

            _auditService.AuditFile(new Domain.Entities.DailyMTMFilesAudit()
            {

            });
        }
    }
}
