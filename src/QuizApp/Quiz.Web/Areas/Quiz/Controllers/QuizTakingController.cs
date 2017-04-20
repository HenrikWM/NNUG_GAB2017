using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;
using Quiz.DataAccess.Ef.Quiz.SqlDb;
using Quiz.DataAccess.InMemory.Quiz.InMemory;
using Quiz.Web.Areas.Quiz.Models;
using WebGrease.Css.Extensions;

namespace Quiz.Web.Areas.Quiz.Controllers
{
    public class QuizTakingController : Controller
    {
        private readonly IQuizItemRepository _quizItemRepository = InMemoryQuizItemRepository.Instance;
        //private readonly IQuizItemRepository _quizItemRepository = new SqlDbQuizItemRepository();

        private readonly IQuizTakingRepository _quizTakingRepository = InMemoryQuizTakingRepository.Instance;
        //private readonly IQuizTakingRepository _quizTakingRepository = new SqlDbQuizTakingRepository();

        private readonly IQuizItemQuestionAnswerRepository _quizItemQuestionAnswerRepository = InMemoryQuizItemQuestionAnswerRepository.Instance;
        //private readonly IQuizItemQuestionAnswerRepository _quizItemQuestionAnswerRepository = new SqlDbQuizItemQuestionAnswerRepository();

        private readonly IQuizItemQuestionRepository _quizItemQuestionRepository = InMemoryQuizItemQuestionRepository.Instance;
        //private readonly IQuizItemQuestionRepository _quizItemQuestionRepository = new SqlDbQuizItemQuestionRepository();

        public ActionResult Start(int quizItemId)
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
            
            var model = new QuizTakingViewModel { QuizItemId = quizItemId };
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

            var quizTaking = new QuizTaking
            {
                QuizItemId = model.QuizItemId,
                Created = DateTime.UtcNow,
                ParticipantName = model.ParticipantName
            };

            var id = _quizTakingRepository.Add(quizTaking);

            return RedirectToAction("InProgress", new { id });
        }

        public ActionResult InProgress(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizTaking = _quizTakingRepository.Get(id);
            if (quizTaking == null)
            {
                return HttpNotFound();
            }

            var model = QuizTakingViewModel.MapFromDataModel(quizTaking);
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

            var quizTaking = _quizTakingRepository.Get(model.Id);
            if (quizTaking == null)
            {
                return HttpNotFound();
            }

            quizTaking.Ended = DateTime.UtcNow;

            _quizTakingRepository.Update(quizTaking);

            var questionsWithAnswersDataModels = model.QuestionsWithAnswers.Select(
                o => QuestionWithAnswersInputViewModel.MapToDataModel(o, quizTaking.Id)
            );

            questionsWithAnswersDataModels.ForEach(
                quizItemQuestionAnswerDataModel =>
                _quizItemQuestionAnswerRepository.Add(quizItemQuestionAnswerDataModel)
            );
            
            return RedirectToAction("QuizCompleted", new { model.Id });
        }
        
        public ActionResult QuizCompleted(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var quizTaking = _quizTakingRepository.Get(id);
            if (quizTaking == null)
            {
                return HttpNotFound();
            }

            var model = QuizTakingCompleteViewModel.MapFromDataModel(quizTaking);
            LoadRelationshipProperties(model);

            // TODO:
            // Trigger Logic App workflow for diploma and score calucation

            return View("QuizCompleted", model);
        }

        private void LoadRelationshipProperties(QuizTakingViewModel model)
        {
            var quizItem = _quizItemRepository.Get(model.QuizItemId);
            model.QuizItemName = quizItem.Name;

            var quizItemQuestions = _quizItemQuestionRepository.GetQuestionsForQuizItem(model.QuizItemId);
            model.QuestionsWithAnswers = quizItemQuestions.Select(QuestionWithAnswersInputViewModel.MapFromDataModel).ToList();
        }

        private void LoadRelationshipProperties(QuizTakingCompleteViewModel model)
        {
            var quizItem = _quizItemRepository.Get(model.QuizItemId);

            model.QuizItemName = quizItem.Name;
        }
    }
}