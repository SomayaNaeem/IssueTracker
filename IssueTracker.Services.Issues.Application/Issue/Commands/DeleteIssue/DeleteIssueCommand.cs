using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IssueTracker.Services.Issues.Application.Common.Helpers.EnumHelper;
using Task = IssueTracker.Services.Issues.Domain.Entities.Task;

namespace IssueTracker.Services.Issues.Application.Issue.Commands.DeleteIssue
{
	public class DeleteIssueCommand:IRequest
	{
		public string Id { get; set; }
		public IssueType IssueType { get; set; }
	}
	public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		public DeleteIssueCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}
		public async Task<Unit> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
		{
			switch (request.IssueType)
			{
				case IssueType.Story:
					var story = await _context.Stories.FindAsync(request.Id);
					if (story == null)
					{
						throw new NotFoundException(nameof(Story), request.Id);
					}
					_context.Stories.Remove(story);
					break;
				case IssueType.Task:
					var task = await _context.Tasks.FindAsync(request.Id);
					if (task == null)
					{
						throw new NotFoundException(nameof(Task), request.Id);
					}
					_context.Tasks.Remove(task);
					break;
				case IssueType.Bug:
					var bug = await _context.Bugs.FindAsync(request.Id);
					if (bug == null)
					{
						throw new NotFoundException(nameof(Bug), request.Id);
					}
					_context.Bugs.Remove(bug);
					break;
				default:
					break;
			}
			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
