using IssueTracker.Services.Issues.Domain.Common;
using IssueTracker.Services.Issues.Domain.Enums;
using IssueTracker.Services.Issues.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class Story:AuditableEntity,IIssue
	{
		public Story()
		{
			StoryParticipants = new List<StoryParticipants>();
			Bugs = new List<Bug>();
			Tasks = new List<Task>();

		}
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ReporterId { get; set; }
		public string AssigneeId { get; set; }
		public IssueStatus Status { get; set; }
		public float? StoryPoint { get; set; }
		public long ProjectId { get; set; }
		public Project Project { get; set; }
		public IList<Task> Tasks { get; set; }
		public IList<Bug> Bugs { get; set; }
		public IList<StoryParticipants> StoryParticipants { get; set; }
	}
}
