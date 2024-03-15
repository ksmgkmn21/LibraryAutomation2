using LibraryAutomation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Responses
{
    public class GetBookResponse : BaseResponse
    {
        public BookModel BookModel { get; set; }
    }
}