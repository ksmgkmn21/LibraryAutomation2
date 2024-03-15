using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace LibraryAutomation.Data.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}