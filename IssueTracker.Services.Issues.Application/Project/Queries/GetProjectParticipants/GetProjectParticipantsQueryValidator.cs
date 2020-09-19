using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Project.Queries.GetProjectParticipants
{
	public class GetProjectParticipantsQueryValidator:AbstractValidator<GetProjectParticipantsQuery>
	{
		public GetProjectParticipantsQueryValidator()
		{
			RuleFor(p => p.ProjectId).NotEmpty().GreaterThan(0);
		}
	}
}
