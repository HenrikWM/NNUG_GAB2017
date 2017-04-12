using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.DataAccess.Quiz;
using Quiz.Web.Areas.Quiz.Models;

namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class QuizTakingController : Controller
    {
        public ActionResult Start(int quizItemId)
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
            
            var model = QuizTakingViewModel.MapFrom(quizItem);
            LoadRelationshipProperties(model);

            return View("Start", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start([Bind(Include = "QuizItemId,ParticipantName")] QuizTakingViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                LoadRelationshipProperties(model);
                return View(model);
            }

            var item = new QuizTaking
            {
                QuizItemId = model.QuizItemId,
                Created = DateTime.UtcNow,
                ParticipantName = model.ParticipantName
            };

            var id = InMemoryQuizTakingRepository.Instance.Add(item);

            return RedirectToAction("InProgress", new { id });
        }

        public ActionResult InProgress(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizTaking = InMemoryQuizTakingRepository.Instance.Get(id);
            if (quizTaking == null)
            {
                return HttpNotFound();
            }

            var model = QuizTakingViewModel.MapFrom(quizTaking);
            LoadRelationshipProperties(model);

            return View("InProgress", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult End([Bind(Include = "Id,ParticipantName,QuestionsWithAnswers")] QuizTakingViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                LoadRelationshipProperties(model);
                return View("InProgress", model);
            }

            var quizTaking = InMemoryQuizTakingRepository.Instance.Get(model.Id);
            if (quizTaking == null)
            {
                return HttpNotFound();
            }

            quizTaking.Ended = DateTime.UtcNow;
            
            InMemoryQuizTakingRepository.Instance.Update(quizTaking);

            foreach (var questionWithAnswersInputViewModel in model.QuestionsWithAnswers)
            {
                var quizItemQuestionAnswer =
                    new QuizItemQuestionAnswer
                    {
                        QuizItemQuestionId = questionWithAnswersInputViewModel.QuizItemQuestionId,
                        QuizTakingId = quizTaking.Id,
                        UserSpecifiedAnswer = questionWithAnswersInputViewModel.UserSpecifiedAnswer
                    };

                InMemoryQuizItemQuestionAnswerRepository.Instance.Add(quizItemQuestionAnswer);
            }

            return RedirectToAction("QuizCompleted", new { model.Id });
        }
        
        public ActionResult QuizCompleted(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizTaking = InMemoryQuizTakingRepository.Instance.Get(id);
            if (quizTaking == null)
            {
                return HttpNotFound();
            }

            var model = QuizTakingCompleteViewModel.MapFrom(quizTaking);
            LoadRelationshipProperties(model);

            // TODO:
            // Trigger Logic App workflow for diploma and score calucation

            return View("QuizCompleted", model);
        }

        private void LoadRelationshipProperties(QuizTakingViewModel model)
        {
            var quizItem = InMemoryQuizItemRepository.Instance.Get(model.QuizItemId);

            model.QuizItemName = quizItem.Name;

            var quizItemQuestions = InMemoryQuizItemQuestionRepository.Instance.GetQuestionsForQuizItem(model.QuizItemId);

            model.QuestionsWithAnswers = quizItemQuestions.Select(QuestionWithAnswersInputViewModel.MapFrom).ToList();
        }

        private void LoadRelationshipProperties(QuizTakingCompleteViewModel model)
        {
            var quizItem = InMemoryQuizItemRepository.Instance.Get(model.QuizItemId);

            model.QuizItemName = quizItem.Name;
        }
    }
}