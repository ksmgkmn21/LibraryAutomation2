using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Entity
{
    public class BookType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string BookTypeName { get; set; }
    }
}