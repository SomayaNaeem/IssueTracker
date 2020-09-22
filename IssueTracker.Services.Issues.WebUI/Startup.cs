using System.Collections.Generic;
using IdentityServer4.AccessTokenValidation;
using IssueTracker.Services.Issues.Application;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Infrastructure;
using IssueTracker.Services.Issues.WebUI.Filters;
using IssueTracker.Services.Issues.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Generation.Processors.Security;
using NSwag;
using NSwag.AspNetCore;
using OpenApiOAuthFlows = NSwag.OpenApiOAuthFlows;
using OpenApiSecurityScheme = NSwag.OpenApiSecurityScheme;

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
			services.AddHttpClient();
			services.AddScoped<ICurrentUserService, CurrentUserService>();
			services.AddScoped<IParticipantService, ParticipantService>();

			services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
			.AddOAuth2Introspection(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
			{
				options.Authority = Configuration.GetSection("Identity:IdentityAuthUrl").Value;

				// this maps to the API resource name and secret
				options.ClientId = Configuration.GetSection("Identity:APIName").Value;
				options.ClientSecret = Configuration.GetSection("Identity:API_secret").Value;
			});

			services.AddOpenApiDocument(options =>
			{
				options.DocumentName = "v1";
				options.Title = "Issues API";
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
							Scopes = new Dictionary<string, string> { { Configuration.GetSection("Identity:APIName").Value, "Issues API" } }
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
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseCors("CorsPolicy");
			app.UseRouting();
			
			app.UseAuthentication();
			app.UseAuthorization();
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
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

}
