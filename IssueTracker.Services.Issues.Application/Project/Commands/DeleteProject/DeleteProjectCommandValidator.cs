using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Project.Commands.DeleteProject
{
	public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
	{
		public DeleteProjectCommandValidator()
		{
			RuleFor(p => p.Id).NotEmpty();
		}
	}
}
