﻿// <auto-generated />
using System;
using ASP_NotesApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASP_NotesApp.Migrations
{
    [DbContext(typeof(NoteAppDBContext))]
    partial class NoteAppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ASP_NotesApp.Models.Domain.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NoteStatusId")
                        .HasColumnType("int");

                    b.Property<bool>("Pined")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NoteStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("ASP_NotesApp.Models.Domain.NoteStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NoteStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Archive"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Deleted"
                        });
                });

            modelBuilder.Entity("ASP_NotesApp.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ASP_NotesApp.Models.Domain.Note", b =>
                {
                    b.HasOne("ASP_NotesApp.Models.Domain.NoteStatus", null)
                        .WithMany("Notes")
                        .HasForeignKey("NoteStatusId");

                    b.HasOne("ASP_NotesApp.Models.Domain.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ASP_NotesApp.Models.Domain.NoteStatus", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("ASP_NotesApp.Models.Domain.User", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}