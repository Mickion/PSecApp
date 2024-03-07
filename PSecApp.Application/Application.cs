using PSecApp.Application.Helpers;
using PSecApp.Application.Models;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Application.Services.Implementations;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using PSecApp.Infrastructure.Repositories;
using PSecApp.Application.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PSecApp.Application
{
    public class Application
    {
        //TODO: Implement Dependency Injection        
        private readonly IAuditService _fileAuditService;
        private readonly IFileDataService _fileDataService;
        private readonly IFileDownloadService _downloadService;
        private readonly IFileValidatorService _fileValidatorService;        
        private readonly IFileReaderService<DailyMTM, DownloadFile> _fileReaderService;


        //private readonly IDailyContractsRepository dataRepository;
        //private readonly IFileReaderService<DailyMTM, DownloadFile> readerService;
        //private readonly IFileDataService dataService;

        public Application(
            IAuditService auditService,
            IFileDataService fileDataService,
            IFileDownloadService fileDownloadService, 
            IFileValidatorService fileValidatorService,
            IFileReaderService<DailyMTM, DownloadFile> fileReaderService)
        {
            _fileAuditService = auditService;
            _fileDataService = fileDataService;
        }

        public void ProcessFiles(int forYear)
        {
            string fileName = "20240102_D_Daily MTM Report.xls";
            // TODO: Save in config.json
            string destination = "C:\\Users\\micki\\Documents\\psec";
            string source = "https://clientportal.jse.co.za/_layouts/15/DownloadHandler.ashx?FileName=/YieldX/Derivatives/Docs_DMTM";

            foreach (DownloadFile file in DownloadHelper.GetDownloadFileNames(source, destination, 2024))
            {
                // wrap with try
                await dService.DownloadFilesAsync(file.SourceFileUri, file.DestinationFolder, file.DestinationFileName);
                //Mark File As Downloaded

                //
                var data = await readerService.ReadDataFromAFileAsync(file);

                //TODO: Exception handling
                if (data.Count > 0)
                {
                    var successfull = await dataService.SaveFileDataAsync(data);
                    if (successfull)
                    {
                        Console.WriteLine("Completed processing file {0}", file.SourceFileUri);
                    }
                    else
                    {
                        Console.WriteLine("Mark the file as un-processed for re-processing");
                    }
                }

                // Mark File As Processed

            }
        }
    }
}
