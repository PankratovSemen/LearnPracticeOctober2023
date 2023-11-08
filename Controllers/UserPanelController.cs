using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnPractice.Models.Database;

namespace LearnPractice.Controllers
{
    public class UserPanelController : Controller
    {
        // GET: UserPanelController
        public ArticlesContext db;
        public UserPanelController(ArticlesContext context)
        {
            db = context;
        }
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
    }
}
