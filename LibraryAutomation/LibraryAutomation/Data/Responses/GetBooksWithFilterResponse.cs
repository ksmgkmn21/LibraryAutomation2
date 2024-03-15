using LibraryAutomation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Responses
{
    public class GetBooksWithFilterResponse : BaseResponse
    {
        public List<BookFilterModel> Books { get; set; }
    }
}