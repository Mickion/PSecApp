using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Domain.Common
{

    //TODO: Its NOT IS-A
    internal abstract class BaseContract
    {
        public DateTime FileDate { get; set; } = DateTime.Now;

        public string Contract { get; set; } = string.Empty;
    }
}
