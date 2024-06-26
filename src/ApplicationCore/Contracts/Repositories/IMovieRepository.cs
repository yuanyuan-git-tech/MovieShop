using ApplicationCore.Entities;
using ApplicationCore.Helpers;


namespace Infrastructure.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetTop30RevenueMovies();
    Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int page = 1);
}