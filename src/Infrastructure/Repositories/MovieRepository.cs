using ApplicationCore.Entities;
using Infrastructure.Data;
using ApplicationCore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository: EfRepository<Movie>, IMovieRepository
{

    public MovieRepository(MovieShopDbContext movieShopDbContext) : base(movieShopDbContext)
    {
        
    }
    public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
    {
        var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
        return movies;
    }

    public override async Task<Movie> GetById(int id)
    {
        // we need to use Include method
        // first, firstordefault, single, singleordefault
        var movie = await _dbContext.Movies
            .Include(m => m.GenresOfMovie)
            .ThenInclude(m => m.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return null;
        }

        return movie;
    }
    
    public async Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
    {
        var totalMoviesCount = await _dbContext.MovieGenres.Where(m => m.GenreId == genreId).CountAsync();
    
        if (totalMoviesCount == 0) {
            throw new Exception("No Movies Found for that genre");
        }
    
        var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId)
            .Include(m => m.Movie)
            .OrderBy(m => m.MovieId)
            .Select(m => new Movie
            {
                Id = m.MovieId, PosterUrl = m.Movie.PosterUrl, Title = m.Movie.Title
            })
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResultSet<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
    }
}