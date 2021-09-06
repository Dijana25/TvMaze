﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TvMaze.DataAccess.Models;

namespace TvMaze.DataAccess.Migrations
{
    [DbContext(typeof(TvMazeContext))]
    partial class TvMazeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TvMaze.DataAccess.Models.Cast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Name");

                    b.Property<int?>("ShowId");

                    b.Property<int>("TvMazeId");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.ToTable("Cast");
                });

            modelBuilder.Entity("TvMaze.DataAccess.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("TvMazeId");

                    b.HasKey("Id");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("TvMaze.DataAccess.Models.Cast", b =>
                {
                    b.HasOne("TvMaze.DataAccess.Models.Show", "Show")
                        .WithMany("Cast")
                        .HasForeignKey("ShowId");
                });
#pragma warning restore 612, 618
        }
    }
}
