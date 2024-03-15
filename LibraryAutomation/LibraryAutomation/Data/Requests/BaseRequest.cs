using LibraryAutomation.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Requests
{
    public class BaseRequest
    {
        public Language Language { get; set; }
        public string Token { get; set; }
    }
}