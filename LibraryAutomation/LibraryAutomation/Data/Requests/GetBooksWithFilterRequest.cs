using LibraryAutomation.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Requests
{
    public class GetBooksWithFilterRequest : BaseRequest
    {
        public int Id { get; set; }

        public FilterType Filter { get; set; }
    }
}