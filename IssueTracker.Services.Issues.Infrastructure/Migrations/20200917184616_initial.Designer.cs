﻿// <auto-generated />
using System;
using IssueTracker.Services.Issues.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IssueTracker.Services.Issues.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200917184616_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Bug", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssigneeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReplicateSteps")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReporterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("ReporterId")
                        .IsUnique()
                        .HasFilter("[ReporterId] IS NOT NULL");

                    b.HasIndex("StoryId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Participant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasFilter("[Key] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.ProjectParticipants", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ProjectParticipants");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Story", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssigneeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("ReporterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float?>("StoryPoint")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Stories");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Task", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssigneeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParticipantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReporterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.HasIndex("ReporterId")
                        .IsUnique()
                        .HasFilter("[ReporterId] IS NOT NULL");

                    b.HasIndex("StoryId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Bug", b =>
                {
                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Participant", null)
                        .WithMany("Bugs")
                        .HasForeignKey("ParticipantId");

                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Participant", "Participant")
                        .WithOne()
                        .HasForeignKey("IssueTracker.Services.Issues.Domain.Entities.Bug", "ReporterId");

                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Story", "Story")
                        .WithMany("Bugs")
                        .HasForeignKey("StoryId");
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.ProjectParticipants", b =>
                {
                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Participant", "Participant")
                        .WithMany("ProjectParticipants")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Project", "Project")
                        .WithMany("ProjectParticipants")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Story", b =>
                {
                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Participant", null)
                        .WithMany("Stories")
                        .HasForeignKey("ParticipantId");

                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Project", "Project")
                        .WithMany("Stories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueTracker.Services.Issues.Domain.Entities.Task", b =>
                {
                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Participant", null)
                        .WithMany("Tasks")
                        .HasForeignKey("ParticipantId");

                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Participant", "Participant")
                        .WithOne()
                        .HasForeignKey("IssueTracker.Services.Issues.Domain.Entities.Task", "ReporterId");

                    b.HasOne("IssueTracker.Services.Issues.Domain.Entities.Story", "Story")
                        .WithMany("Tasks")
                        .HasForeignKey("StoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
