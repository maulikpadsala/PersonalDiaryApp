using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalDiaryApp.Common;
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
            string path = ApiURL + "Login/Login?username="+model.Username+ "&password="+model.Password;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;
            UserModel userData = new UserModel();
            
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                userData = JsonConvert.DeserializeObject<UserModel>(result);

                if (userData.UserId == 0)
                {
                    model.Error = "Username or password invalid, Please try Agaian.";
                    return View(model);
                }
                                
                HttpContext.Session.SetString("UserName", userData.FirstName + " " + userData.LastName);
                HttpContext.Session.SetInt32("UserId", userData.UserId);
                model.Error = string.Empty;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.Error = "Username or password invalid, Please try Agaian.";
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
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            
            return RedirectToAction("Login", "Login");
        }


    }
}
