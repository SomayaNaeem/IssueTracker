using IssueTracker.Services.Issues.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class StoryParticipants: AuditableEntity
	{
		//public StoryParticipants()
		//{
		//	Story = new Story();
		//	Participant = new Participant();
		//}
		public string ParticipantId { get; set; }
		public string StoryId { get; set; }
		public Participant Participant { get; set; }
		public Story Story { get; set; }
	}
}
