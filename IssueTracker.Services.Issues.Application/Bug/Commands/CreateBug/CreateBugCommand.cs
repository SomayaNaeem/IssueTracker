using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Helpers;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Bug.Commands.CreateBug
{
	public class CreateBugCommand : Common.Models.Issue, IRequest<string>
	{
		public string StepsToReplicate { get; set; }
	}
	public class CreateBugCommandHandler : IRequestHandler<CreateBugCommand, string>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		public CreateBugCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}
		public async Task<string> Handle(CreateBugCommand request, CancellationToken cancellationToken)
		{
			var story = _context.Stories.Include(p=>p.Project).FirstOrDefault(p => p.Id == request.ParentId);
			if (story == null)
			{
				throw new NotFoundException(nameof(Domain.Entities.Story), request.ParentId);
			}
			//add bug
			var newBug = new Domain.Entities.Bug()
			{
				Title = request.Title,
				Description = request.Description,
				Status = IssueStatus.Unassigned,
				StoryId = request.ParentId,
				Id = Helper.GenerateIssueId(story.Project.Key),
				ReporterId = _currentUserService.UserId,
				ReplicateSteps=request.StepsToReplicate
			};
			_context.Bugs.Add(newBug);
			await _context.SaveChangesAsync(cancellationToken);

			return newBug.Id;
		}
	}
}
