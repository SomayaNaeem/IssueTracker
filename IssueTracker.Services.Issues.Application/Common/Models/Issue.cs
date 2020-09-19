using IssueTracker.Services.Issues.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Application.Common.Models
{
	public class Issue
	{
		//public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ParentId { get; set; }
		public string AssigneeId { get; set; }
		public IssueStatus Status { get; set; }
	}
}
