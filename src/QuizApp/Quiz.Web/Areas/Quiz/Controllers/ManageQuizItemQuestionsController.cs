using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.DataAccess.Quiz;
using Quiz.Web.Areas.Quiz.Models;
using WebGrease.Css.Extensions;

namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class ManageQuizItemQuestionsController : Controller
    {
        public ActionResult Index(int quizItemId)
        {
            if (quizItemId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItem = InMemoryQuizItemRepository.Instance.Get(quizItemId);
            if (quizItem == null)
            {
                return HttpNotFound();
            }

            var items = InMemoryQuizItemQuestionRepository.Instance.GetQuestionsForQuizItem(quizItem.Id);

            var model = items.Select(QuizItemQuestionViewModel.MapFrom);
            model = model.Select(LoadRelationshipProperties);
            
            return View(model);
        }
        
        public ActionResult Create(int quizItemId)
        {
            if (quizItemId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItem = InMemoryQuizItemRepository.Instance.Get(quizItemId);
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
                            "AnswerAlternative1,AnswerAlternative2,AnswerAlternative3,AnswerAlternative4," +
                            "IsAnswerAlternative1Correct,IsAnswerAlternative2Correct,IsAnswerAlternative3Correct,IsAnswerAlternative4Correct")]
            QuizItemQuestionViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var item = new QuizItemQuestion
            {
                Id = 1,
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

            InMemoryQuizItemQuestionRepository.Instance.Add(item);

            return RedirectToAction("Index", new { quizItemId=model.QuizItemId });
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = InMemoryQuizItemQuestionRepository.Instance.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemQuestionViewModel.MapFrom(item);
            LoadRelationshipProperties(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,QuizItemId,Question," +
                            "AnswerAlternative1,AnswerAlternative2,AnswerAlternative3,AnswerAlternative4," +
                            "IsAnswerAlternative1Correct,IsAnswerAlternative2Correct,IsAnswerAlternative3Correct,IsAnswerAlternative4Correct")]
            QuizItemQuestionViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var item = InMemoryQuizItemQuestionRepository.Instance.Get(model.Id);
            if (item == null)
            {
                return HttpNotFound();
            }

            item.Modified = DateTime.UtcNow;
            item.Question = model.Question;
            item.AnswerAlternative1 = model.AnswerAlternative1;
            item.AnswerAlternative2 = model.AnswerAlternative2;
            item.AnswerAlternative3 = model.AnswerAlternative3;
            item.AnswerAlternative4 = model.AnswerAlternative4;
            item.IsAnswerAlternative1Correct = model.IsAnswerAlternative1Correct;
            item.IsAnswerAlternative2Correct = model.IsAnswerAlternative2Correct;
            item.IsAnswerAlternative3Correct = model.IsAnswerAlternative3Correct;
            item.IsAnswerAlternative4Correct = model.IsAnswerAlternative4Correct;

            InMemoryQuizItemQuestionRepository.Instance.Update(item);

            return RedirectToAction("Index", new { quizItemId = model.QuizItemId });
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = InMemoryQuizItemQuestionRepository.Instance.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemQuestionViewModel.MapFrom(item);
            LoadRelationshipProperties(model);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id,QuizItemId")] int id, int quizItemId)
        {
            InMemoryQuizItemQuestionRepository.Instance.Delete(id);

            return RedirectToAction("Index", new { quizItemId });
        }

        private QuizItemQuestionViewModel LoadRelationshipProperties(QuizItemQuestionViewModel model)
        {
            var quizItem = InMemoryQuizItemRepository.Instance.Get(model.QuizItemId);

            model.QuizItemName = quizItem.Name;

            return model;
        }
    }
}