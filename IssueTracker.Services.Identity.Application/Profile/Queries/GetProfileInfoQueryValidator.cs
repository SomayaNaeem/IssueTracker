using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Application.Profile.Queries
{
	public class GetProfileInfoQueryValidator:AbstractValidator<GetProfileInfoQuery>
	{
		public GetProfileInfoQueryValidator()
		{
			RuleFor(p => p.Email).NotEmpty();
		}
	}
}
