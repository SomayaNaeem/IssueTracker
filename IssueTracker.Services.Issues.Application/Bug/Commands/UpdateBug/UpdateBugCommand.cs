using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Bug.Commands.UpdateBug
{
	public class UpdateBugCommand : Common.Models.Issue, IRequest
	{
		public string Id { get; set; }
		public string StepsToReplicate { get; set; }
	}
	public class UpdateBugCommandHandler : IRequestHandler<UpdateBugCommand>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		public UpdateBugCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}
		public async Task<Unit> Handle(UpdateBugCommand request, CancellationToken cancellationToken)
		{
			//var entity = await _context.Bugs.FindAsync(request.Id);
			var entity = await _context.Bugs.Include(s => s.Story).Include(s => s.Story.StoryParticipants).FirstOrDefaultAsync(s => s.Id == request.Id);

			if (entity == null)
			{
				throw new NotFoundException(nameof(Domain.Entities.Bug), request.Id);
			}
			var StoryParticipants = entity.Story.StoryParticipants;
			entity.Description = request.Description;
			entity.Status = request.Status;
			entity.Title = request.Title;
			entity.AssigneeId = request.AssigneeId;
			entity.ReplicateSteps = request.StepsToReplicate;

			if (StoryParticipants.ToList().Find(p => p.ParticipantId == request.AssigneeId) == null || StoryParticipants.Count() == 0)
			{
				StoryParticipants.Add(new Domain.Entities.StoryParticipants()
				{

					Story = entity.Story,
					ParticipantId = request.AssigneeId
				});
			}
			entity.Story.StoryParticipants = StoryParticipants;
			await _context.SaveChangesAsync(cancellationToken);
			return Unit.Value;
		}
	}
}
