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
        Task<bool> SaveFileDataAsync(List<DailyMTM> dataList);

    }
}
