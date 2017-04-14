using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;
using Quiz.DataAccess.Quiz.InMemory;
using Quiz.Web.Areas.Quiz.Models;

namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class ManageQuizItemsController : Controller
    {
        private readonly IQuizItemRepository _quizItemRepository = InMemoryQuizItemRepository.Instance;
        private readonly IQuizItemQuestionRepository _quizItemQuestionRepository = InMemoryQuizItemQuestionRepository.Instance;

        public ActionResult Index()
        {
            var quizItems = _quizItemRepository.GetAll();
            
            var model = quizItems.Select(QuizItemViewModel.MapFromDataModel);
            model = model.Select(LoadRelationshipProperties);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] QuizItemViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                LoadRelationshipProperties(model);
                return View(model);
            }

            var quizItem = new QuizItem
            {
                Created = DateTime.UtcNow,
                Name = model.Name
            };

            _quizItemRepository.Add(quizItem);

            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizItem = _quizItemRepository.Get(id);
            if (quizItem == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemViewModel.MapFromDataModel(quizItem);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] QuizItemViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var quizItem = _quizItemRepository.Get(model.Id);
            if (quizItem == null)
            {
                return HttpNotFound();
            }

            quizItem.Name = model.Name;
            quizItem.Modified = DateTime.UtcNow;

            _quizItemRepository.Update(quizItem);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = _quizItemRepository.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemViewModel.MapFromDataModel(item);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id")] int id)
        {
            _quizItemRepository.Delete(id);
            
            return RedirectToAction("Index");
        }

        private QuizItemViewModel LoadRelationshipProperties(QuizItemViewModel model)
        {
            var quizItemQuestions = _quizItemQuestionRepository.GetQuestionsForQuizItem(model.Id);

            model.Questions = quizItemQuestions.Select(QuizItemQuestionViewModel.MapFromDataModel);

            return model;
        }
    }
}