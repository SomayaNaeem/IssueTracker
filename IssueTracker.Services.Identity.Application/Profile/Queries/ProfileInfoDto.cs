using IssueTracker.Services.Identity.Application.Common.Mappings;
using IssueTracker.Services.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Application.Profile.Queries
{
	public class ProfileInfoDto:IMapFrom<ApplicationUser>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<ApplicationUser, ProfileInfoDto>();			
		}
	}
}
