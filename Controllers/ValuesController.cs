using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public void Post([FromBody] SaveLinkModel saveLink) {
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
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        [HttpDelete("link/{id}")]
        public void Delete(int id) {
            Link link = appDbContext.Links.Select(l => l).Where(l => l.Id == id).FirstOrDefault();
            appDbContext.Links.Remove(link);
            appDbContext.SaveChanges();
        }
    }
}
