using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Issue.Story.Commands.UpdateStory
{
	public class UpdateStoryCommandValidator : AbstractValidator<UpdateStoryCommand>
	{
		public UpdateStoryCommandValidator()
		{
			RuleFor(s => s.Title).NotEmpty();
			RuleFor(s => s.Id).NotEmpty();
		}
	}
}
