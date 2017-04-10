using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.DataAccess.Quiz;
using Quiz.Web.Areas.Quiz.Models;
using WebGrease.Css.Extensions;

namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class ManageQuizItemsController : Controller
    {
        public ActionResult Index()
        {
            var items = InMemoryQuizItemRepository.Instance.GetAll();
            
            var model = items.Select(QuizItemViewModel.MapFrom);

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
                return View(model);
            }

            var item = new QuizItem
            {
                Id = 1,
                Created = DateTime.UtcNow,
                Name = model.Name,
                Questions = new List<QuizItemQuestion>()
            };

            InMemoryQuizItemRepository.Instance.Add(item);

            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = InMemoryQuizItemRepository.Instance.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemViewModel.MapFrom(item);

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

            var item = InMemoryQuizItemRepository.Instance.Get(model.Id);
            if (item == null)
            {
                return HttpNotFound();
            }

            item.Name = model.Name;
            item.Modified = DateTime.UtcNow;

            InMemoryQuizItemRepository.Instance.Update(item);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = InMemoryQuizItemRepository.Instance.Get(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var model = QuizItemViewModel.MapFrom(item);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id")] int id)
        {
            var quizItemQuestions = InMemoryQuizItemQuestionRepository.Instance.GetQuestionsForQuizItem(id);
            quizItemQuestions.ForEach(o => InMemoryQuizItemQuestionRepository.Instance.Delete(o.Id));

            InMemoryQuizItemRepository.Instance.Delete(id);
            
            return RedirectToAction("Index");
        }
    }
}