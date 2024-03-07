using PSecApp.Application.Helpers;
using PSecApp.Application.Models;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Enums;
using PSecApp.Application.Exceptions;

namespace PSecApp.Application
{
    public class Application
    {
        // TODO: Save in config.json
        #region Private Members
        private readonly string _fileName = "20240102_D_Daily MTM Report.xls";
        private readonly string _destination = "C:\\Users\\micki\\Documents\\tst";
        private readonly string _downloadSource = "https://clientportal.jse.co.za/_layouts/15/DownloadHandler.ashx?FileName=/YieldX/Derivatives/Docs_DMTM";


        //TODO: Implement Dependency Injection        
        private readonly IAuditService _fileAuditService;
        private readonly IFileDataService _fileDataService;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly IFileValidatorService _fileValidatorService;        
        private readonly IFileReaderService<DailyMTM, DownloadFile> _fileReaderService;


        //private readonly IDailyContractsRepository dataRepository;
        //private readonly IFileReaderService<DailyMTM, DownloadFile> readerService;
        //private readonly IFileDataService dataService;
        #endregion

        #region Constructors
        public Application(
            IAuditService auditService,
            IFileDataService fileDataService,
            IFileDownloadService fileDownloadService, 
            IFileValidatorService fileValidatorService,
            IFileReaderService<DailyMTM, DownloadFile> fileReaderService)
        {
            _fileAuditService = auditService;
            _fileDataService = fileDataService;
            _fileDownloadService = fileDownloadService;
            _fileValidatorService = fileValidatorService;
            _fileReaderService = fileReaderService;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Entry point of the program
        /// </summary>
        /// <param name="forYear"></param>
        public async Task ProcessFiles(int forYear)
        {
            foreach (DownloadFile file in DownloadHelper.GetDownloadFileNames(_downloadSource, _destination, forYear))
            {
                try
                {
                    await this.DownloadFileAsync(file);
                }
                catch (FileDownloadException fileDownloadEx)
                {
                    await this.AuditAsync(FileProcessingStages.StartOfDownload, file, true, fileDownloadEx.Message);
                }

                try
                {
                    await this.ExtractDataFromFileAsync(file);
                }
                catch (FileProcessingException fileProcessingEx)
                {
                    await this.AuditAsync(FileProcessingStages.StartOfProcessing, file,  true, fileProcessingEx.Message);
                }
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Download a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task DownloadFileAsync(DownloadFile file)
        {
            await this.AuditAsync(FileProcessingStages.StartOfDownload, file);

            await _fileDownloadService.DownloadFilesAsync(file.SourceFileUri, file.DestinationFolder, file.DestinationFileName);

            await this.AuditAsync(FileProcessingStages.FileDownloadedSuccessfully, file); //Mark File As Downloaded
        }

        /// <summary>
        /// Extract data from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task ExtractDataFromFileAsync(DownloadFile file)
        {
            var fileData = await _fileReaderService.ReadDataFromAFileAsync(file);
            if (fileData.Count > 0)
            {
                var successfull = await _fileDataService.SaveFileDataAsync(fileData);
                if (successfull)
                    // FLAG as processed
                    await this.AuditAsync(FileProcessingStages.FileProcessedSuccessfully, file);

                else
                    await this.AuditAsync(FileProcessingStages.StartOfProcessing, file, true, "file failed to process.");

            }
        }

        /// <summary>
        /// Save File Audit
        /// </summary>
        /// <param name="filestage"></param>
        /// <param name="file"></param>
        /// <param name="error"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        private async Task<DailyMTMFilesAudit> AuditAsync(FileProcessingStages filestage, DownloadFile file, bool error = false, string errMessage="")
        {
            // DailyMTMFilesAudit audit = new DailyMTMFilesAudit();

            // If it doesn't exist, save default values
            DailyMTMFilesAudit audit = await _fileAuditService.GetFileAuditByFileNameAsync(file.DestinationFileName);

            switch (filestage)
            {
                case FileProcessingStages.StartOfDownload:
                    audit.FileDate = file.FileDate;
                    audit.FileName = file.DestinationFileName;
                    audit.ProcessingStartTime = DateTime.Now;
                    audit.DownloadedFlag = false;                   
                    audit.ProcessedFlag = false;
                    audit.DownloadError = error ? errMessage: "";
                    break;

                case FileProcessingStages.FileDownloadedSuccessfully:
                    audit.FileDate = file.FileDate;
                    audit.FileName = file.DestinationFileName;
                    audit.DownloadedFlag = error ? false : true; // if error, downloaded is false
                    audit.DownloadError = error ? errMessage: "";
                    audit.ProcessedFlag = false;                    
                    break;

                case FileProcessingStages.StartOfProcessing:
                    audit.FileDate = file.FileDate;
                    audit.FileName = file.DestinationFileName;
                    audit.ProcessingEndTime = !error ? DateTime.Now : null; // if no error, set timestamp
                    audit.ProcessedFlag = error ? false : true; // if error, downloaded is false;
                    audit.ProcessingError = error ? errMessage : "";
                    break;

                case FileProcessingStages.FileProcessedSuccessfully:
                    audit.FileDate = file.FileDate;
                    audit.FileName = file.DestinationFileName;
                    audit.ProcessingEndTime = !error ? DateTime.Now : null; // if no error, set timestamp
                    audit.ProcessedFlag = error ? false : true; // if error, downloaded is false;
                    audit.ProcessingError = error ? errMessage : "";
                    break;

                default:
                    break;
            }


            return await _fileAuditService.AuditFileAsync(audit);            
        }

        
        #endregion

    }
}
