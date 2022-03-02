﻿// <auto-generated />
using System;
using Backend___team_1.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend___team_1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend___team_1.Features.Clients.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ContactPersonId")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ContactPersonId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Backend___team_1.Features.Projects.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ManagerId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Backend___team_1.Features.Teams.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GitHubLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .HasColumnType("text");

                    b.Property<string>("TeamLeaderId")
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserProfileId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamLeaderId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Backend___team_1.Features.UserProfiles.UserProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FacebookLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Backend___team_1.Features.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("WorkshopId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("WorkshopId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Backend___team_1.Features.Workshops.Workshop", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("CoverImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Presentation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TrainerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Workshops");
                });

            modelBuilder.Entity("Backend___team_1.Features.Clients.Client", b =>
                {
                    b.HasOne("Backend___team_1.Features.Users.User", "ContactPerson")
                        .WithMany()
                        .HasForeignKey("ContactPersonId");

                    b.Navigation("ContactPerson");
                });

            modelBuilder.Entity("Backend___team_1.Features.Projects.Project", b =>
                {
                    b.HasOne("Backend___team_1.Features.Clients.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend___team_1.Features.Users.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.Navigation("Client");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Backend___team_1.Features.Teams.Team", b =>
                {
                    b.HasOne("Backend___team_1.Features.Projects.Project", null)
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Backend___team_1.Features.Users.User", "TeamLeader")
                        .WithMany()
                        .HasForeignKey("TeamLeaderId");

                    b.HasOne("Backend___team_1.Features.UserProfiles.UserProfile", null)
                        .WithMany("Teams")
                        .HasForeignKey("UserProfileId");

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("Backend___team_1.Features.UserProfiles.UserProfile", b =>
                {
                    b.HasOne("Backend___team_1.Features.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend___team_1.Features.Users.User", b =>
                {
                    b.HasOne("Backend___team_1.Features.Workshops.Workshop", null)
                        .WithMany("Participants")
                        .HasForeignKey("WorkshopId");
                });

            modelBuilder.Entity("Backend___team_1.Features.Workshops.Workshop", b =>
                {
                    b.HasOne("Backend___team_1.Features.Users.User", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("Backend___team_1.Features.Projects.Project", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Backend___team_1.Features.UserProfiles.UserProfile", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Backend___team_1.Features.Workshops.Workshop", b =>
                {
                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
