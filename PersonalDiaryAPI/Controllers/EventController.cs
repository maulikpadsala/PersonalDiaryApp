using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDiaryAPI.Database;
using PersonalDiaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        
        public PersonalDiaryDataContext DbContext;
        public EventController(PersonalDiaryDataContext context)
        {
            DbContext = context;
        }
        [Route("GetAllUserEvents")]
        [HttpGet]
        public async Task<List<EventModel>> GetAllUserEvents(int userId =0)
        {
            List<EventModel> response = new List<EventModel>();

            try
            {
                var list = DbContext.Set<EventModel>().Where(m=>m.UserId==userId).ToListAsync().Result;
                foreach (var item in list)
                {
                    response.Add(item);
                }
            }
            catch (Exception ex)
            {
                response = new List<EventModel>();
            }

            return response;
        }
        [Route("CreateEvent")]
        [HttpPost]
        public async Task<EventModel> CreateEvent(EventModel model)
        {
            EventModel response = new EventModel();
            try
            {
                DbContext.Set<EventModel>().Add(model);
                int id = DbContext.SaveChangesAsync().Result;
                response = DbContext.Set<EventModel>().FindAsync(id).Result;
            }
            catch (Exception ex)
            {
                response = new EventModel();
            }
            return response;
        }

        [Route("GetEvent")]
        [HttpGet]
        public async Task<EventModel> GetEvent(int eventId = 0)
        {
            EventModel response = new EventModel();
            try
            {
                response = DbContext.Set<EventModel>().FindAsync(eventId).Result;
            }
            catch (Exception ex)
            {
                response = new EventModel();
            }
            return response;
        }

        [Route("UpdateEvent")]
        [HttpPut]
        public async Task<bool> UpdateEvent(EventModel model)
        {
            bool response = false;
            try
            {
                DbContext.Set<EventModel>().Update(model);
                await DbContext.SaveChangesAsync();
                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }
            return response;
        }

        [Route("DeleteEvent")]
        [HttpDelete]
        public async Task<bool> DeleteEvent(int eventId = 0)
        {
            bool response = false;
            try
            {
                var entity = DbContext.Set<EventModel>().FindAsync(eventId).Result;
                DbContext.Set<EventModel>().Remove(entity);
                await DbContext.SaveChangesAsync();
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }
    }
}
