using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Common.Exceptions
{
	public class AccessDeniedException:Exception
	{
		public AccessDeniedException(string name, object key)
		   : base($"Entity \"{name}\" ({key}) delete access denied.")
		{
		}
	}
}
