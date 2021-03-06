﻿using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task = IssueTracker.Services.Issues.Domain.Entities.Task;

namespace IssueTracker.Services.Issues.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		public DbSet<Domain.Entities.Project> Projects { get; set; }
		public DbSet<Story> Stories { get; set; }
		public DbSet<Domain.Entities.Task> Tasks { get; set; }
		public DbSet<Domain.Entities.Bug> Bugs { get; set; }
		public DbSet<ProjectParticipants> ProjectParticipants { get; set; }
		public DbSet<Domain.Entities.Participant> Participants { get; set; }
		public DbSet<Domain.Entities.StoryParticipants> StoryParticipants { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
