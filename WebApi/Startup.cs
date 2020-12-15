using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebApi.Middleware;
using WebApi.Filters;
using Service.Fake.Fakers;
using Service.Interfaces;
using Models;
using Service.Fake;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;

namespace WebApi
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
            services.AddHealthChecks();
            services.AddControllers(options => {
                //options.ModelValidatorProviders.Clear();
                //options.Filters.Add<ValidatorAsyncActionFilter>();
            })
            .AddNewtonsoftJson(options => {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            })
            .AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            //services.AddScoped<LimitRequestsMiddleware>();
            services.AddScoped<SampleAsyncActionFilter>()
            .AddScoped<SampleActionFilter>()
            .AddScoped<ValidatorAsyncActionFilter>();

            services
            .AddSingleton<ProductFaker>()
            .AddSingleton<IEntityService<Product>>(x => new EntityService<Product>(x.GetService<ProductFaker>(), int.Parse(Configuration["ProductsCount"])))
            .AddSingleton<UserFaker>()
            .AddSingleton<IEntityService<User>>(x => new EntityService<User>(x.GetService<UserFaker>(), int.Parse(Configuration["UsersCount"])))
            .AddSingleton<OrderFaker>()
            .AddSingleton<IEntityService<Order>>(x => new EntityService<Order>(x.GetService<OrderFaker>(), int.Parse(Configuration["OrdersCount"])));

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(AuthService.Key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
            });

            services.AddScoped<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseMiddleware<LimitRequestsMiddleware>();
            app.UseMiddleware<LoggingRequestsMiddleware>();
            //app.Use(async (context, next) => { System.Console.WriteLine("USE"); await next();});
            //app.Run(async context => await context.Response.WriteAsync("Hello!"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=WeatherForecast}/{action=Get}/{id?}"
                // );

                endpoints.MapControllers();

                endpoints.MapGet("/hello", async x => await x.Response.WriteAsync("Hello!"));
                endpoints.MapHealthChecks("/health");
            });

        }
    }
}
