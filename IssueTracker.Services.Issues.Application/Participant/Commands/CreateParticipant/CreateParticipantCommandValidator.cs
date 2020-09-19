using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Participant.Commands.CreateParticipant
{
	public class CreateParticipantCommandValidator : AbstractValidator<CreateParticipantCommand>
	{
		public CreateParticipantCommandValidator()
		{
			RuleFor(p =>p.Email ).NotEmpty();
			RuleFor(p => p.ProjectId).NotEmpty();
		}
	}
}
