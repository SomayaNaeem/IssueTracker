using IssueTracker.Services.Issues.Application.Common.Helpers;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
namespace IssueTracker.Services.Issues.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<long>
    {
        public string Name { get; set; }
    }
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public CreateProjectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<long> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            //add project
            var project = new Domain.Entities.Project()
            {
                Name = request.Name,
                Key = Helper.GenerateRandomString(),
                OwnerId = _currentUserService.UserId
            };

            //add participant if not exist
            var Participant = _context.Participants.FirstOrDefault(p => p.Id == _currentUserService.UserId);
            if (Participant==null)
            {
                Participant = new Participant()
                {
                    Name = _currentUserService.FullName,
                    Id = _currentUserService.UserId,
                    Email = _currentUserService.Email
                };
                _context.Participants.Add(Participant);
            }
            project.ProjectParticipants = new List<ProjectParticipants>() {
            new ProjectParticipants()
            {
                Project=project,
                Participant=Participant
            }
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            return project.Id;

        }
    }
}
