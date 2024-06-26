
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

public class MoviesController : Controller
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Details(int id)
    {
        // movie service with details
        // pass the movie details data to view
        var movie = await _movieService.GetMovieDetails(id);
        return View(movie);
    }

    public async Task<IActionResult> Genres(int id, int pageSize = 30, int pageNumber = 1)
    {
        var pagedMovies = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
        return View( "PagedMovies", pagedMovies);
    }
}