using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Services.Abstractions
{
    public interface IFileDataService
    {        
        /// <summary>
        /// Persit datalist into a database
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        Task<bool> SaveFileDataAsync(List<DailyMTM> dataList);

    }
}
