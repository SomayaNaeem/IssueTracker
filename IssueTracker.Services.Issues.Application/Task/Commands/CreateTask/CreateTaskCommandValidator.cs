using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Task.Commands.CreateTask
{
	public class CreateTaskCommandValidator:AbstractValidator<CreateTaskCommand>
	{
		public CreateTaskCommandValidator()
		{
			RuleFor(t => t.Title).NotEmpty();
			RuleFor(t => t.ParentId).NotEmpty();
		}
	}
}
