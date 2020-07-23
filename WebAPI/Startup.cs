using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.client;
using App.contracts;
using Domain.entity;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Security.Token;
using WebAPI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace WebAPI {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<RebStoreContext> (optionsAction => {
                optionsAction.UseMySQL (Configuration.GetConnectionString ("DefaultConnection"));
            });
            
            services.AddControllers (opt=>{
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation (cfg => cfg.RegisterValidatorsFromAssemblyContaining<Create> ());
            
            services.AddMediatR (typeof (Consult.handler).Assembly);
            
            var builder = services.AddIdentityCore<User> ();
            
            var identityBuilder = new IdentityBuilder (builder.UserType, builder.Services);
            
            identityBuilder.AddEntityFrameworkStores<RebStoreContext> ();
            
            identityBuilder.AddSignInManager<SignInManager<User>> ();
            
            services.TryAddSingleton<ISystemClock, SystemClock> ();
            
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("mi palabra secreta"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddScoped<IJwtGenerate, JwtGenerate> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiddleware<HandleErrorMiddleware> ();
            
            if (env.IsDevelopment ()) {
                //app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            //app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}