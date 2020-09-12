using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IssueTracker.Services.Identity.Application;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Infrastructure;
using IssueTracker.Services.Identity.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

			services.AddControllers().AddControllersAsServices();
			services.AddMvcCore(options =>
			{
				options.EnableEndpointRouting = false;
				//options.Filters.Add(new ApiExceptionFilter());
			}).AddAuthorization();
			services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
		 // .AddNewtonsoftJson();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseHttpsRedirection();
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			app.UseStaticFiles();
			app.UseHttpsRedirection();

			app.UseRouting();

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
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllers();
			//});
		}
	}
}
