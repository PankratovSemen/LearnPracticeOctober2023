using LearnPractice.Models;
using LearnPractice.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LearnPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ArticlesContext db;
        private CarsContext carsContext;

        public HomeController(ILogger<HomeController> logger, ArticlesContext context, CarsContext carsCont)
        {
            _logger = logger;
            db = context;
            carsContext = carsCont;
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.Articles = db.Articles.Take(5);
                var latestId = db.Articles.Max(p => p.Id);
                ViewBag.Article = db.Articles.Find(latestId);
                latestId = carsContext.Cars.Max(p => p.Id);
                var minId = carsContext.Cars.Min(p => p.Id);
                ViewBag.Cars = carsContext.Cars.Take(3);

                ViewBag.Car = carsContext.Cars.Find(latestId);
                ViewBag.Car1 = carsContext.Cars.Find(minId);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
        
        public IActionResult Articles()
        {
            try
            {
                return View(db.Articles.ToList());
            }

            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
            
        }
        public IActionResult Article(int? id)
        {
            if (id == null) return Redirect("~/Home/Article");
            var article = db.Articles.Find(id);

            return View(article);
        }
        public IActionResult Cars()
        {

            return View(carsContext.Cars.ToList());

        }
        public IActionResult Car(int? id)
        {
            if (id == null) return Redirect("~/Home/Cars");
            var car = carsContext.Cars.Find(id);
            return View(car);
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