using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Bug.Commands.CreateBug
{
	public class CreateBugCommandValidator:AbstractValidator<CreateBugCommand>
	{
		public CreateBugCommandValidator()
		{
			RuleFor(b => b.Title).NotEmpty();
			RuleFor(b => b.ParentId).NotEmpty();
		}
	}
}
