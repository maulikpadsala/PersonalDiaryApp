using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalDiary.SharedLibrary.Models;
using PersonalDiaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDiaryApp.Controllers
{
    public class LoginController : Controller
    {
        public static string ApiURL = "https://localhost:44395/api/";
        public IActionResult Index()
        {
            HttpContext.Session.SetString("UserName","Guest");
            HttpContext.Session.SetInt32("UserId", 0);
            return View();
        }
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            string path = ApiURL + "Login/Login";

            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(
                path, stringContent).Result;

            var result = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);

            if (result.IsSuccess)
            {
                if (result.UserId == 0 || !string.IsNullOrEmpty(result.Error))
                {
                    return View(model);
                }
                                
                HttpContext.Session.SetString("UserName", result.FirstName + " " + result.LastName);
                HttpContext.Session.SetInt32("UserId", result.UserId);
                HttpContext.Session.SetString("AccessToken", result.Token);

                model.Error = string.Empty;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.Error = result.Error;
                return View(model);
            }
            
        }

        public IActionResult Registration()
        {
            UserModel model = new UserModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(UserModel model)
        {
            string path = ApiURL + "Login/CreateUser";

            HttpClient client = new HttpClient();
            
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(
                path, stringContent).Result;

            var result = JsonConvert.DeserializeObject<UserModel>(response.Content.ReadAsStringAsync().Result);

            if (result.IsSuccess)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(result);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            
            return RedirectToAction("Login", "Login");
        }


    }
}
