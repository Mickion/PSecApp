namespace PSecApp.Application.Services.Abstractions
{
    public interface IFileDownloadService
    {
        /// <summary>
        /// Downloads file from source to location
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFileName"></param>
        /// <returns></returns>
        Task DownloadFilesAsync(string sourceFile, string destinationFolder, string destinationFileName);
    }
}
