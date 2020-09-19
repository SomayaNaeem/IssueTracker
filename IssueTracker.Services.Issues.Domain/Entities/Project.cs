using IssueTracker.Services.Issues.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class Project:AuditableEntity
	{
		public Project()
		{
			ProjectParticipants = new List<ProjectParticipants>();
		}
		public long Id { get; set; }
		public string Name { get; set; }
		public string Key { get; set; }
		public string OwnerId { get; set; }
		public IList<ProjectParticipants> ProjectParticipants { get; set; }
		public IList<Story> Stories { get; set; }
	}
}
