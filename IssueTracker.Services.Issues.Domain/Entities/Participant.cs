using IssueTracker.Services.Issues.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class Participant:AuditableEntity
	{
		public string Id { get; private set; }
		public string Name { get; private set; }
		public string Email { get; private set; }
		public IList<ProjectParticipants> ProjectParticipants { get; set; }
		public IList<Story> Stories { get; set; }
		public IList<Bug> Bugs { get; set; }
		public IList<Task> Tasks { get; set; }
	}
}
