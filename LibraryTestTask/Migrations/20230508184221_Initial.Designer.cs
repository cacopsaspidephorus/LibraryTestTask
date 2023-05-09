﻿// <auto-generated />
using System;
using LibraryTestTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibraryTestTask.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20230508184221_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookReader", b =>
                {
                    b.Property<int>("IssuedBooksId")
                        .HasColumnType("integer");

                    b.Property<int>("ReadersId")
                        .HasColumnType("integer");

                    b.HasKey("IssuedBooksId", "ReadersId");

                    b.HasIndex("ReadersId");

                    b.ToTable("BookReader");
                });

            modelBuilder.Entity("LibraryTestTask.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfAddition")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfChange")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfCopies")
                        .HasColumnType("integer");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("YearOfPublication")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("LibraryTestTask.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateReturned")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateTaken")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ReaderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("History", (string)null);
                });

            modelBuilder.Entity("LibraryTestTask.Models.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfAddition")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfChange")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reader", (string)null);
                });

            modelBuilder.Entity("BookReader", b =>
                {
                    b.HasOne("LibraryTestTask.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("IssuedBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryTestTask.Models.Reader", null)
                        .WithMany()
                        .HasForeignKey("ReadersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}