using IssueTracker.Services.Issues.Domain.Common;
using IssueTracker.Services.Issues.Domain.Enums;
using IssueTracker.Services.Issues.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class Bug : AuditableEntity, IIssue
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ReporterId { get; set; }
		public string AssigneeId { get; set; }
		public IssueStatus Status { get; set; }
		public string ReplicateSteps { get; set; }
		public string StoryId { get; set; }
		public Story Story { get; set; }
		public Participant Participant { get; set; }
	}
}
