using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSecApp.Application.Exceptions
{
    public class FileProcessingException : Exception
    {
        public FileProcessingException() { }

        public FileProcessingException(string message) : base(message)
        {
        }
    }
}
