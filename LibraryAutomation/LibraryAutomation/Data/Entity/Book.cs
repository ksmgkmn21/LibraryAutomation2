using LibraryAutomation.Data.Entity;
using LibraryAutomation.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data
{
    public class Book
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string BookName { get; set; }

        [Required]
        public int BookType { get; set; }

        [Required]
        public int Category { get; set; }

        public int? Subcategory { get; set; }

        [Required]
        [MaxLength(255)]
        public string WrittenBy { get; set; }

        [Required]
        public DateTime DateOfIssue { get; set; }

        [Required]
        public BookStatus BookStatus { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime LastUpdateDate { get; set; }

        public int? LastUpdateBy { get; set; }

        
    }
}