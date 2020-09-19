using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class TaskConfiguration : IEntityTypeConfiguration<Task>
	{
		public void Configure(EntityTypeBuilder<Task> builder)
		{
			builder.HasKey(p => p.Id);
			builder.HasOne(p => p.Story).WithMany(p => p.Tasks).HasForeignKey(p => p.StoryId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(p => p.Participant).WithMany(p => p.Tasks).HasForeignKey(p => p.AssigneeId).OnDelete(DeleteBehavior.SetNull);
			//builder.HasOne(p => p.Participant).WithOne().HasForeignKey<Task>(p => p.ReporterId);
			builder.Property(p => p.ReporterId).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

		}
	}
}
