using AutoMapper;
using IssueTracker.Services.Issues.Application.Common.Mappings;
using IssueTracker.Services.Issues.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Project.Queries.GetProjectParticipants
{
	public class ParticipantsListDto : IMapFrom<Domain.Entities.ProjectParticipants>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProjectParticipants, ParticipantsListDto>()
             .ForMember(p=>p.Id,opt=>opt.MapFrom(p=>p.ParticipantId))
           .ForMember(p => p.Name, opt => opt.MapFrom(p => p.Participant.Name))
               .ForMember(p => p.Email, opt => opt.MapFrom(p => p.Participant.Email))           
            .ReverseMap();
        }
    }

}
