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
        public ValuesController(UserManager<AppUser> userMgr
            ) {
            userManager = userMgr;
            user = userManager.FindByEmailAsync(User.Identity.Name).Result;

        }
        [HttpGet]
        public ActionResult<List<Folder>> Get() {
            return user.MyFolders;
        }

        [HttpPost]
        public void Post([FromBody] SaveLinkModel saveLink) {
            Link link = new Link {Body = saveLink.LinkBody};
            Folder folder = user.MyFolders.Select(f => f).Where(f => f.Name.Equals(saveLink.FolderName));
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
