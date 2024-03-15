using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Requests
{
    public class DeleteBookRequest : BaseRequest
    {
        public int BookId { get; set; }
    }
}