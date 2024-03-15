using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryAutomation.Data.Responses
{
    public class LoginUserResponse : BaseResponse
    {
        public string Token { get; set; }

    }
}