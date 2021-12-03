using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonalDiaryApp.Common;
using PersonalDiaryApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDiaryApp.Controllers
{
    public class HomeController : Controller
    {

        public static string ApiURL = "https://localhost:44395/api/";

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("Login","Login");
            }

            TempData["UserName"] = HttpContext.Session.GetString("UserName");
            List<EventModel> eventList = new List<EventModel>();

            string path = ApiURL + "Event/GetAllUserEvents?userId="+ HttpContext.Session.GetInt32("UserId");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                eventList = JsonConvert.DeserializeObject<List<EventModel>>(result);
            }

            return View(eventList);
        }
        public IActionResult AddEvent()
        {
            EventModel eventModel = new EventModel();
            eventModel.EventDate = DateTime.Now;
            return View(eventModel);
        }
        [HttpPost]
        public IActionResult AddEvent(EventModel model, IFormFile EventImage)
        {
            if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            model.UserId =Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            string base64FileString;
            if (EventImage != null)
            {
                if (EventImage.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    EventImage.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    base64FileString = Convert.ToBase64String(fileBytes);
                    model.EventImage = "data:" + EventImage.ContentType + ";base64," + base64FileString;
                }
            }
            else
            {
                model.EventImage = string.Empty;
            }


            string path = ApiURL + "Event/CreateEvent";
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(
                path, stringContent).Result;
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
        public IActionResult EventDetail(int eventId)
        {
            if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            EventModel eventdata = new EventModel();
            string path = ApiURL + "Event/GetEvent?eventId=" + eventId;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                eventdata = JsonConvert.DeserializeObject<EventModel>(result);
            }
            return View(eventdata);
        }
        public IActionResult EditEvent(int eventId)
        {
            if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            EventModel eventdata = new EventModel();
            string path = ApiURL + "Event/GetEvent?eventId=" + eventId;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                eventdata = JsonConvert.DeserializeObject<EventModel>(result);
            }
            return View(eventdata);
        }
        [HttpPost]
        public IActionResult EditEvent(EventModel model,string oldImage, IFormFile EventImage)
        {
            if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            model.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            string base64FileString;
            if (EventImage != null)
            {
                if (EventImage.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    EventImage.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    base64FileString = Convert.ToBase64String(fileBytes);
                    model.EventImage = "data:" + EventImage.ContentType + ";base64," + base64FileString;
                }
            }
            else
            {
                model.EventImage = oldImage;
            }

            string path = ApiURL + "Event/UpdateEvent";
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(
                path, stringContent).Result;
            response.EnsureSuccessStatusCode();

            return RedirectToAction("EventDetail", "Home",new { eventId=model.EventId });
        }
        public IActionResult DeleteEvent(int eventid)
        {
            if (HttpContext.Session.GetInt32("UserId") == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            string path = ApiURL + "Event/DeleteEvent?eventId=" + eventid;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
