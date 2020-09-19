using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Participant.Commands.DeleteParticipant
{
	public class DeleteParticipantCommandValidator:AbstractValidator<DeleteParticipantCommand>
	{
		public DeleteParticipantCommandValidator()
		{
			RuleFor(p => p.Email).NotEmpty();
			RuleFor(p => p.ProjectId).NotEmpty().GreaterThan(0);
		}
	}
}
