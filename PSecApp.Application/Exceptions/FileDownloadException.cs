using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Exceptions
{
    public class FileDownloadException : Exception
    {
        public FileDownloadException() { }

        public FileDownloadException(string message): base(message)
        {                
        }
    }
}
