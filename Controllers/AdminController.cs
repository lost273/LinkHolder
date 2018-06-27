using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkHolder.Controllers{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class AdminController : Controller{
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;
        public AdminController(UserManager<AppUser> usrMgr,
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passValid,
            IPasswordHasher<AppUser> passwordHash){
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }
        [HttpGet]
        public List<EditUserModel> GetUsers(){
            return userManager.Users.Select(u => 
                                        new EditUserModel(){
                                            Id = u.Id, 
                                            Name = u.UserName, 
                                            Email = u.Email,
                                        }).ToList();
        }
        [HttpGet("{id}")]
        public async Task<EditUserModel> GetUser(string id){
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null){
                return new EditUserModel() {Id = user.Id, 
                                        Name = user.UserName, 
                                        Email = user.Email,
                                        };
            } else {
                return new EditUserModel();
            }
        }
        [HttpPost]
        public async Task Create([FromBody]CreateUserModel model){
            if(ModelState.IsValid) {
                AppUser user = new AppUser{
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if(result.Succeeded){
                    await Response.WriteAsync("User successfully created");
                } else {
                    await Response.WriteAsync($"{result}");
                }
            } else {
                await Response.WriteAsync("ModelState is not valid!");
            }
        }
        [HttpDelete("{id}")]
        public async Task Delete(string id){
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null){
                IdentityResult result = await userManager.DeleteAsync(user);
                if(result.Succeeded){
                    await Response.WriteAsync("User successfully deleted");
                } else {
                    await Response.WriteAsync($"{result}");
                }
            } else {
                await Response.WriteAsync("User Not Found");
            }
        }
        [HttpPut("{id}")]
        public async Task Edit(string id,[FromBody]EditUserModel model){
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null){
                user.Email = model.Email;
                user.UserName = model.Name;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager,user);
                if(!validEmail.Succeeded){
                    await Response.WriteAsync($"{validEmail}");
                    return;
                }
                IdentityResult validPass = null;
                if(!string.IsNullOrEmpty(model.Password)){
                    validPass = await passwordValidator.ValidateAsync(userManager, user, model.Password);
                    if(validPass.Succeeded){
                        user.PasswordHash = passwordHasher.HashPassword(user, model.Password);
                    } else {
                        await Response.WriteAsync($"{validPass}");
                        return;
                    }
                }
                if((validEmail.Succeeded && validPass == null)||(validEmail.Succeeded && model.Password != string.Empty && validPass.Succeeded)){
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if(result.Succeeded){
                        await Response.WriteAsync("User successfully changed");
                    } else {
                         await Response.WriteAsync($"{result}");
                    }
                }
            } else {
                await Response.WriteAsync("User Not Found");
            }
        }
    }
}