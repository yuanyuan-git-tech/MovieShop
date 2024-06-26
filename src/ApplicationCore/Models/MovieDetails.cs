using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class MovieDetails
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Overview { get; set; }
    public string? Tagline { get; set; }
    public decimal? Budget { get; set; }
    public decimal? Revenue { get; set; }
    public string? ImdbUrl { get; set; }
    public string? TmdbUrl { get; set; }
    public string? BackdropUrl { get; set; }
    public string? PosterUrl { get; set; }
    public decimal? Rating { get; set; }
    
    public DateTime? ReleaseDate { get; set; }
    
    public int? RunTime { get; set; }
    public decimal? Price {get; set; }
    
    public List<GenreModel> Genres { get; set; }
    public List<TrailerModel> Trailers { get; set; }
    public List<CastModel> Casts { get; set; }
}