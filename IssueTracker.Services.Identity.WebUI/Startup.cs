using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using IssueTracker.Services.Identity.Application;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Infrastructure;
using IssueTracker.Services.Identity.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IdentityServer4.Extensions;
using IssueTracker.Services.Identity.WebUI.Filters;
using IdentityServer4.Services;
using Serilog;

namespace IssueTracker.Services.Identity.WebUI
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplication(Configuration);
			services.AddScoped<ICurrentUserService, CurrentUserService>();
			services.AddInfrastructure(Configuration, Environment, typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
			services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter())).AddControllersAsServices();
			services.AddMvcCore(options =>
			{
				options.EnableEndpointRouting = false;
			}).AddAuthorization();
			services.AddScoped<IProfileService, ProfileService>();
			services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users API", Version = "v1" });
			});
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", cors =>
						cors.AllowAnyOrigin()
							.AllowAnyMethod()
							.WithExposedHeaders("Content-Disposition")
							.AllowAnyHeader());
			});


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			var serilog = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.Enrich.FromLogContext()
			.WriteTo.File(@"identityserver4_log.txt");

			loggerFactory.WithFilter(new FilterLoggerSettings
				{
					{ "IdentityServer4", LogLevel.Debug },
					{ "Microsoft", LogLevel.Warning },
					{ "System", LogLevel.Warning },
				}).AddSerilog(serilog.CreateLogger());
		
			app.UseHsts();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCors("CorsPolicy");
			app.UseRouting();
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API V1");
			});
			app.UseIdentityServer();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
			app.UseMvcWithDefaultRoute();
		}
	}
}
