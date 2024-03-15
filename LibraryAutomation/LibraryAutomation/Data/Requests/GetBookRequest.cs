using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Requests
{
    public class GetBookRequest : BaseRequest
    {
        public int BookId { get; set; }
    }
}