using AutoMapper;
using AutoMapper.QueryableExtensions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Project.Queries.GetProjectParticipants
{
	public class GetProjectParticipantsQuery:IRequest<PagedList<ParticipantsListDto>>
	{
		public long ProjectId { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}
	public class GetProjectParticipantsQueryHandler : IRequestHandler<GetProjectParticipantsQuery, PagedList<ParticipantsListDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		public GetProjectParticipantsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
		{
			_context = context;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}
		public async Task<PagedList<ParticipantsListDto>> Handle(GetProjectParticipantsQuery request, CancellationToken cancellationToken)
		{
			var participants = await _context.ProjectParticipants.Include(p => p.Participant).Where(p => p.ProjectId == request.ProjectId)				
				.ProjectTo<ParticipantsListDto>(_mapper.ConfigurationProvider)
				.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize)
				.ToListAsync(cancellationToken);

			return new PagedList<ParticipantsListDto>(participants, request.PageSize, request.PageNumber);
		}
	}
}
