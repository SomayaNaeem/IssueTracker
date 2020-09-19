using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Bug.Commands.UpdateBug
{
	public class UpdateBugCommandValidator:AbstractValidator<UpdateBugCommand>
	{
		public UpdateBugCommandValidator()
		{
			RuleFor(b => b.Id).NotEmpty();
			RuleFor(b => b.Title).NotEmpty();
			RuleFor(b => b.Status).NotEmpty();
		}
	}
}
