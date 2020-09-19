using AutoMapper;
using AutoMapper.QueryableExtensions;
using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Issue.Queries.GetIssues
{
	public class GetIssuesQuery:IRequest<IssuesListDto>
	{
		public string Id { get; set; }
		public long ProjectId { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}
	public class GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, IssuesListDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		public GetIssuesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
		{
			_context = context;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}
		public async Task<IssuesListDto> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
		{
			var project = _context.Projects.FirstOrDefault(p => p.Id == request.ProjectId);
			if (project == null)
			{
				throw new NotFoundException(nameof(Project), request.ProjectId);
			}
			var ParticipantIssues =await _context.Participants.Include(p=>p.Bugs).Include(p=>p.Tasks).Include(p=>p.StoryParticipants).ThenInclude(p=>p.Story).Where(p=>p.Id==request.Id)
				.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize)
				.FirstOrDefaultAsync(cancellationToken);
			IssuesListDto issuesListDto = new IssuesListDto();
			issuesListDto.Bugs = _mapper.Map<List<IssueDto>>(ParticipantIssues.Bugs);
			issuesListDto.Tasks = _mapper.Map<List<IssueDto>>(ParticipantIssues.Tasks);
			issuesListDto.Stories = ParticipantIssues.StoryParticipants.Select(s => s.Story).Select(x =>new  IssueDto() { AssigneeId = x.AssigneeId, Description = x.Description, ParentId = x.ProjectId.ToString(), Status = x.Status, Title = x.Title ,Id=x.Id}).ToList();
			return issuesListDto;
		}
	}
}
