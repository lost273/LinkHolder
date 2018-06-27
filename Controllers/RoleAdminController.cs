using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkHolder.Controllers {
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class RoleAdminController : Controller {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        public RoleAdminController(RoleManager<IdentityRole> roleMgr,
                                    UserManager<AppUser> userMrg) {
            roleManager = roleMgr;
            userManager = userMrg;
        }
        [HttpGet]
        public async Task<List<RoleModificationModel>> GetRoles() {
            List<RoleModificationModel> RMMList = new List<RoleModificationModel>();
            foreach(IdentityRole role in roleManager.Roles){
                List<string> members = new List<string>();
                List<string> notMembers = new List<string>();
                foreach(AppUser user in userManager.Users){
                    Boolean result = await userManager.IsInRoleAsync(user, role.Name);
                    if(result) {
                        members.Add(user.Id);
                    } else {
                        notMembers.Add(user.Id);
                    }
                }
                RoleModificationModel RMM = new RoleModificationModel(){
                    RoleName = role.Name,
                    RoleId = role.Id,
                    IdsToAdd = members.ToArray(),
                    IdsToDelete = notMembers.ToArray()
                };
                RMMList.Add(RMM);
            }
            return RMMList;
        }
        [HttpPost]
        public async Task Create([FromBody]string name) {
            if(ModelState.IsValid){
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if(result.Succeeded){
                    await Response.WriteAsync("Role successfully created");
                } else {
                    await Response.WriteAsync($"{result}");
                }
            } else {
                await Response.WriteAsync("ModelState is not valid!");
            }
        }
        [HttpPut]
        public async Task<string> Edit([FromBody]RoleModificationModel model){
            IdentityResult result;
            if(ModelState.IsValid){
                foreach(string userId in model.IdsToAdd ?? new string[]{}){
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if(user != null){
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if(!result.Succeeded){
                            return result.ToString();
                        }
                    }
                }
                foreach(string userId in model.IdsToDelete ?? new string[]{}){
                    AppUser user = await userManager.FindByIdAsync(userId);
                    if(user != null){
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if(!result.Succeeded){
                            return result.ToString();
                        }
                    }
                }
            }
            if(ModelState.IsValid){
                return "OK";
            } else {
                return "false";
            }
        }
        [HttpDelete("{id}")]
        public async Task Delete(string id){
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if(role != null){
                IdentityResult result = await roleManager.DeleteAsync(role);
                if(result.Succeeded){
                    await Response.WriteAsync("Role successfully deleted");
                } else {
                    await Response.WriteAsync($"{result}");
                }
            } else {
                await Response.WriteAsync("Role Not Found");
            }
        }
    }
}