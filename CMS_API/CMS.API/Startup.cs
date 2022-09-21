
using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Data.Models;
using CMS.Service.Services.Account;
using CMS.Service.Services.CMSPage;
using CMS.Service.Services.Common;
using CMS.Service.Services.LookupMaster;
using CMS.Service.Services.LookupTypeMaster;
using CMS.Service.Services.ProductMaster;
using CMS.Service.Services.ProductReview;
using CMS.Service.Services.RoleType;
using CMS.Service.Services.SubLookupMaster;
using CMS.Service.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace CMS.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        const string JWT_Key = Constants.JWT_Key;
        const string JWT_ISSUER = Constants.JWT_ISSUER;
        const string CONNECTION_STRING = Constants.CONNECTION_STRING;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           services.AddControllers();
           
            services.AddDirectoryBrowser();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: 'Bearer {Token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration[JWT_ISSUER],
        ValidAudience = Configuration[JWT_ISSUER],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[JWT_Key]))
    };
});

            services.AddDbContext<DB_CMSContext>(options =>
options.UseSqlServer(Configuration[CONNECTION_STRING]));

            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            Registerservice(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS (v1)");
                    c.RoutePrefix = "swagger";
                });
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS (v1)");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
                EnableDirectoryBrowsing = true
            });
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content",
                EnableDirectoryBrowsing = true
            });
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseDeveloperExceptionPage();
          

        }

        private void Registerservice(IServiceCollection services)
        {
            services.AddSingleton<BaseService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IRoleTypeService, RoleTypeService>();
            services.AddScoped<ILookupMasterService, LookupMasterService>();
            services.AddScoped<ILookupTypeMasterService, LookupTypeMasterService>();
            services.AddScoped<IProductMasterService, ProductMasterService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<ISubLookupMasterService, SubLookupMasterService>();
            services.AddScoped<IUserMasterService, UserMasterService>();
            services.AddScoped<ICMSPageService, CMSPageService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
