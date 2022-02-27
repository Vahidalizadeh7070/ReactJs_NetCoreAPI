using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Areas.Dashboard.Models.Repositories;
using GameHub.Helper;
using GameHub.Models;
using GameHub.Models.Interfaces;
using GameHub.Models.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameHub.Helper;

namespace GameHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(1));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    NameClaimType = "User",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
            });

            services.AddScoped<CategoryInterface, CategoryRepository>();
            services.AddScoped<NewsInterface, NewsRepository>();
            services.AddScoped<SliderInterface, SliderRepository>();
            services.AddScoped<ManagerSliderInterface, ManagerSliderRepository>();
            services.AddScoped<ManagerNewsInterface, ManagerNewsRepository>();
            services.AddScoped<ManagerCategoryInterface, ManagerCategoryRepository>();
            services.AddScoped<UserInterface, UserRepository>();
            services.AddScoped<ReviewInterface, ReviewRepository>();
            services.AddScoped<RateListInterface, RateListRepository>();
            services.AddScoped<FutureReleaseGameInterface, FutureReleaseGameRepository>();
            services.AddScoped<JWTService>();
            services.AddControllers();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000/")
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookiePolicy(
                new CookiePolicyOptions
                {
                    Secure = CookieSecurePolicy.Always
                });
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            
            app.UseAuthorization();
            
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
