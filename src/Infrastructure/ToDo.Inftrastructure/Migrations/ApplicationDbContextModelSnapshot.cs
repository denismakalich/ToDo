﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ToDo.Inftrastructure.Context;

#nullable disable

namespace ToDo.Inftrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToDo.Domain.Entities.TaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_on");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("task_items", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e944e01b-e0b5-4df6-8400-ec64f2c52bc2"),
                            CreatedOn = new DateTime(2024, 7, 1, 20, 19, 6, 853, DateTimeKind.Utc).AddTicks(6955),
                            Description = "test some descriptions",
                            ModifiedOn = new DateTime(2024, 7, 1, 20, 19, 6, 853, DateTimeKind.Utc).AddTicks(7049),
                            Priority = 10,
                            Status = (byte)1,
                            Title = "testing title",
                            UserId = new Guid("1f0ca162-4c04-4c2f-bbda-86a91f6d1768")
                        });
                });

            modelBuilder.Entity("ToDo.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("passwordHash");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f0ca162-4c04-4c2f-bbda-86a91f6d1768"),
                            Email = "admin@mail.ru",
                            NormalizedEmail = "ADMIN@MAIL.RU",
                            PasswordHash = "BBvXOSMA973cWH1tPgNqZ6rJgLyDXp3/mwhPv0p+EosYyuttP/B6p17KyHIUeNvcmDudHgrleqp/tNCNFekDvg==",
                            Salt = "BOYxetCaoZfHflOZj6CvW+thpkFZoAfN0DQM7KXDVcJ5aZ6o+qOhTQbT1qWzwavP9X6dxJLP2RoXPTtD676JFSDsCUyWSdFh/+DkFvCSmjqvlxqQk7bbJ+W13Gej1NMXDjGybDxc9SYH+j59LHLbE9WLt9mE+FRUieULHWzg37Y="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
