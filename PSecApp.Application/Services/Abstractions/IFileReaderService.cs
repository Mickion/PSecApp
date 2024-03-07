using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Abstractions
{
    public interface IFileReaderService<T, V> where T : class, new()
    {
        /// <summary>
        /// Extracts data from a file.
        /// A different implementation can be implemented to extract data from any type of file using this contract
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<List<T>> ReadDataFromAFileAsync(V source);
    }
}
