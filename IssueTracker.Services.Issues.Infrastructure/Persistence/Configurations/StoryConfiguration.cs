using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class StoryConfiguration : IEntityTypeConfiguration<Story>
	{
		public void Configure(EntityTypeBuilder<Story> builder)
		{
			builder.HasKey(p => p.Id);
			builder.HasOne(p => p.Project).WithMany(p => p.Stories).HasForeignKey(p => p.ProjectId);
			//builder.HasOne(p => p.Participant).WithOne().HasForeignKey<Story>(p => p.ReporterId);
			builder.Property(p => p.ReporterId).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
			builder.Ignore(p => p.Participants);
		}
	}
}
