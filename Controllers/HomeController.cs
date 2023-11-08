using LearnPractice.Models;
using LearnPractice.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LearnPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ArticlesContext db;

        public HomeController(ILogger<HomeController> logger, ArticlesContext context)
        {
            _logger = logger;
            db=context;
        }

        public IActionResult Index()
        {
            ViewBag.Articles = db.Articles.Take(5);
            var latestId = db.Articles.Max(p => p.Id);
            ViewBag.Article = db.Articles.Find(latestId);
            return View();
        }

        public IActionResult Articles()
        {
            return View(db.Articles.ToList());
        }
        public IActionResult Article(int? id)
        {
            if (id == null) return Redirect("~/Home/Article");
            var article = db.Articles.Find(id);

            return View(article);
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