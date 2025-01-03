﻿// <auto-generated />
using System;
using DrivingSchool.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DrivingSchool.Persistence.Migrations
{
    [DbContext(typeof(DrivingSchoolDbContext))]
    [Migration("20241204051948_AddCandidateFieldToTestResult")]
    partial class AddCandidateFieldToTestResult
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DrivingSchool.Domain.Entities.AnswerOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("PointValue")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TestId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.TestResult", b =>
                {
                    b.Property<Guid>("TestId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CandidateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TakenAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("TestId");

                    b.HasIndex("CandidateId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Candidate", b =>
                {
                    b.HasBaseType("DrivingSchool.Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Candidate");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Moderator", b =>
                {
                    b.HasBaseType("DrivingSchool.Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Moderator");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.AnswerOption", b =>
                {
                    b.HasOne("DrivingSchool.Domain.Entities.Question", null)
                        .WithMany("AnswerOptions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Question", b =>
                {
                    b.HasOne("DrivingSchool.Domain.Entities.Test", null)
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.TestResult", b =>
                {
                    b.HasOne("DrivingSchool.Domain.Entities.Candidate", null)
                        .WithMany("TestResults")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Question", b =>
                {
                    b.Navigation("AnswerOptions");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("DrivingSchool.Domain.Entities.Candidate", b =>
                {
                    b.Navigation("TestResults");
                });
#pragma warning restore 612, 618
        }
    }
}
