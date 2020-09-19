using IssueTracker.Services.Issues.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Services
{
	public class DateTimeService : IDateTime
	{
		public DateTime Now => DateTime.Now;
	}
}
