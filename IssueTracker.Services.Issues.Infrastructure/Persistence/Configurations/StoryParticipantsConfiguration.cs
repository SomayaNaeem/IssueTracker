using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class StoryParticipantsConfiguration : IEntityTypeConfiguration<StoryParticipants>
	{
		public void Configure(EntityTypeBuilder<StoryParticipants> builder)
		{
			builder.HasKey(p => new { p.StoryId, p.ParticipantId });
			builder.HasOne(p => p.Participant).WithMany(p => p.StoryParticipants).HasForeignKey(p => p.ParticipantId);
			builder.HasOne(p => p.Story).WithMany(p => p.StoryParticipants).HasForeignKey(p => p.StoryId);

		}
	}
}
