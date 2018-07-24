using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VideoGameDBServices.Models
{
    public partial class VideogamesContext : DbContext
    {
        public virtual DbSet<Developers> Developers { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<Publishers> Publishers { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Systems> Systems { get; set; }
        public virtual DbSet<Years> Years { get; set; }

        public VideogamesContext(DbContextOptions<VideogamesContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developers>(entity =>
            {
                entity.ToTable("vwdevelopers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DeveloperName)
                    .HasColumnName("developername")
                    .HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.ToTable("vwgames");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(2000)");

                entity.Property(e => e.Genre)
                    .HasColumnName("genre")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Players)
                    .HasColumnName("players")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nvarchar(500)");

                entity.Property(e => e.SystemName)
                    .HasColumnName("systemName")
                    .HasColumnType("nvarchar(100)");

                entity.Property(e => e.DeveloperName)
                    .HasColumnName("developerName")
                    .HasColumnType("nvarchar(100)");

                entity.Property(e => e.PublisherName)
                    .HasColumnName("publishername")
                    .HasColumnType("nvarchar(100)");

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("date");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("nvarchar(100)");

            });

            modelBuilder.Entity<Manufacturers>(entity =>
            {
                entity.ToTable("vwmanufacturers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("manufacturername")
                    .HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<Publishers>(entity =>
            {
                entity.ToTable("vwpublishers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("publishername")
                    .HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.ToTable("ratings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<Systems>(entity =>
            {
                entity.ToTable("vwsystems");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.SystemName)
                    .HasColumnName("systemname")
                    .HasColumnType("nvarchar(100)");

            });

            modelBuilder.Entity<Years>(entity =>
            {
                entity.ToTable("vwyears");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("date");
            });
        }
    }
}
