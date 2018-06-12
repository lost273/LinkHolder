using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LinkHolder.Controllers {
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class AccountController : Controller {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> userMgr,
            SignInManager<AppUser> signinMgr) {
            userManager = userMgr;
            signInManager = signinMgr;
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<String> Register([FromBody]CreateUserModel model) {
            if (!ModelState.IsValid) {
                return "Register - ModelState is not valid";
            }
            var user = new AppUser() { UserName = model.Name, Email = model.Email };
 
            IdentityResult result = await userManager.CreateAsync(user, model.Password);
 
            if (!result.Succeeded) {
                return result.ToString();
            }
 
            return "Register - OK";
        }
        
        [HttpPost("token")]
        [AllowAnonymous] 
         public async Task Token() { 
            var username = Request.Form["username"];
            var password = Request.Form["password"];
           
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }
 
            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthProperties.ISSUER,
                    audience: AuthProperties.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthProperties.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthProperties.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
             
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
 
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }
 
        private ClaimsIdentity GetIdentity(string username, string password) {
            var user = userManager.FindByEmailAsync(username).Result;
            var roles = userManager.GetRolesAsync(user).Result;
            if (roles != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username)
                };
                foreach(string r in roles){
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, r));
                }
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
 
            return null;
        }
    }
}