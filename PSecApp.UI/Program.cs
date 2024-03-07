// Other comments are added to show case my understanding

using PSecApp.Application;
using PSecApp.Application.Helpers;
using PSecApp.Application.Models;
using PSecApp.Application.Services.Abstractions;
using PSecApp.Application.Services.Implementations;
using PSecApp.Domain.Entities;
using PSecApp.Domain.Interfaces;
using PSecApp.Infrastructure.Repositories;

//string fileName = "20240102_D_Daily MTM Report.xls";
//// TODO: Save in config.json
//string destination = "C:\\Users\\micki\\Documents\\psec";
//string source = "https://clientportal.jse.co.za/_layouts/15/DownloadHandler.ashx?FileName=/YieldX/Derivatives/Docs_DMTM";


//TODO: Implement Dependency Injection
IAuditFileRepository auditFileRepository = new AuditFileRepository(); 
IAuditService auditService = new AuditService(auditFileRepository);
IFileValidatorService fileValidatorService = new FileValidatorService(auditFileRepository);
IDailyContractsRepository dataRepository = new DailyContractsRepository();

IFileDownloadService dService = new FileDownloadService(fileValidatorService);
IFileReaderService<DailyMTM, DownloadFile> readerService = new FileReaderService(fileValidatorService);

IFileDataService dataService = new FileDataService(dataRepository);

Application app = new Application(auditService, dataService, dService, fileValidatorService, readerService);
await app.ProcessFiles(2024);
//foreach (DownloadFile file in DownloadHelper.GetDownloadFileNames(source, destination, 2024))
//{
//    // wrap with try
//    await dService.DownloadFilesAsync(file.SourceFileUri, file.DestinationFolder, file.DestinationFileName);
//    //Mark File As Downloaded

//    //
//    var data = await readerService.ReadDataFromAFileAsync(file);

//    //TODO: Exception handling
//    if(data.Count > 0)
//    {
//        var successfull = await dataService.SaveFileDataAsync(data);
//        if (successfull)
//        {
//            Console.WriteLine("Completed processing file {0}", file.SourceFileUri);
//        }
//        else
//        {
//            Console.WriteLine("Mark the file as un-processed for re-processing");
//        }
//    }

//    // Mark File As Processed

//}

//// TODO: Refactor working soultion
//DownloadService dService = new DownloadService();
//await dService.DownloadFilesAsync(source, destination, 2024);
//Console.WriteLine("FILES DOWNLOADED SUCCESSFULLY");

//// 1. Download file from - to

//var dFile = new DownloadFile()
//{
//    FileName = fileName,
//    DownloadToPath = destination + "\\" + fileName,
//    DownloadFromPath = source + "/" + fileName
//};

//await DownloadFileAsync(dFile);
//Console.WriteLine("FILE DOWNLOADED SUCCESSFULLY...");



//async Task DownloadFileAsync(DownloadFile file)
//{
//    // See https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
//    // for why, in the real world, you want to use a shared instance of HttpClient
//    // rather than creating a new one for each request
//    var httpClient = new HttpClient();

//    var httpResult = await httpClient.GetAsync(file.DownloadToPath);
//    using var resultStream = await httpResult.Content.ReadAsStreamAsync();
//    using var fileStream = System.IO.File.Create(file.DownloadFromPath);

//    resultStream.CopyTo(fileStream);
//}


//IUtils utils = new Utils();
//IDownloadService fileService = new DownloadService(utils, fromUrl, toUrl);
////IDailyMTMFileRepository fileRepository = new DailyMTMFileRepository();




//// TODO: Save to config file. WHERE TO put config, Application layer or UI layer??
//// 
//// string url = "https://clientportal.jse.co.za/_layouts/15/DownloadHandler.ashx?FileName=/YieldX/Derivatives/Docs_DMTM/20240104_D_Daily%20MTM%20Report.xls";


//
//var downloadedFiles = await fileService.DownloadYearFilesAsync(2024);

//IExcelService excelService = new ExcelService();
//excelService.ProcessFiles(downloadedFiles);

Console.ReadLine();
