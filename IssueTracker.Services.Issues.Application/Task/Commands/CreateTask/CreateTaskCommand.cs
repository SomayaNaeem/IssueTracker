using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Helpers;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using IssueTracker.Services.Issues.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace IssueTracker.Services.Issues.Application.Task.Commands.CreateTask
{
	public class CreateTaskCommand : Common.Models.Issue, IRequest<string>
	{
		
	}
	public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, string>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		public CreateTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}
		public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
		{
			var story = _context.Stories.Include(p=>p.Project).FirstOrDefault(p => p.Id == request.ParentId);
			if (story == null)
			{
				throw new NotFoundException(nameof(Story), request.ParentId);
			}
			//add task
			var newTask = new Domain.Entities.Task()
			{
				Title = request.Title,
				Description = request.Description,
				Status = IssueStatus.Unassigned,
				StoryId = request.ParentId,
				Id = Helper.GenerateIssueId(story.Project.Key),
				ReporterId = _currentUserService.UserId
			};
			_context.Tasks.Add(newTask);
			await _context.SaveChangesAsync(cancellationToken);

			return newTask.Id;
		}
	}
}
