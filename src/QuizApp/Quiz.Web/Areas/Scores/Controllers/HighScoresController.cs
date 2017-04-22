using System.Linq;
using System.Web.Mvc;
using Quiz.DataAccess.Abstractions.Quiz;
using Quiz.DataAccess.Ef.Quiz.SqlDb;
using Quiz.DataAccess.InMemory.Quiz.InMemory;
using Quiz.Web.Areas.Scores.Models;
using WebGrease.Css.Extensions;

namespace Quiz.Web.Areas.Scores.Controllers
{
    public class HighScoresController : Controller
    {
        //private readonly IQuizItemRepository _quizItemRepository = InMemoryQuizItemRepository.Instance;
        private readonly IQuizItemRepository _quizItemRepository = new SqlDbQuizItemRepository();

        //private readonly IQuizTakingRepository _quizTakingRepository = InMemoryQuizTakingRepository.Instance;
        private readonly IQuizTakingRepository _quizTakingRepository = new SqlDbQuizTakingRepository();

        public ActionResult Index()
        {
            var quizTakings = _quizTakingRepository.GetAll();
            
            var model = HighScoreViewModel.MapFromDataModel(quizTakings);
            model.ForEach(LoadRelationshipProperties);
            model = model
                .OrderByDescending(o => o.Score)
                .ThenBy(o => o.Ended);

            return View("Index", model);
        }

        private void LoadRelationshipProperties(HighScoreViewModel model)
        {
            var quizItem = _quizItemRepository.Get(model.QuizItemId);
            model.QuizItemName = quizItem.Name;
        }
    }
}