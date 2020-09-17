using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class ProjectParticipantsConfiguration : IEntityTypeConfiguration<ProjectParticipants>
	{
		public void Configure(EntityTypeBuilder<ProjectParticipants> builder)
		{
			builder.HasKey(p => new { p.ProjectId, p.ParticipantId });
			builder.HasOne(p => p.Participant).WithMany(p => p.ProjectParticipants).HasForeignKey(p => p.ParticipantId);
			builder.HasOne(p => p.Project).WithMany(p => p.ProjectParticipants).HasForeignKey(p => p.ProjectId);

		}
	}
}
