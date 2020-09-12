using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Application.SignUp.Commands
{
	public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
	{
		public SignUpCommandValidator()
		{
			RuleFor(s => s.Email).NotEmpty().EmailAddress();
			RuleFor(s => s.Name).NotEmpty();
			RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
		}
	}
}
