using System;
using System.Collections.Generic;
using System.ComponentModel;
using Quiz.DataAccess.Quiz;

namespace Quiz.Web.Areas.Scores.Models
{
    public class HighScoreViewModel
    {
        [DisplayName("Quiz Id")]
        public int QuizItemId { get; set; }
        
        [DisplayName("Quiz Name")]
        public string QuizItemName { get; set; }

        [DisplayName("Score")]
        public double Score { get; set; }

        [DisplayName("Participant Name")]
        public string ParticipantName { get; set; }

        [DisplayName("Ended at")]
        public DateTime? Ended { get; set; }

        public static IEnumerable<HighScoreViewModel> MapFromDataModel(IEnumerable<QuizTaking> quizTakings)
        {
            var viewModel = new List<HighScoreViewModel>();
            foreach (var quizTaking in quizTakings)
            {
                viewModel.Add(
                    new HighScoreViewModel
                    {
                        QuizItemId = quizTaking.QuizItemId,
                        ParticipantName = quizTaking.ParticipantName,
                        Score = quizTaking.Score,
                        Ended = quizTaking.Ended
                    });
            }
            return viewModel;
        }
    }
}