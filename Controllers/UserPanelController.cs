using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnPractice.Models.Database;
using Microsoft.AspNetCore.Authorization;
using LearnPractice.Data;

namespace LearnPractice.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanelController
        public ArticlesContext db;
        public CarsContext carsContext;
        public LearnPracticeContext usersContext;
        public UserPanelController(ArticlesContext context, CarsContext carsCont,LearnPracticeContext us)
        {
            db = context;
            carsContext = carsCont;
            usersContext = us;
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Articles()
        {
            return View(db.Articles.ToList());
        }

        public ActionResult DeleteArticle(int id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }
            else
            {
                var article = db.Articles.Find(id);
                db.Articles.Remove(article);
                db.SaveChanges();
                return Redirect("~/UserPanel/Articles");
            }
            
        }

        [HttpGet]
        public ActionResult CreateArticle()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult CreateArticle(Articles articles)
        {
            var article = new Articles
            {
                Title = articles.Title,
                Description = articles.Description,
                Content = articles.Content,
                Author = articles.Author,
                Date = articles.Date,
               
                ImageLink = articles.ImageLink,
            };
            db.Articles.Add(article);
            db.SaveChanges();
            return View();
        }


        [HttpGet]
        public ActionResult CarsCreate()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CarsCreate(Cars cars)
        {
            var car = new Cars
            {
                Model = cars.Model,
                Mark = cars.Mark,
                IdPts = cars.IdPts,
                IdSts = cars.IdSts,
                Sum = cars.Sum,
                MilHour = cars.MilHour,
                Preview = cars.Preview
            };
            carsContext.Cars.Add(car);
            carsContext.SaveChanges();

            return View();
        }


        public ActionResult Cars()
        {
            return View(carsContext.Cars.ToList());
        }

        public ActionResult DeleteCars(int id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }
            else
            {
                var article = carsContext.Cars.Find(id);
                carsContext.Cars.Remove(article);
                carsContext.SaveChanges();
                return Redirect("~/UserPanel/Cars");
            }

        }

        [HttpGet]
        public ActionResult EditCar(int? id)
        {
            var car = carsContext.Cars.Find(id);
            
            
            return View(car);
        }

        [HttpPost]
        public ActionResult EditCar(Cars cars)
        {
            var car = new Cars
            {
                Model = cars.Model,
                Mark = cars.Mark,
                IdPts = cars.IdPts,
                IdSts = cars.IdSts,
                Sum = cars.Sum,
                MilHour = cars.MilHour,
                Preview = cars.Preview,
                UserId = cars.UserId
                
            };
            carsContext.Cars.Update(car);
            carsContext.SaveChanges();

            return View();


            
        }


        public ActionResult Mycar(string email)
        {
            var car = carsContext.Cars.Where(x=>x.UserId== email).ToList();
            
            
            ViewBag.Car = car;
            return View();
        }


        public ActionResult Autoliz()
        {
            var cars = carsContext.Cars.Where(x => x.UserId != null);
            ViewBag.Cars = cars;
            return View();
        }

        public ActionResult Autolizs(int? id)
        {
            var cars = carsContext.Cars.Find(id);
            ViewBag.Cars = cars;
            var user = usersContext.Users.Where(x => x.Email == cars.UserId).ToList();
            ViewBag.User = user;
            return View();
        }
    }
}
