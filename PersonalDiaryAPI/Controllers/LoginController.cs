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
    public class LoginController : ControllerBase
    {
        public PersonalDiaryDataContext DbContext;
        public LoginController(PersonalDiaryDataContext context)
        {
            DbContext = context;
        }

        [Route("Login")]
        [HttpGet]
        public async Task<UserModel> Login(string username,string password)
        {
            UserModel response = new UserModel();
            try
            {
                response = DbContext.Set<UserModel>().Where(m => m.Email == username && m.Password == password).ToListAsync().Result.FirstOrDefault();

                if (response == null)
                {
                    response = new UserModel();
                }
            }
            catch (Exception ex)
            {
                response = new UserModel();
            }
            return response;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<UserModel> CreateUser(UserModel model)
        {
            UserModel response = new UserModel();
            try
            {
                DbContext.Set<UserModel>().Add(model);
                int id = DbContext.SaveChangesAsync().Result;
                response = DbContext.Set<UserModel>().FindAsync(id).Result;
            }
            catch (Exception ex)
            {
                response = new UserModel();
            }
            return response;
        }
    }
}
