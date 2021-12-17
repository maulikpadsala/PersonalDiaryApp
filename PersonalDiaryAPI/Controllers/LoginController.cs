using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalDiary.SharedLibrary.Models;
using PersonalDiaryAPI.Database;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiaryAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public PersonalDiaryDataContext DbContext;

        public UserManager<IdentityUser> userManager;
        public SignInManager<IdentityUser> signInManager;
        public LoginController(PersonalDiaryDataContext context, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            DbContext = context;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<UserModel> Login(LoginModel model)
        {
            UserModel response = new UserModel();
            try
            {
                var user = await userManager.FindByEmailAsync(model.Username);

                if (user == null)
                {
                    return new UserModel
                    {
                        Error = "User not found with this email id.",
                        IsSuccess=false
                    };
                }

                var isLoginUser =await userManager.CheckPasswordAsync(user,model.Password);

                if (!isLoginUser)
                {
                    return new UserModel
                    {
                        Error = "Password Incorrect, Please enter correct password.",
                        IsSuccess = false
                    };
                }

                response = DbContext.Set<UserModel>().Where(m => m.IdentityUserId==user.Id).ToListAsync().Result.FirstOrDefault();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response = new UserModel();
                response.Error = "Internal Server Error, Please try Again.";
                response.IsSuccess = false;
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
                var findUser = await userManager.FindByEmailAsync(model.Email);

                if (findUser!=null)
                {
                    if (!string.IsNullOrEmpty(findUser.Id))
                    {
                        response = model;
                        response.Error = "User already registered with this email id.";
                    }
                }

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult userResult =await userManager.CreateAsync(user, model.Password);

                if(userResult.Succeeded)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(user,model.Password, false,false);
                    if (signInResult.Succeeded)
                    {

                        var newUser = await userManager.FindByEmailAsync(model.Email);

                        //Manage Users Extra Property
                        model.Password = "IdentityReference";
                        model.IdentityUserId = newUser.Id;
                        model.Error = string.Empty;
                        model.IsActive = true;

                        DbContext.Set<UserModel>().Add(model);
                        int id = DbContext.SaveChangesAsync().Result;
                        response = DbContext.Set<UserModel>().FindAsync(id).Result;

                        response.IsSuccess = true;
                    }
                    else
                    {
                        model.Error="Something Went Wrong, Please try Again.";
                        model.IsSuccess = false;
                        response = model;
                    }
                }
                else
                {
                    if (userResult.Errors!=null)
                    {
                        foreach (var error in userResult.Errors)
                        {
                            model.Error += error.Description + " "; 
                        }
                    }
                    else
                    {
                        model.Error = "Something Went Wrong, Please try Again.";
                    }
                    model.IsSuccess = false;
                    response = model;
                }
                
            }
            catch (Exception ex)
            {
                response = new UserModel();
                response.Error = "Internal Server Error, Please try Again. " + ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
