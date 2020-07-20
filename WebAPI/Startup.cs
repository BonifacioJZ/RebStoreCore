using System;
using System.Collections.Generic;
using System.Linq;
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
using Persistence;
using Security.Token;
using WebAPI.Middleware;

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
            
            services.AddControllers ().AddFluentValidation (cfg => cfg.RegisterValidatorsFromAssemblyContaining<Create> ());
            
            services.AddMediatR (typeof (Consult.handler).Assembly);
            
            var builder = services.AddIdentityCore<User> ();
            
            var identityBuilder = new IdentityBuilder (builder.UserType, builder.Services);
            
            identityBuilder.AddEntityFrameworkStores<RebStoreContext> ();
            
            identityBuilder.AddSignInManager<SignInManager<User>> ();
            
            services.TryAddSingleton<ISystemClock, SystemClock> ();
            
            services.AddScoped<IJwtGenerate, JwtGenerate> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiddleware<HandleErrorMiddleware> ();
            
            if (env.IsDevelopment ()) {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}