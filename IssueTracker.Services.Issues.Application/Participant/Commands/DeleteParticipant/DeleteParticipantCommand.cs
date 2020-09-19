using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Application.Participant.Commands.DeleteParticipant
{
	public class DeleteParticipantCommand : IRequest
	{
		public string Email { get; set; }
        public long ProjectId { get; set; }
    }
    public class DeleteParticipantCommandHandler : IRequestHandler<DeleteParticipantCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public DeleteParticipantCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<Unit> Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Participants.Include(p=>p.ProjectParticipants).FirstOrDefaultAsync(p=>p.Email ==request.Email);
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Participant), request.Email);
            }
            if (project.OwnerId != _currentUserService.UserId)
            {
                throw new AccessDeniedException(nameof(Project), request.ProjectId);
            }
            var projectParticipaant = entity.ProjectParticipants.FirstOrDefault(p=>p.ParticipantId==entity.Id && p.ParticipantId!=project.OwnerId);
            _context.ProjectParticipants.Remove(projectParticipaant);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
