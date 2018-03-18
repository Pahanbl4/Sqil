using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using WebApplication11.Data;
using WebApplication11.Data.Abstract;
using WebApplication11.Data.Repositories;
using WebApplication11.Model.Mapping;

namespace WebApplication11
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


            services.AddMvc();
            var connection = @"Server=PAVEL\INSTANCE;Database=USersRolesPermis;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            // Register the OAuth2 validation handler.
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                   .AddJwtBearer(options =>
                   {
                       options.Audience = "resource-server";
                       options.Authority = "http://localhost:58366/";
                       options.RequireHttpsMetadata = false;
                       options.IncludeErrorDetails = true;
                       
                   });
            // Automapper   
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            services.AddCors();
            services.AddMvc()
                .AddJsonOptions(opts =>
                {
                    // Force Camel Case to JSON
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            // Repositories
           
            services.AddScoped<IPermissionsRepository, PermissionsRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            app.UseStaticFiles();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            //context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
              });

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseWelcomePage();
        }
    }
}
