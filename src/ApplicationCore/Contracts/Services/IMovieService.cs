using ApplicationCore.Models;
using ApplicationCore.Helpers;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    // have all the business logic methods related to Movies
    Task<List<MovieCardModel>> GetTop30GrossingMovies();
    Task<MovieDetails> GetMovieDetails(int id);
    Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int pageNumber = 1);
}