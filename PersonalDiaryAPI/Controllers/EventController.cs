using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.SharedLibrary.Models;
using PersonalDiaryAPI.Database;

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
        public async Task<EventListModel> GetAllUserEvents(int userId =0)
        {
            EventListModel response = new EventListModel();

            if (userId==0)
            {
                return new EventListModel { 
                    Error="User Id Not Valid."
                };
            }

            try
            {
                var list = DbContext.Set<EventModel>().Where(m=>m.UserId==userId).OrderByDescending(m=>m.EventDate).ToListAsync().Result;
                response.eventList = new List<EventModel>();
                foreach (var item in list)
                {
                    response.eventList.Add(item);
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response = new EventListModel();
                response.Error = "Internal server Error, Please try again.";
            }

            return response;
        }
        [Route("FavouriteEventsList")]
        [HttpGet]
        public async Task<EventListModel> FavouriteEventsList(int userId = 0)
        {
            EventListModel response = new EventListModel();

            if (userId == 0)
            {
                return new EventListModel
                {
                    Error = "User Id Not Valid."
                };
            }

            try
            {
                var list = DbContext.Set<EventModel>().Where(m => m.UserId == userId && m.IsFavourite==true).OrderByDescending(m => m.EventDate).ToListAsync().Result;
                response.eventList = new List<EventModel>();
                foreach (var item in list)
                {
                    response.eventList.Add(item);
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response = new EventListModel();
                response.Error = "Internal server Error, Please try again.";
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
