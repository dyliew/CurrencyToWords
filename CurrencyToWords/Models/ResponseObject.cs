using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyToWords.Models
{
    public class ResponseObject
    {
        public bool Success { get; set; }
        public object Payload { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
    }
}