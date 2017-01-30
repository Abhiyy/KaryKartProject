using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppBanwao.KaryKart.Web.Helpers
{
    public class MessageHelper
    {

        public const string TempDataKey = "TempDataAlerts";

        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }

    }   
    public static class AlertStyles
        {
            public const string Success = "success";
            public const string Information = "info";
            public const string Warning = "warning";
            public const string Danger = "danger";
            public const string Default = "default";
        }
    public class ApplicationMessages
    {
        public class UserRegisterationType
        {
            public const string MOBILE = "mobile";
            public const string MOBILEWITHERROR = "mobilewitherror";
            public const string EMAIL = "email";
            public const string EMAILWITHERROR = "emailwitherror";
            public const string USEREXIST = "userexist";

        }
        public class UserLogin
        {
            public const string VALIDUSER = "validuser";
            public const string INVALIDUSER = "invaliduser";
        }
        public class ForgotPassword
        {
            public const string SUCCESS = "success";
            public const string VALIDUSEREMAILNOTSEND = "validuseremailnotsend";
            public const string ERROR = "error";
            public const string USERNOTEXISTS = "usernotexists";
        }

        public class Product
        {
            public const string SUCCESS = "success";
            public const string ERROR = "error";
           
        }

    }

    
}