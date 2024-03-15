using LibraryAutomation.Data.Enums;
using LibraryAutomation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Responses
{
    public class GetTypesResponse : BaseResponse
    {
        public List<TypeModel> Types { get; set; }
    }
}