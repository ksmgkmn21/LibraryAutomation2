using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Models
{
    public class LoginValidationModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}