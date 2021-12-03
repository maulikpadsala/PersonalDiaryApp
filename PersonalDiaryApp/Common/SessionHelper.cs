using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiaryApp.Common
{
    public class SessionHelper
    {
        public static int UserId
        {
            get => HttpHelper.HttpContext.Session.GetInt32("UserId").HasValue ? HttpHelper.HttpContext.Session.GetInt32("UserId").Value : 0;
            set => HttpHelper.HttpContext.Session.SetInt32("UserId", value);
        }

        public static string WelcomeUser
        {

            get => HttpHelper.HttpContext.Session.GetString("WelcomeUser") == null ? "Guest" : HttpHelper.HttpContext.Session.GetString("WelcomeUser");
            set => HttpHelper.HttpContext.Session.SetString("WelcomeUser", value);
        }
    }
}
