using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using IssueTracker.Services.Issues.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Issue.Story.Commands.UpdateStory
{
	public class UpdateStoryCommand:IRequest
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public float? StoryPoints { get; set; }
		public string AssigneeId { get; set; }
		public IssueStatus Status { get; set; }
	}
	public class UpdateStoryCommandHandler : IRequestHandler<UpdateStoryCommand>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		public UpdateStoryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}
		public async Task<Unit> Handle(UpdateStoryCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Stories.FindAsync(request.Id);
			if (entity == null)
			{
				throw new NotFoundException(nameof(Story), request.Id);
			}
			entity.Description = request.Description;
			entity.Status = request.Status;
			entity.StoryPoint = request.StoryPoints;
			entity.Title = request.Title;
			entity.AssigneeId = request.AssigneeId;
			await _context.SaveChangesAsync(cancellationToken);
			return Unit.Value;
		}
	}
}
