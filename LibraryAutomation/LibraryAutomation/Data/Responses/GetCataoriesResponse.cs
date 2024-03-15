using LibraryAutomation.Data.Enums;
using LibraryAutomation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Responses
{
    public class GetCataoriesResponse : BaseResponse
    {
        public List<CatagoryModel> Catagories { get; set; }
    }
}