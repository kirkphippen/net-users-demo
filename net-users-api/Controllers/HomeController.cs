using Microsoft.AspNetCore.Mvc;

namespace NetUsersApi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Root endpoint that displays users in a nice HTML table
    /// </summary>
    public IActionResult Index()
    {
        _logger.LogInformation("GET / endpoint called");
        var users = UsersController.GetAllUsers();
        return View(users);
    }
}
