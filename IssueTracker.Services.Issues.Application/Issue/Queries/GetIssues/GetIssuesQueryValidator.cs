using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Issue.Queries.GetIssues
{
	public class GetIssuesQueryValidator:AbstractValidator<GetIssuesQuery>
	{
		public GetIssuesQueryValidator()
		{
			RuleFor(i => i.Id).NotEmpty();
		}
	}
}
