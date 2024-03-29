﻿using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Interfaces
{
    public interface IDailyContractsRepository
    {
        /// <summary>
        /// Persists file data into a dababase
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> InsertFileDataAsyc(List<DailyMTM> data);

    }
}
