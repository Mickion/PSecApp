using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Helpers
{
    public class DateTimeHelper 
    {
        /// <summary>
        /// Return all the days for a specific year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<DateTime> GetAllDaysForYear(int year)
        {
            var daysYearList = new List<DateTime>();

            DateTime current = DateTime.Parse("1/1/"+ year);
            DateTime nextYear = current.AddYears(1);

            do
            {
                daysYearList.Add(current);

                if (current.Date == DateTime.Now.Date) {
                    break;
                }

                current = current.AddDays(1);
                
            } while (current < nextYear);

            return daysYearList;
        }
    }
}
