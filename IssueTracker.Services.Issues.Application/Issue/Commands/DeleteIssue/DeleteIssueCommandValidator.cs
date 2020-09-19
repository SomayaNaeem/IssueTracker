using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Issue.Commands.DeleteIssue
{
	public class DeleteIssueCommandValidator:AbstractValidator<DeleteIssueCommand>
	{
		public DeleteIssueCommandValidator()
		{
			RuleFor(i => i.Id).NotEmpty();
			RuleFor(i => i.IssueType).NotEmpty();
		}
	}
}
