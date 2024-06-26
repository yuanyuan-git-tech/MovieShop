using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.Helpers;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieDetails> GetMovieDetails(int id)
    {
        var movie = await _movieRepository.GetById(id);
        var movieDetails = new MovieDetails
        {
            Id = movie.Id, Budget = movie.Budget, Overview = movie.Overview, Price = movie.Price,
            PosterUrl = movie.PosterUrl, Revenue = movie.Revenue,
            ReleaseDate = movie.ReleaseDate.GetValueOrDefault(), Rating = movie.Rating, Tagline = movie.Tagline,
            Title = movie.Title, RunTime = movie.RunTime,
            BackdropUrl = movie.BackdropUrl, ImdbUrl = movie.ImdbUrl,
            TmdbUrl = movie.TmdbUrl
        };
        movieDetails.Genres = new List<GenreModel>();
        foreach (var genre in movie.GenresOfMovie)
        {
            movieDetails.Genres.Add(new GenreModel
            {
                Id = genre.Genre.Id, Name = genre.Genre.Name
            });
        }
        movieDetails.Casts = new List<CastModel>();
        foreach (var cast in movie.CastsOfMovie)
        {
            movieDetails.Casts.Add(new CastModel
            {
                Id = cast.Cast.Id, Name = cast.Cast.Name, Character = cast.Character,
                Gender = cast.Cast.Gender, ProfilePath = cast.Cast.ProfilePath,
                TmdbUrl = cast.Cast.TmdbUrl
            });
        }
        movieDetails.Trailers = new List<TrailerModel>();
        foreach (var trailer in movie.Trailers)
        {
            movieDetails.Trailers.Add(new TrailerModel
            {
                Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl, MovieId = trailer.MovieId
            });
        }

        return movieDetails;
    }

    public async Task<List<MovieCardModel>> GetTop30GrossingMovies()
    {
        var movies = await _movieRepository.GetTop30RevenueMovies();
        var movieCards = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            movieCards.Add(new MovieCardModel
            {
                Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title
            });
        }
        return movieCards;
    }

    public async Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1)
    {
        var pagedMovies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);
        var movieCards = new List<MovieCardModel>();
        movieCards.AddRange(pagedMovies.Data.Select(m => new MovieCardModel
        {
            Id = m.Id, PosterUrl = m.PosterUrl, Title = m.Title
        }));
        return new PagedResultSet<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.Count);
    }
}