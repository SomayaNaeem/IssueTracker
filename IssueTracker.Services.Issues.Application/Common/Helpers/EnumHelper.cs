using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Common.Helpers
{
	public static class EnumHelper
	{
		public enum IssueType
		{
			Story=1,
			Task=2,
			Bug=3
		}
	}
}
