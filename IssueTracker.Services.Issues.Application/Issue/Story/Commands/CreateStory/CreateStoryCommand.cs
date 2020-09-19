using IssueTracker.Services.Issues.Application.Common.Helpers;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Enums;
using IssueTracker.Services.Issues.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using IssueTracker.Services.Issues.Application.Common.Exceptions;

namespace IssueTracker.Services.Issues.Application.Issue.Story.Commands.CreateStory
{
	public class CreateStoryCommand : Common.Models.Issue, IRequest<string>
	{
		public float? StoryPoints { get; set; }
	}
    public class CreateStoryCommandHandler : IRequestHandler<CreateStoryCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public CreateStoryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateStoryCommand request, CancellationToken cancellationToken)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == long.Parse(request.ParentId));
            if (project==null)
            {
                throw new NotFoundException(nameof(Project), request.ParentId);
            }
            //add story
            var Story = new Domain.Entities.Story()
            {
                Title = request.Title,
                Description=request.Description,
                StoryPoint=request.StoryPoints,
                Status=IssueStatus.Unassigned,
                ProjectId=long.Parse(request.ParentId),
                Id = Helper.GenerateIssueId(project.Key),
                ReporterId = _currentUserService.UserId
            };
            _context.Stories.Add(Story);
            await _context.SaveChangesAsync(cancellationToken);

            return Story.Id;

        }
    }
}
