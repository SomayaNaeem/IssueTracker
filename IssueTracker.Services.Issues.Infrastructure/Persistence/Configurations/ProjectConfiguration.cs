using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Issues.Infrastructure.Persistence.Configurations
{
	public class ProjectConfiguration : IEntityTypeConfiguration<Project>
	{
		public void Configure(EntityTypeBuilder<Project> builder)
		{
			builder.HasIndex(p => p.Key).IsUnique();
			builder.Property(p => p.Key).Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
			builder.HasKey(p => p.Id);
			builder.HasMany(p => p.Stories).WithOne(p => p.Project).HasForeignKey(p => p.ProjectId);
		}
	}
}
