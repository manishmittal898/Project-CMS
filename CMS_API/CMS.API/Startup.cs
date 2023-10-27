
using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Cache;
using CMS.Core.ServiceHelper.Method;
using CMS.Data.Models;
using CMS.Service.Services.Account;
using CMS.Service.Services.CMSPage;
using CMS.Service.Services.Common;
using CMS.Service.Services.CustomerAddress;
using CMS.Service.Services.GeneralEntry;
using CMS.Service.Services.LookupMaster;
using CMS.Service.Services.LookupTypeMaster;
using CMS.Service.Services.OTP;
using CMS.Service.Services.ProductMaster;
using CMS.Service.Services.ProductReview;
using CMS.Service.Services.RoleType;
using CMS.Service.Services.SubLookupMaster;
using CMS.Service.Services.UserCartProduct;
using CMS.Service.Services.UserMaster;
using CMS.Service.Services.WishList;
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

namespace CMS.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string JWT_Key = Constants.JWT_Key;
        private const string JWT_ISSUER = Constants.JWT_ISSUER;
        private const string CONNECTION_STRING = Constants.CONNECTION_STRING;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddControllers();
            _ = services.AddDirectoryBrowser();
            _ = services.AddSwaggerGen(c =>
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
            _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
            _ = services.AddDbContext<DB_CMSContext>(options => options.UseSqlServer(Configuration[CONNECTION_STRING]));
            _ = services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
                     builder =>
                     {
                         _ = builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                     }));
            _ = services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;

                });
            Registerservice(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS (v1)");
                    c.RoutePrefix = "swagger";
                });
            }
            else
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS (v1)");
                    c.RoutePrefix = "swagger";
                });
            }

            _ = app.UseRouting();
            _ = app.UseStaticFiles();
            _ = app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
                EnableDirectoryBrowsing = true
            });
            _ = app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content",
                EnableDirectoryBrowsing = true
            });
            _ = app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            _ = app.UseHttpsRedirection();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();
            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
            _ = app.UseDeveloperExceptionPage();
        }

        private void Registerservice(IServiceCollection services)
        {
            _ = services.AddSingleton<BaseService>();
            _ = services.AddScoped<ICommonService, CommonService>();
            _ = services.AddScoped<IRoleTypeService, RoleTypeService>();
            _ = services.AddScoped<ILookupMasterService, LookupMasterService>();
            _ = services.AddScoped<ILookupTypeMasterService, LookupTypeMasterService>();
            services.AddScoped<IProductMasterService, ProductMasterService>();
            _ = services.AddScoped<IProductReviewService, ProductReviewService>();
            _ = services.AddScoped<ISubLookupMasterService, SubLookupMasterService>();
            _ = services.AddScoped<IUserMasterService, UserMasterService>();
            _ = services.AddScoped<ICMSPageService, CMSPageService>();
            _ = services.AddScoped<IAccountService, AccountService>();
            _ = services.AddScoped<IGECategoryService, GECategoryService>();
            _ = services.AddScoped<IGeneralEntryService, GeneralEntryService>();
            _ = services.AddScoped<ICustomerAddressService, CustomerAddressService>();
            _ = services.AddScoped<IWishListService, WishListService>();
            _ = services.AddScoped<IUserCartProductService, UserCartProductService>();
            _ = services.AddScoped<IOTPService, OTPService>();
            _ = services.AddScoped<ICacheService, CacheService>();

        }
    }
}
