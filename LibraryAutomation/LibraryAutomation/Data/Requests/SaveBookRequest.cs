using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Requests
{
    public class SaveBookRequest : BaseRequest
    {
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public int BookType { get; set; }
        public int Category { get; set; }
        public int? Subcategory { get; set; }
        public string WrittenBy { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}