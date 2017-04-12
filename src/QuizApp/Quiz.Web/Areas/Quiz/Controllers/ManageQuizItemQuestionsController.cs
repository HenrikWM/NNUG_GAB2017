using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.DataAccess.Quiz;
using Quiz.DataAccess.Quiz.InMemory;
using Quiz.Web.Areas.Quiz.Models;

namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class ManageQuizItemQuestionsController : Controller
    {
        private readonly IQuizItemRepository _quizItemRepository = InMemoryQuizItemRepository.Instance;
        private readonly IQuizItemQuestionRepository _quizItemQuestionRepository = InMemoryQuizItemQuestionRepository.Instance;

        public ActionResult Index(int quizItemId)
        {
            if (quizItemId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItem = _quizItemRepository.Get(quizItemId);
            if (quizItem == null)
            {
                return HttpNotFound();
            }

            var quizItemQuestions = _quizItemQuestionRepository.GetQuestionsForQuizItem(quizItem.Id);

            var model = quizItemQuestions.Select(QuizItemQuestionViewModel.MapFrom);
            model = model.Select(LoadRelationshipProperties);
            
            return View(model);
        }
        
        public ActionResult Create(int quizItemId)
        {
            if (quizItemId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItem = _quizItemRepository.Get(quizItemId);
            if (quizItem == null)
            {
                return HttpNotFound();
            }

            var model = new QuizItemQuestionViewModel {QuizItemId = quizItemId};
            LoadRelationshipProperties(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "QuizItemId,Question," +
                            "AnswerAlternative1,AnswerAlternative2," +
                            "AnswerAlternative3,AnswerAlternative4," +
                            "IsAnswerAlternative1Correct,IsAnswerAlternative2Correct," +
                            "IsAnswerAlternative3Correct,IsAnswerAlternative4Correct")]
            QuizItemQuestionViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                LoadRelationshipProperties(model);
                return View(model);
            }

            var quizItemQuestion = new QuizItemQuestion
            {
                QuizItemId = model.QuizItemId,
                Created = DateTime.UtcNow,
                Question = model.Question,
                AnswerAlternative1 = model.AnswerAlternative1,
                AnswerAlternative2 = model.AnswerAlternative2,
                AnswerAlternative3 = model.AnswerAlternative3,
                AnswerAlternative4 = model.AnswerAlternative4,
                IsAnswerAlternative1Correct = model.IsAnswerAlternative1Correct,
                IsAnswerAlternative2Correct = model.IsAnswerAlternative2Correct,
                IsAnswerAlternative3Correct = model.IsAnswerAlternative3Correct,
                IsAnswerAlternative4Correct = model.IsAnswerAlternative4Correct
            };

            _quizItemQuestionRepository.Add(quizItemQuestion);

            return RedirectToAction("Index", new { model.QuizItemId });
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItemQuestion = _quizItemQuestionRepository.Get(id);
            if (quizItemQuestion == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemQuestionViewModel.MapFrom(quizItemQuestion);
            LoadRelationshipProperties(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,QuizItemId,Question," +
                            "AnswerAlternative1,AnswerAlternative2," +
                            "AnswerAlternative3,AnswerAlternative4," +
                            "IsAnswerAlternative1Correct,IsAnswerAlternative2Correct," +
                            "IsAnswerAlternative3Correct,IsAnswerAlternative4Correct")]
            QuizItemQuestionViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                LoadRelationshipProperties(model);
                return View(model);
            }

            var quizItemQuestion = _quizItemQuestionRepository.Get(model.Id);
            if (quizItemQuestion == null)
            {
                return HttpNotFound();
            }

            quizItemQuestion.Modified = DateTime.UtcNow;
            quizItemQuestion.Question = model.Question;
            quizItemQuestion.AnswerAlternative1 = model.AnswerAlternative1;
            quizItemQuestion.AnswerAlternative2 = model.AnswerAlternative2;
            quizItemQuestion.AnswerAlternative3 = model.AnswerAlternative3;
            quizItemQuestion.AnswerAlternative4 = model.AnswerAlternative4;
            quizItemQuestion.IsAnswerAlternative1Correct = model.IsAnswerAlternative1Correct;
            quizItemQuestion.IsAnswerAlternative2Correct = model.IsAnswerAlternative2Correct;
            quizItemQuestion.IsAnswerAlternative3Correct = model.IsAnswerAlternative3Correct;
            quizItemQuestion.IsAnswerAlternative4Correct = model.IsAnswerAlternative4Correct;

            _quizItemQuestionRepository.Update(quizItemQuestion);

            return RedirectToAction("Index", new { model.QuizItemId });
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItemQuestion = _quizItemQuestionRepository.Get(id);
            if (quizItemQuestion == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemQuestionViewModel.MapFrom(quizItemQuestion);
            LoadRelationshipProperties(model);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id,QuizItemId")] int id, int quizItemId)
        {
            _quizItemQuestionRepository.Delete(id);

            return RedirectToAction("Index", new { quizItemId });
        }

        private QuizItemQuestionViewModel LoadRelationshipProperties(QuizItemQuestionViewModel model)
        {
            var quizItem = _quizItemRepository.Get(model.QuizItemId);

            model.QuizItemName = quizItem.Name;

            return model;
        }
    }
}