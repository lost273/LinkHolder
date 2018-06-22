using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkHolder.Controllers {
    [Authorize(Roles = "User,Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private AppUser user; 
        private UserManager<AppUser> userManager;
        private AppDbContext appDbContext;
        public ValuesController(UserManager<AppUser> userMgr, AppDbContext appDbCont) {
            userManager = userMgr;
            appDbContext = appDbCont;
        }
        //the method shows all folders
        [HttpGet]
        public IEnumerable<ViewFolder> Get() {
            user = userManager.FindByEmailAsync(User.Identity.Name).Result;
            return appDbContext.Folders.Where(f => f.AppUserId == user.Id)
                                        .Select(f => new ViewFolder 
                                                    {Id = f.Id,
                                                     Name = f.Name, 
                                                     MyLinks = f.MyLinks
                                                     .Select(l => new ViewLink
                                                        {Id = l.Id,
                                                         Description=l.Description,
                                                         Body=l.Body}).ToList()})
                                        .ToList();
        }
        //the method keeps a link 
        [HttpPost]
        public async Task Post([FromBody] SaveLinkModel saveLink) {
            //eager loading
            user = appDbContext.Users.Include(u => u.MyFolders)
                                    .Select(u => u).Where(u => u.Email.Equals(User.Identity.Name))
                                    .FirstOrDefault();
            
            Link link = new Link {Body = saveLink.LinkBody, Description = saveLink.LinkDescription};
            Folder folder = new Folder();

            //if user don't have the folders
            if(user.MyFolders != null) {
                folder = user.MyFolders.Select(f => f).Where(f => f.Name.Equals(saveLink.FolderName))
                                        .FirstOrDefault() ?? new Folder();
            }
            //if user don't have the named folder
            if(folder.Name == null) {
                folder.Name = saveLink.FolderName;
                folder.AppUserId = user.Id;
                appDbContext.Folders.Add(folder);
            }
            
            link.FolderId = folder.Id;
            appDbContext.Links.Add(link);

            appDbContext.SaveChanges();
            await Response.WriteAsync("Link successfully saved");
        }
        //method changes a link
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }
        //method deletes a link
        [HttpDelete("link/{id}")]
        public async Task DeleteLink(int id) {
            Link link = appDbContext.Links.Select(l => l).Where(l => l.Id == id).FirstOrDefault();
            if (link == null) {
                await Response.WriteAsync("Link not found!");
                return;
            }
            appDbContext.Links.Remove(link);
            appDbContext.SaveChanges();
            await Response.WriteAsync("Link successfully deleted");
        }
        //method deletes a folder
        [HttpDelete("folder/{id}")]
        public async Task DeleteFolder(int id) {
            Folder folder = appDbContext.Folders.Select(f => f)
                                                .Where(f => f.Id == id)
                                                .FirstOrDefault();
            if (folder == null) {
                await Response.WriteAsync("Folder not found!");
                return;
            }
            List<Link> links = appDbContext.Links.Select(l => l)
                                                  .Where(l => l.FolderId == folder.Id)
                                                  .ToList();
            appDbContext.Folders.Remove(folder);
            appDbContext.SaveChanges();
            await Response.WriteAsync("Folder successfully deleted");
        }
    }
}
