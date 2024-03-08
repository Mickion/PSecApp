using PSecApp.Application.Helpers;
using PSecApp.Application.POCOs;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Enums;
using PSecApp.Application.Exceptions;
using Microsoft.Extensions.Options;

namespace PSecApp.Application.Services.Implementations
{

    public class OrchestratorService : IOrchestratorService
    {

        #region Private Members   
        private readonly AppSettings _config;
        private readonly IAuditService _fileAuditService;
        private readonly IFileDataService _fileDataService;
        private readonly IFileDownloadService _fileDownloadService;
        private readonly IFileReaderService<DailyMTM, DownloadFile> _fileReaderService;               
        #endregion

        #region Constructors
        public OrchestratorService(
            IAuditService auditService,
            IFileDataService fileDataService,
            IFileDownloadService fileDownloadService,
            IFileValidatorService fileValidatorService,
            IFileReaderService<DailyMTM, DownloadFile> fileReaderService, IOptions<AppSettings> options)
        {
            _fileAuditService = auditService;
            _fileDataService = fileDataService;
            _fileDownloadService = fileDownloadService;
            _fileReaderService = fileReaderService;
            _config = options.Value;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Downlloads files for the specified year 
        /// </summary>
        /// <param name="forYear"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ProcessFilesAsync(int forYear)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Processing of files started at :  {0}", DateTime.Now);

            await this.ProcessFilesAsync(forYear, _config.DownloadFromUri, _config.DestinationFolder);
        }


        /// <summary>
        /// Entry point of the program
        /// </summary>
        /// <param name="forYear"></param>
        public async Task ProcessFilesAsync(int forYear, string source, string destination)
        {
            foreach (DownloadFile file in DownloadHelper.GetDownloadFileNames(source, destination, forYear))
            {
                try
                {
                    await DownloadFileAsync(file);
                }
                catch (FileDownloadException fileDownloadEx)
                {
                    await AuditAsync(FileProcessingStages.StartOfDownload, file, true, fileDownloadEx.Message);
                    this.Red(DateTime.Now + " : by passed download : " + file.DestinationFileName+ " reason "+ fileDownloadEx.Message);
                }

                try
                {
                    await ExtractDataFromFileAsync(file);
                }
                catch (FileProcessingException fileProcessingEx)
                {
                    await AuditAsync(FileProcessingStages.StartOfProcessing, file, true, fileProcessingEx.Message);
                    this.Red(DateTime.Now + " : by passed file extract : " + file.DestinationFileName + " reason " + fileProcessingEx.Message);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Processing of files ended at :  {0}", DateTime.Now);
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
            this.Amber(DateTime.Now + " : Starting download of "+ file.DestinationFileName);
            await AuditAsync(FileProcessingStages.StartOfDownload, file);            

            await _fileDownloadService.DownloadFilesAsync(file.SourceFileUri, file.DestinationFolder, file.DestinationFileName);

            await AuditAsync(FileProcessingStages.FileDownloadedSuccessfully, file); //Mark File As Downloaded
            this.Green(DateTime.Now + " : Downloaded Successful " + file.DestinationFileName);
        }

        /// <summary>
        /// Extract data from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task ExtractDataFromFileAsync(DownloadFile file)
        {
            this.Amber(DateTime.Now + " : EXTRACTING FILE DATA " + file.DestinationFileName);
            var fileData = await _fileReaderService.ReadDataFromAFileAsync(file);
            if (fileData.Count > 0)
            {
                var successfull = await _fileDataService.SaveFileDataAsync(fileData);
                if (successfull)
                {
                    // FLAG as processed
                    await AuditAsync(FileProcessingStages.FileProcessedSuccessfully, file);

                    this.Green(DateTime.Now + " : EXTRACTED Successfuly " + file.DestinationFileName);
                }
                else
                {
                    await AuditAsync(FileProcessingStages.StartOfProcessing, file, true, "file failed to process.");
                    this.Red(DateTime.Now + " : FILE FAILED EXTRACTED" + file.DestinationFileName);
                }                  
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
        private async Task<DailyMTMFilesAudit> AuditAsync(FileProcessingStages filestage, DownloadFile file, bool error = false, string errMessage = "")
        {

            // Get existing file Audit, if it exists so to update it accordingly, else create new audit entry.
            DailyMTMFilesAudit audit = await _fileAuditService.GetFileAuditByFileNameAsync(file.DestinationFileName);

            switch (filestage)
            {
                case FileProcessingStages.StartOfDownload:
                    audit.FileDate = file.FileDate;
                    audit.FileName = file.DestinationFileName;
                    audit.ProcessingStartTime = DateTime.Now;
                    audit.DownloadedFlag = false;
                    audit.ProcessedFlag = false;
                    audit.DownloadError = error ? errMessage : "";
                    break;

                case FileProcessingStages.FileDownloadedSuccessfully:
                    audit.FileDate = file.FileDate;
                    audit.FileName = file.DestinationFileName;
                    audit.DownloadedFlag = error ? false : true; // if error, downloaded is false
                    audit.DownloadError = error ? errMessage : "";
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


        #region Console App trace
        private void Green(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
        }

        private void Amber(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
        }

        private void Red(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
        }

        private void Blue(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(msg);
        }
        #endregion

    }
}
