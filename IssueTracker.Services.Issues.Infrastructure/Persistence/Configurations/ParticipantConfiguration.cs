using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
	{
		public void Configure(EntityTypeBuilder<Participant> builder)
		{
			builder.HasKey(p => p.Id);
			builder.HasMany(p => p.Bugs).WithOne(p => p.Participant).HasForeignKey(p => p.AssigneeId).OnDelete(DeleteBehavior.SetNull);
			builder.HasMany(p => p.Tasks).WithOne(p => p.Participant).HasForeignKey(p => p.AssigneeId).OnDelete(DeleteBehavior.SetNull);
			//builder.HasMany(p => p.StoryParticipants).WithOne(p => p.Participant).HasForeignKey(p => p.StoryId).OnDelete(DeleteBehavior.SetNull);

		}
	}
}
