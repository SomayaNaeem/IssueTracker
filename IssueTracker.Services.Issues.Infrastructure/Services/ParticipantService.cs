﻿using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Infrastructure
{
	public class ParticipantService: IParticipantService
	{
		private readonly IHttpClientFactory httpClient;
		IConfiguration configuration;
		public ParticipantService(IHttpClientFactory _httpClient, IConfiguration _configuration)
		{
			httpClient = _httpClient;
			configuration = _configuration;
		}

		public async Task<Domain.Entities.Participant> GetParticipant(string email)
		{
			using (var request = new HttpRequestMessage(HttpMethod.Get, configuration["Identity:GetUserInfoUrl"] + email))
			{
				var client = httpClient.CreateClient();
				using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
				{
					if (response.IsSuccessStatusCode)
					{
						var responseJson = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<Domain.Entities.Participant>(responseJson);

					}
				}
				return null;
			}
		}
	}
}
