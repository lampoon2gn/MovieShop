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
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
    }
}