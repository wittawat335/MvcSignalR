using Microsoft.AspNetCore.Mvc;
using MvcSignalR.Models;
using MvcSignalR.Repositories;
using System.Diagnostics;

namespace MvcSignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepository _repository;

        public HomeController(ILogger<HomeController> logger, UserRepository repository)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var userFromDb = await _repository.GetUserDetails(username, password);

            if (userFromDb == null)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View();
            }

            HttpContext.Session.SetString("Username", userFromDb.Username);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult Index()
        {
            //var model = _repository.GetList();
            return View();
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
}