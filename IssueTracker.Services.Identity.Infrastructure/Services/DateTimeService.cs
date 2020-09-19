using IssueTracker.Services.Identity.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Infrastructure.Services
{
	public class DateTimeService : IDateTime
	{
		public DateTime Now => DateTime.Now;
	}
}
