using PSecApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Interfaces
{
    public interface IDailyMTMRepository
    {
       public bool SaveContract(DailyMTM dailyMTM);
    }
}
