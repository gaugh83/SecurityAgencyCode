using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.ViewModels
{
   public class ErrorViewModel
    {
   public ErrorViewModel(bool showError)
        {
            ShowException = showError;
        }

        public ErrorViewModel(bool showError, Exception exception)
        {
            ShowException = showError;
            Exception = exception;
        }

        public bool ShowException { get; set; }
        public Exception Exception { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public DateTime ErrorDate { get; set; }
        public string ExceptionDetail { get; set; }
    }
    }
