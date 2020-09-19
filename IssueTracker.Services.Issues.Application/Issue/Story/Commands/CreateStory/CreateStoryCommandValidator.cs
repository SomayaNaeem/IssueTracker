using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Issue.Story.Commands.CreateStory
{
	public class CreateStoryCommandValidator: AbstractValidator<CreateStoryCommand>
	{
		public CreateStoryCommandValidator()
		{
			RuleFor(s => s.ParentId).NotEmpty();
			RuleFor(s => s.Title).NotEmpty();
		}
	}
}
