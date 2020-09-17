using IssueTracker.Services.Issues.Domain.Common;

namespace IssueTracker.Services.Issues.Domain.Entities
{
	public class ProjectParticipants:AuditableEntity
	{
		public string ParticipantId { get; set; }
		public long ProjectId { get; set; }
		public Participant Participant { get; set; }
		public Project Project { get; set; }
	}
}
