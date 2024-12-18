using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskServices _taskServices;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITaskServices taskServices)
        {
            _logger = logger;
            _taskServices = taskServices;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskServices.GetAllTasksAsync();
            return View(tasks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
