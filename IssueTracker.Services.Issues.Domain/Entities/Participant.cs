using IssueTracker.Services.Issues.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class Participant:AuditableEntity
	{
		public Participant()
		{
			Bugs = new List<Bug>();
			Tasks = new List<Task>();
			StoryParticipants = new List<StoryParticipants>();
		}
		public string Id { get; set; }
		public string Name { get;set; }
		public string Email { get; set; }
		public IList<ProjectParticipants> ProjectParticipants { get; set; }
		public IList<StoryParticipants> StoryParticipants { get; set; }
		public IList<Bug> Bugs { get; set; }
		public IList<Task> Tasks { get; set; }
	}
}
