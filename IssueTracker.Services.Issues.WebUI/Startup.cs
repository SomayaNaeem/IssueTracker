using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IssueTracker.Services.Issues.Application;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Infrastructure;
using IssueTracker.Services.Issues.WebUI.Filters;
using IssueTracker.Services.Issues.WebUI.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace IssueTracker.Services.Issues.WebUI
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
			services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter())).AddNewtonsoftJson();
			services.AddApplication();
			services.AddInfrastructure(Configuration);
			services.AddHttpContextAccessor();
			services.AddScoped<ICurrentUserService, CurrentUserService>();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Issues APIs", Version = "v1" });
			});
			services.AddMvcCore().AddAuthorization();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.Authority = Configuration.GetSection("Identity:IdentityAuthUrl").Value;
				options.RequireHttpsMetadata = false;
				
				options.Audience = Configuration.GetSection("Identity:APIName").Value;
			});
			//string s=Configuration.GetSection("Identity:APIName").Value;
			//services.AddMediatR(Assembly.GetExecutingAssembly());
			//services.AddAuthentication("token")
			//.AddJwtBearer("token", options =>
			//{
			// options.Authority = Configuration.GetSection("Identity:IdentityAuthUrl").Value;
			// options.Audience = Configuration.GetSection("Identity:APIName").Value;

			// options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

			// // if token does not contain a dot, it is a reference token
			//// options.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");
			//})
			//.AddOAuth2Introspection("token", options =>
			//{
			// options.Authority = Configuration.GetSection("Identity:IdentityAuthUrl").Value;// IdentityConstants.ApplicationScheme;

			// // this maps to the API resource name and secret
			// options.ClientId = "mvc";
			// options.ClientSecret = "secret";
			//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();
			app.UseHttpsRedirection();

			app.UseRouting();
			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Issues APIs V1");
			});
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
