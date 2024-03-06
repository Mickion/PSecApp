using PSecApp.Application.Models;
using PSecApp.Application.Helpers;
using PSecApp.Application.Services.Abstractions;

namespace PSecApp.Application.Services.Implementations
{
    public class DownloadService : IDownloadService
    {        
        //TODO : Inject
        //TODO : Add logger

        private HttpClient _httpClient = new HttpClient();
        private string _partialFileName = "_D_Daily MTM Report.xls"; //TODO: Where to store this?

        //(DownloadFileService depends/requires Process File Service
        private FileProcessorService _fileProcessor = new FileProcessorService(); // TODO: Interface & Inject (DownloadFileService depend on Process File)

        #region Public Methods
        public async Task DownloadFilesAsync(string source, string destination, int year)
        {            
            foreach (DownloadFile file in this.GetDownloadFileNames(source, destination, year))
            {
                // Do not re-download already downloaded files
                // TODO: remove Only files that have not been downloaded already should be downloaded
                if (File.Exists(Path.Combine(file.DestinationFolder, file.DestinationFileName))) continue;

                await this.DownloadFilesAsync(file.SourceFileUri, file.DestinationFolder, file.DestinationFileName);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Downloads a file from a source to local disk destination
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        private async Task DownloadFilesAsync(string sourceFile, string destinationFolder, string destinationFileName)
        {
            // Get file stream
            using (Stream fileStream = await _httpClient.GetStreamAsync(sourceFile))
            {
                await DownloadFilesAsync(fileStream, destinationFolder, destinationFileName);
            }
        }

        /// <summary>
        /// Saves the file stream into local destination folder
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        private async Task DownloadFilesAsync(Stream fileStream, string destinationFolder, string destinationFileName)
        {
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            string path = Path.Combine(destinationFolder, destinationFileName);
            
            // Output file stream into a file
            using (FileStream outputFileStream = new FileStream(path, FileMode.CreateNew))
            {
                await fileStream.CopyToAsync(outputFileStream);

                // Do not process empty files.
                if (new FileInfo(path).Length > 0)
                {
                    _fileProcessor.ProcessDownloadedFile(path);
                }
                        
            }
        }

        /// <summary>
        /// Returns a dynamic list of file names to be downloaded.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private List<DownloadFile> GetDownloadFileNames(string source, string destination, int year)
        {
            // TODO: Should I refactor & remove the HelperClass?
            List<DownloadFile> files = new();
            foreach (var day in DateTimeHelper.GetAllDaysForYear(year))
            {
                // Compose files names to download from JSE website
                // NB: My preferred solution was if JSE website had an API where we can filter files by year

                string fullFileName = day.ToString("yyyyMMdd") + _partialFileName;

                files.Add(new DownloadFile()
                {
                    DestinationFolder = destination,
                    DestinationFileName = fullFileName,
                    SourceFileUri = source + "/" + fullFileName
                });
            }
            return files;
        }
        #endregion
    }

    record Download(string Url, string Path);
}
