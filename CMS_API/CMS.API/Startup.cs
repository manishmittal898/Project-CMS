
using CMS.Data.Models;
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

namespace CMS.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMS.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
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
              In = ParameterLocation.Header, },
            new List<string>()
          }
        });

            });
            services.AddDbContext<DB_CMSContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddTransient<IRoleTypeService, RoleTypeService>();
            services.AddTransient<ILookupMasterService, LookupMasterService>();
            services.AddTransient<ILookupTypeMasterService, LookupTypeMasterService>();
            services.AddTransient<IProductMasterService, ProductMasterService>();
            services.AddTransient<IProductReviewService, ProductReviewService>();
            services.AddTransient<ISubLookupMasterService, SubLookupMasterService>();
            services.AddTransient<IUserMasterService, UserMasterService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS.API v1"));
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
        //    app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
