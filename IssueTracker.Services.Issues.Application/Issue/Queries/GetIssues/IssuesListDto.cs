using AutoMapper;
using IssueTracker.Services.Issues.Application.Common.Mappings;
using IssueTracker.Services.Issues.Application.Common.Models;
using IssueTracker.Services.Issues.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static IssueTracker.Services.Issues.Application.Common.Helpers.EnumHelper;

namespace IssueTracker.Services.Issues.Application.Issue.Queries.GetIssues
{
	public class IssuesListDto:IMapFrom<IssueDto>
	{
		public List<IssueDto> Bugs { get; set; }
		public List<IssueDto> Tasks { get; set; }
		public List<IssueDto> Stories { get; set; }

		public void Mapping(Profile profile)
		{

			profile.CreateMap<Domain.Entities.StoryParticipants, IssueDto>()
				.ForMember(p => p.ParentId, opt => opt.MapFrom(p => p.Story.ProjectId))
				.ForMember(p => p.Title, opt => opt.MapFrom(p => p.Story.Title))
				.ForMember(p => p.Description, opt => opt.MapFrom(p => p.Story.Description))
				.ForMember(p => p.AssigneeId, opt => opt.MapFrom(p => p.Story.AssigneeId))
				.ReverseMap();
			profile.CreateMap<Domain.Entities.Bug, IssueDto>()
				.ForMember(p => p.ParentId, opt => opt.MapFrom(p => p.StoryId))
				.ReverseMap();
			profile.CreateMap<Domain.Entities.Task, IssueDto>()
				.ForMember(p => p.ParentId, opt => opt.MapFrom(p => p.StoryId))
				.ReverseMap();
			profile.CreateMap<Domain.Entities.Story, IssueDto>()
				.ForMember(p => p.ParentId, opt => opt.MapFrom(p => p.ProjectId))
				.PreserveReferences();
			
		}
	}
}
