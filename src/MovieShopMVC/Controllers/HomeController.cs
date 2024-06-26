using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly IMovieService _movieService;

    public HomeController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IActionResult> Index()
    {
        // most of the logic should come from other dependencies, services
        // Interfaces
        var movies = await _movieService.GetTop30GrossingMovies();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
// Entity framework we can use it in 2 ways
// Code first approach (write c# code, and you use migrations to create the tables)
// Database first approach (is not fully supported in .NET Core)