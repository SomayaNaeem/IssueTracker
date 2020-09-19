using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IssueTracker.Services.Issues.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using IssueTracker.Services.Issues.Domain.Entities;

namespace IssueTracker.Services.Issues.Application.Task.Commands.UpdateTask
{
	public class UpdateTaskCommand : Common.Models.Issue, IRequest
	{
		public string Id { get; set; }
	}
	public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		public UpdateTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}
		public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = await _context.Tasks.Include(s => s.Story).Include(s=>s.Story.StoryParticipants).FirstOrDefaultAsync(s => s.Id == request.Id);
			if (entity == null)
			{
				throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);
			}
			var StoryParticipants = entity.Story.StoryParticipants;
			entity.Description = request.Description;
			entity.Status = request.Status;
			entity.Title = request.Title;
			entity.AssigneeId = request.AssigneeId;
			//if (StoryParticipants.Count()>0)
			//{
			//	StoryParticipants = new List<StoryParticipants>();
			//	entity.Story.StoryParticipants = new List<IssueTracker.Services.Issues.Domain.Entities.StoryParticipants>() { new Domain.Entities.StoryParticipants() {
			//		Story = entity.Story,
			//		ParticipantId = request.AssigneeId
			//	} };
			//}
			if (StoryParticipants.ToList().Find(p=>p.ParticipantId ==request.AssigneeId)==null || StoryParticipants.Count()== 0)
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
