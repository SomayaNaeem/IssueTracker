using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Task.Commands.UpdateTask
{
	public class UpdateTaskCommandValidator:AbstractValidator<UpdateTaskCommand>
	{
		public UpdateTaskCommandValidator()
		{
			RuleFor(t => t.Title).NotEmpty();
			RuleFor(t => t.Id).NotEmpty();
		}
	}
}
