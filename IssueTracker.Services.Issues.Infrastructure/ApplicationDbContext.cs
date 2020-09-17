using IssueTracker.Services.Issues.Application.Common.Interfaces;
using IssueTracker.Services.Issues.Domain.Common;
using IssueTracker.Services.Issues.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Services.Issues.Infrastructure
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTime _dateTime;

		public ApplicationDbContext(
			DbContextOptions options,
			ICurrentUserService currentUserService,
			IDateTime dateTime) : base(options)
		{
			_currentUserService = currentUserService;
			_dateTime = dateTime;
		}
		public DbSet<Project> Projects { get; set; }
		public DbSet<Story> Stories { get; set; }
		public DbSet<Domain.Entities.Task> Tasks { get; set; }
		public DbSet<Bug> Bugs { get; set; }
		public DbSet<ProjectParticipants> ProjectParticipants { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;

                    case EntityState.Deleted:
                        if (entry.CurrentValues["IsDeleted"].ToString() == "False")
                        {
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.Entity.LastModifiedBy = _currentUserService.UserId;
                            entry.Entity.LastModified = _dateTime.Now;
                        }

                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Story>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Domain.Entities.Task>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Bug>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<Participant>().HasQueryFilter(p => !p.IsDeleted);
            builder.Entity<ProjectParticipants>().HasQueryFilter(p => !p.IsDeleted);
            base.OnModelCreating(builder);


        }
    }
}
