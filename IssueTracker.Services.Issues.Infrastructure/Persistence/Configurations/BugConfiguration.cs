using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class BugConfiguration : IEntityTypeConfiguration<Bug>
	{
		public void Configure(EntityTypeBuilder<Bug> builder)
		{
			builder.HasKey(p => p.Id);
			builder.HasOne(p => p.Story).WithMany(p => p.Bugs).HasForeignKey(p => p.StoryId);
			builder.HasOne(p => p.Participant).WithMany(p => p.Bugs).HasForeignKey(p=>p.AssigneeId);
			builder.HasOne(p => p.Participant).WithOne().HasForeignKey<Bug>(p=>p.ReporterId);
			builder.Property(p => p.ReporterId).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

		}
	}
}
