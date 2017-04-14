using System.Linq;
using System.Web.Mvc;
using Quiz.DataAccess.Quiz;
using Quiz.DataAccess.Quiz.InMemory;
using Quiz.Web.Areas.Scores.Models;
using WebGrease.Css.Extensions;

namespace Quiz.Web.Areas.Scores.Controllers
{
    public class HighScoresController : Controller
    {
        private readonly IQuizItemRepository _quizItemRepository = InMemoryQuizItemRepository.Instance;
        private readonly InMemoryQuizTakingRepository _quizTakingRepository = InMemoryQuizTakingRepository.Instance;

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