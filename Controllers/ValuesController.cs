using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<Folder>> Get() {
            user = userManager.FindByEmailAsync(User.Identity.Name).Result;
            return user.MyFolders;
        }

        [HttpPost]
        public void Post([FromBody] SaveLinkModel saveLink) {
            
            user = userManager.FindByEmailAsync(User.Identity.Name).Result;
            Link link = new Link {Body = saveLink.LinkBody, Description = saveLink.LinkDescription};
            Folder folder = new Folder();

            if(user.MyFolders != null) {
                folder = user.MyFolders.Select(f => f).Where(f => f.Name.Equals(saveLink.FolderName)).FirstOrDefault();
            }
            if(folder.Name == null) {
                folder.Name = saveLink.FolderName;
            }
        
            folder.MyLinks.Add(link);
            
            appDbContext.Links.Add(link);
            appDbContext.Folders.Add(folder);
            

            folder.MyLinks.Add(link);
            user.MyFolders.Add(folder);
            appDbContext.Users.Update(user);

            appDbContext.Folders.Add(folder);
            
            user.MyFolders.Add(folder);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
