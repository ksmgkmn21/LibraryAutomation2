using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Models
{
    public class BookModel
    {
        public string BookName { get; set; }

        public string BookType { get; set; }
        public string Category { get; set; }

        public string Subcategory { get; set; }

        public string WrittenBy { get; set; }

        public DateTime DateOfIssue { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }
    }
}