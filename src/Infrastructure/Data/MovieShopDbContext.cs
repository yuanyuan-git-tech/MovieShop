using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class MovieShopDbContext: DbContext
{
    // inject the dbcontext options
    public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
    {
        
    }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Trailer> Trailers { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(ConfigureMovie);
        modelBuilder.Entity<Cast>(ConfigureCast);
        modelBuilder.Entity<Trailer>(ConfigureTrailer);
        modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
        modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
    }

    private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
    {   
        // Fluent API Way
        // specify the rules for Movie Entity
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).HasMaxLength(256);
        builder.Property(m => m.Overview).HasMaxLength(4096);
        builder.Property(m => m.Tagline).HasMaxLength(512);
        builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
        builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
        builder.Property(m => m.PosterUrl).HasMaxLength(2084);
        builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
        builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
        builder.Property(m => m.UpdatedBy).HasMaxLength(512);
        builder.Property(m => m.CreatedBy).HasMaxLength(512);

        builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
        builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
        builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);

        builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

        builder.Ignore(m => m.Rating);

        builder.HasIndex(m => m.Title);
        builder.HasIndex(m => m.Price);
        builder.HasIndex(m => m.Revenue);
        builder.HasIndex(m => m.Budget);
    }

    private void ConfigureCast(EntityTypeBuilder<Cast> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(128).IsRequired();
        builder.Property(c => c.Gender).HasMaxLength(16).IsRequired();
        builder.Property(c => c.ProfilePath).HasMaxLength(2084).IsRequired();
        builder.Property(c => c.TmdbUrl).HasMaxLength(2084).IsRequired();
    }
    
    private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TrailerUrl).HasMaxLength(2048).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(2084).IsRequired();
    }
    
    private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("MovieGenres");
        builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
        builder.HasOne(m => m.Genre).WithMany(m => m.MoviesOfGenre).HasForeignKey(m => m.GenreId);
        builder.HasOne(m => m.Movie).WithMany(m => m.GenresOfMovie).HasForeignKey(m => m.MovieId);
    }

    private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
    {
        builder.ToTable("MovieCasts");
        builder.HasKey(mc => new { mc.CastId, mc.MovieId, mc.Character });
        builder.HasOne(mc => mc.Movie).WithMany(m => m.CastsOfMovie).HasForeignKey(mc => mc.MovieId);
        builder.HasOne(mc => mc.Cast).WithMany(c => c.MoviesOfCast).HasForeignKey(mc => mc.MovieId);
    }

}