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
        AppUser db = new AppUser();
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public ValuesController(UserManager<AppUser> userMgr,
            SignInManager<AppUser> signinMgr) {
            userManager = userMgr;
            signInManager = signinMgr;
        }
        [HttpGet]
        public ActionResult<string> Get() {
            return signInManager.Context.User.ToString();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) {
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
