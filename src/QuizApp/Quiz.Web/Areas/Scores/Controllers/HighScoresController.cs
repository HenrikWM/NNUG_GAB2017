using System.Web.Mvc;

namespace Quiz.Web.Areas.Scores.Controllers
{
    public class HighScoresController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}