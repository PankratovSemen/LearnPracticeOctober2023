using Microsoft.AspNetCore.Http;

using LearnPractice.Models.Database;
using Microsoft.AspNetCore.Authorization;
using LearnPractice.Data;
using System.Web;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using LearnPractice.Models.Logic;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace LearnPractice.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanelController
        private readonly ILogger<HomeController> _logger;
        public ArticlesContext db;
        public CarsContext carsContext;
        public LearnPracticeContext usersContext;
        public ServicesContext servicesContext;
        public ApplicationsContext applications;
        IWebHostEnvironment _appEnvironment;
        public UserPanelController(ArticlesContext context, CarsContext carsCont,LearnPracticeContext us, ServicesContext servicesCont, ApplicationsContext applications, ILogger<HomeController> logger, IWebHostEnvironment appEnvironment)
        {
            db = context;
            carsContext = carsCont;
            usersContext = us;
            servicesContext = servicesCont;
            this.applications = applications;
            _logger = logger;
            _appEnvironment = appEnvironment;
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
            return Redirect("~/UserPanel/Index");
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

            return Redirect("~/UserPanel/Cars");
        }


        public ActionResult Cars()
        {
            return View(carsContext.Cars.Where(x=>x.UserId==null).ToList());
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
                Id = cars.Id,
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


        public ActionResult ServicesView()
        {
            var servicesList = servicesContext.Services.ToList();
            foreach(var service in servicesList)
            {
                ViewBag.Client = usersContext.Users.Where(x => x.Email == service.LoginUser);
            }

            foreach(var cars in servicesList)
            {
                ViewBag.Cars = carsContext.Cars.Where(x => x.UserId == cars.LoginUser).ToList();
            }
           
            return View(servicesList);
        }

        [HttpGet]
        public ActionResult Services(string email)
        {
            if (email == null)
            {
                return StatusCode(404);
            }
            else
            {

                var car = carsContext.Cars.Where(x=>x.UserId==email).ToList();
                foreach(var carid in car)
                {
                    ViewBag.CarId = carid.Id;
                }
               
                ViewBag.Email = email;
                return View();
            }

            
        }

        [HttpPost]
        public ActionResult Services(Services services)
        {
            var serv = new Services
            {
                Date = services.Date,
                IdCar = services.IdCar,
                LoginUser = services.LoginUser,
                DescriptionFail = services.DescriptionFail,
                Status = services.Status
            };
            servicesContext.Services.Add(serv);
            servicesContext.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult ServiceEdit(int? id)
        {
            var serv = servicesContext.Services.Find(id);
            var User = usersContext.Users.Where(x => x.Email == serv.LoginUser);
            foreach(var user in User)
            {
                ViewBag.User = user.Email;
            }
           
            var car = carsContext.Cars.Where(x => x.UserId == serv.LoginUser).ToList();
            foreach (var carid in car)
            {
                ViewBag.CarId = carid.Id;
            }
            var des = servicesContext.Services.Where(x => x.Id == id).ToList();
            foreach(var desk in des)
            {
                ViewBag.Descr = desk.DescriptionFail;
            }
           
            
            return View(serv);
        }

        [HttpPost]
        public ActionResult ServiceEdit(Services services) 
        {
            var serv = new Services
            {
                Date = services.Date,
                IdCar = services.IdCar,
                LoginUser = services.LoginUser,
                DescriptionFail = services.DescriptionFail,
                Status = services.Status,
                Message = services.Message
            };
            servicesContext.Services.Update(serv);
            servicesContext.SaveChanges();
            var app = new Applications
            {
                Message = $"Статус заявки на техническое обслужиание изменен на {services.Status}.<br/> {services.Message} ",
                Status = "Отправлен",
                LoginUser = services.LoginUser,
                Date = DateTime.UtcNow
            };
            applications.Add(app);
            applications.SaveChanges();
            return View();
        }

        public ActionResult Message(string email)
        {
            ViewBag.Message = applications.Applications.Where(x => x.LoginUser == email).ToList();
            return View();
        }
        public ActionResult LoadCars()
        {
            return View();
        }
        //Импорт в Excel
        [HttpPost]
        

        public async Task<ActionResult> Import(IFormFile fileExcel)
        {
            string path = "";
            if (fileExcel != null)
            {
                path = "/Files/" + fileExcel.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await fileExcel.CopyToAsync(fileStream);
                }
                _logger.LogInformation(path);
            }

                if (ModelState.IsValid)
            {
                ExcelVM viewModel = new ExcelVM();
                using(XLWorkbook workbook = new XLWorkbook(_appEnvironment.WebRootPath + path))
                {
                    foreach(IXLWorksheet worksheet in workbook.Worksheets)
                    {
                      
                       

                        
                            foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                            {
                                try
                                {
                                    
                                    Cars car = new Cars();
                                   
                                    car.Mark = row.Cell(1).Value.ToString();
                                    car.Model = row.Cell(2).Value.ToString();
                                    car.IdPts = row.Cell(3).Value.ToString();
                                    car.IdSts = row.Cell(4).Value.ToString();
                                    car.Sum = Convert.ToInt32(row.Cell(5).Value.ToString());
                                    car.MilHour = Convert.ToInt32(row.Cell(6).Value.ToString());
                                    car.Preview = row.Cell(7).Value.ToString();
                                    carsContext.Add(car);
                                    carsContext.SaveChanges();
                                //if (carsContext.Cars.Where(q => q.IdPts == car.IdPts) != null)
                                //{
                                //    _logger.LogError("Такая машина существует");
                                //}
                                //else
                                //{
                                    

                                //}




                                   

                            }
                                catch(Exception ex)
                                {
                                    _logger.LogError(ex.Message);
                                   
                                }
                            }
                        
                    }
                }

                return Redirect("~/UserPanel");
            }
            return Redirect("~/UserPanel");
        }


        }
}
