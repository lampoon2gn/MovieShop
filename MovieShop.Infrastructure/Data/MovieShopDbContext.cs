using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext:DbContext
    {
        //this will get called inside startup.cs
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }

        //override this to use fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //action delegate takes a method with void return type
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            //place to confugure our Trailer entity
            builder.ToTable("Trailer");
            builder.HasKey(t=>t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);

        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(m => m.Gender).HasMaxLength(4096);
            builder.Property(m => m.TmdbUrl).HasMaxLength(4096);
            builder.Property(m => m.ProfilePath).HasMaxLength(2048);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.MovieID, mc.CastId, mc.Character });
            builder.HasOne(mc => mc.Cast).WithMany(m => m.MovieCasts).HasForeignKey(mc => mc.CastId);
            builder.HasOne(mc => mc.Movie).WithMany(c => c.MovieCasts).HasForeignKey(mc => mc.MovieID);
            builder.Property(mc => mc.Character).IsRequired().HasMaxLength(450);
            builder.HasIndex(mc => new { mc.MovieID, mc.CastId, mc.Character });
        }

        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("Crew");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128).IsRequired(false);
            builder.Property(m => m.Gender).HasMaxLength(4096).IsRequired(false);
            builder.Property(m => m.TmdbUrl).HasMaxLength(4096).IsRequired(false);
            builder.Property(m => m.ProfilePath).HasMaxLength(2048).IsRequired(false);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new { mc.MovieId, mc.CrewId, mc.Department,mc.Job });
            builder.HasOne(mc => mc.Movie).WithMany(m => m.MovieCrews).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Crew).WithMany(c => c.MovieCrews).HasForeignKey(mc => mc.CrewId);
            builder.Property(mc => mc.Department).IsRequired().HasMaxLength(128);
            builder.Property(mc => mc.Job).IsRequired().HasMaxLength(128);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).IsRequired(false).HasMaxLength(128);
            builder.Property(u => u.LastName).IsRequired(false).HasMaxLength(128);
            builder.Property(u => u.DateOfBirth).IsRequired(false).HasPrecision(7);
            builder.Property(u => u.Email).IsRequired(false).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).IsRequired(false).HasMaxLength(1024);
            builder.Property(u => u.Salt).IsRequired(false).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(16);
            builder.Property(u => u.TwoFactorEnabled).IsRequired(false);
            builder.Property(u => u.LockoutEndDate).IsRequired(false).HasPrecision(7);
            builder.Property(u => u.LastLoginDateTime).IsRequired(false).HasPrecision(7);
            builder.Property(u => u.IsLocked).IsRequired(false);
            builder.Property(u => u.AccessFailedCount).IsRequired(false);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            //place to confugure our movie entity
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId});
            builder.HasOne(r => r.Movie).WithMany(u => u.Reviews).HasForeignKey(r => r.MovieId);
            builder.HasOne(r => r.User).WithMany(m => m.Reviews).HasForeignKey(mc => mc.UserId);
            builder.Property(mc => mc.Rating).IsRequired().HasPrecision(3,2);
            builder.Property(mc => mc.ReviewText).IsRequired(false).HasMaxLength(4096);
        }
        
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}