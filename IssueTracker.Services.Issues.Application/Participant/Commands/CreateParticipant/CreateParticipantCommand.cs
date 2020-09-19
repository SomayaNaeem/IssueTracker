using IssueTracker.Services.Issues.Application.Common.Exceptions;
using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Entities;
using IssueTracker.Services.Issues.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static IssueTracker.Services.Issues.Application.Common.Helpers.EnumHelper;
using Task = IssueTracker.Services.Issues.Domain.Entities.Task;

namespace IssueTracker.Services.Issues.Application.Participant.Commands.CreateParticipant
{
	public class CreateParticipantCommand:IRequest<string>
	{
		public string Email { get; set; }
		public string ProjectId { get; set; }
	}
	public class CreateParticipantCommandHandler : IRequestHandler<CreateParticipantCommand, string>
	{
		private readonly IApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;
		private readonly IParticipantService _participantService;
		public CreateParticipantCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IParticipantService participantService)
		{
			_context = context;
			_currentUserService = currentUserService;
			_participantService = participantService;
		}
		public async Task<string> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
		{
			var project = _context.Projects.FirstOrDefault(p => p.Id == long.Parse(request.ProjectId));
			if (project == null)
			{
				throw new NotFoundException(nameof(Domain.Entities.Project), request.ProjectId);
			}
			var participant = await GetParticipant(request.Email);
			project.ProjectParticipants.Add(new ProjectParticipants()
			{
				Project = project,
				Participant = participant
			});
			await _context.SaveChangesAsync(cancellationToken);
			return participant.Id;
		}

		public async Task<Domain.Entities.Participant> GetParticipant(string email)
		{
			var entity = await _context.Participants.FirstOrDefaultAsync(p=>p.Email==email);
			if (entity==null)
			{
				//Get Participant from users as http call, later we can do it with RabbitMq
				
				entity = await _participantService.GetParticipant(email);
			}
			return entity;
		}
	}
}
