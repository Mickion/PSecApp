using PSecApp.Application.Services.Implementations;

string fileName = "20240102_D_Daily MTM Report.xls";
// TODO: Save in config.json
string destination = "C:\\Users\\micki\\Documents\\psec";
string source = "https://clientportal.jse.co.za/_layouts/15/DownloadHandler.ashx?FileName=/YieldX/Derivatives/Docs_DMTM";

// TODO: Refactor working soultion
DownloadService dService = new DownloadService();
await dService.DownloadFilesAsync(source, destination, 2024);
Console.WriteLine("FILES DOWNLOADED SUCCESSFULLY");

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

public class DownloadFile
{
    public string FileName { get; set; } = string.Empty;
    public string DownloadFromPath { get; set; } = string.Empty;
    public string DownloadToPath { get; set; } = string.Empty;
}

//public class Response
//{
//    public bool Success { get; set; } = true;
//    public string Message { get; set; } = string.Empty;
//}