using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using IssueTracker.Services.Identity.Application;
using IssueTracker.Services.Identity.Application.Common.Interfaces;
using IssueTracker.Services.Identity.Infrastructure;
using IssueTracker.Services.Identity.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IssueTracker.Services.Identity.WebUI.Filters;
using IdentityServer4.Services;
using Serilog;
using NSwag.Generation.Processors.Security;
using NSwag;
using NSwag.AspNetCore;
using OpenApiOAuthFlows = NSwag.OpenApiOAuthFlows;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;
using System.Collections.Generic;
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
			services.AddInfrastructure(Configuration, Environment, typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
			services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter())).AddControllersAsServices();
			services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);			
			services.AddTransient<IProfileService, ProfileService>();
			services.AddTransient<ICurrentUserService, CurrentUserService>();
			services.AddOpenApiDocument(options =>
			{
				options.DocumentName = "v1";
				options.Title = "Identity API";
				options.Version = "v1";

				options.AddSecurity("oauth2", new OpenApiSecurityScheme
				{
					Type = OpenApiSecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						AuthorizationCode = new NSwag.OpenApiOAuthFlow
						{
							AuthorizationUrl = Configuration.GetSection("Identity:Authorization_endpoint").Value,
							TokenUrl = Configuration.GetSection("Identity:Token_endpoint").Value,
							Scopes = new Dictionary<string, string> { { Configuration.GetSection("Identity:APIName").Value, "Identity API" } }
						}
					}
				});

				options.OperationProcessors.Add(new OperationSecurityScopeProcessor("oauth2"));
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
			app.UseIdentityServer();

			app.UseOpenApi();
			app.UseSwaggerUi3(options =>
			{
				options.OAuth2Client = new OAuth2ClientSettings
				{
					ClientId = Configuration.GetSection("Identity:Client_id").Value,
					ClientSecret = Configuration.GetSection("Identity:API_secret").Value,
					AppName = Configuration.GetSection("Identity:APIName").Value,
					UsePkceWithAuthorizationCodeGrant = true
				};
			});
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
