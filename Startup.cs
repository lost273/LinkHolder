using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkHolder.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LinkHolder {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddIdentity<AppUser, IdentityRole>(opts =>{
                opts.User.RequireUniqueEmail = true;})
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(o => {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthProperties.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthProperties.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthProperties.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });    

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration["Data:LinkHolder:ConnectionString"]));

            services.AddAuthorization(options => {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
 
            app.UseAuthentication();

            //app.UseHttpsRedirection();
            app.UseMvc();

            //AppDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
