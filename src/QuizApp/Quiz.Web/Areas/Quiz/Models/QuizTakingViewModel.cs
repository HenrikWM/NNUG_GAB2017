using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Quiz.Web.Areas.Quiz.Models
{
    public class QuizTakingViewModel
    {
        public int Id { get; set; }
        public int QuizItemId { get; set; }

        [DisplayName("Quiz")]
        public string QuizItemName { get; set; }

        [DisplayName("Your name")]
        public string ParticipantName { get; set; }

        [DisplayName("Score")]
        public double Score { get; set; }
        
        [DisplayName("Created at")]
        public DateTime Created { get; set; }

        [DisplayName("Ended at")]
        public DateTime? Ended { get; set; }

        public IList<QuestionWithAnswersInputViewModel> QuestionsWithAnswers { get; set; }

        public static QuizTakingViewModel MapFrom(DataAccess.Quiz.QuizItem model)
        {
            return new QuizTakingViewModel
            {
                QuizItemId = model.Id
            };
        }

        public static QuizTakingViewModel MapFrom(DataAccess.Quiz.QuizTaking model)
        {
            return new QuizTakingViewModel
            {
                Id = model.Id,
                QuizItemId = model.QuizItemId,
                Created = model.Created,
                Ended = model.Ended,
                ParticipantName = model.ParticipantName,
                Score = model.Score
            };
        }
    }
}