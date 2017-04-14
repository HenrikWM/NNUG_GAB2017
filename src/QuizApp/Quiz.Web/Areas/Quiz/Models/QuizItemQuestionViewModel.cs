using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Quiz.Core.Quiz;

namespace Quiz.Web.Areas.Quiz.Models
{
    public class QuizItemQuestionViewModel
    {
        public static QuizItemQuestionViewModel MapFromDataModel(QuizItemQuestion model)
        {
            return new QuizItemQuestionViewModel
            {
                Id = model.Id,
                Question = model.Question,
                Created = model.Created.ToShortDateString() + " " + model.Created.ToShortTimeString(),
                Modified = model.Modified?.ToShortDateString() + " " + model.Modified?.ToShortTimeString(),
                QuizItemId = model.QuizItemId,
                AnswerAlternative1 = model.AnswerAlternative1,
                AnswerAlternative2 = model.AnswerAlternative2,
                AnswerAlternative3 = model.AnswerAlternative3,
                AnswerAlternative4 = model.AnswerAlternative4,
                IsAnswerAlternative1Correct = model.IsAnswerAlternative1Correct,
                IsAnswerAlternative2Correct = model.IsAnswerAlternative2Correct,
                IsAnswerAlternative3Correct = model.IsAnswerAlternative3Correct,
                IsAnswerAlternative4Correct = model.IsAnswerAlternative4Correct
            };
        }
        
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Question")]
        public string Question { get; set; }

        [DisplayName("Modified at")]
        public string Modified { get; set; }

        [DisplayName("Created at")]
        public string Created { get; set; }
        
        public int QuizItemId { get; set; }

        [DisplayName("Quiz")]
        public string QuizItemName { get; set; }

        [Required]
        [DisplayName("Answer 1")]
        public string AnswerAlternative1 { get; set; }

        [Required]
        [DisplayName("Answer 2")]
        public string AnswerAlternative2 { get; set; }

        [DisplayName("Answer 3")]
        public string AnswerAlternative3 { get; set; }

        [DisplayName("Answer 4")]
        public string AnswerAlternative4 { get; set; }

        [DisplayName("Answer 1 is correct")]
        public bool IsAnswerAlternative1Correct { get; set; }

        [DisplayName("Answer 2 is correct")]
        public bool IsAnswerAlternative2Correct { get; set; }

        [DisplayName("Answer 3 is correct")]
        public bool IsAnswerAlternative3Correct { get; set; }

        [DisplayName("Answer 4 is correct")]
        public bool IsAnswerAlternative4Correct { get; set; }
    }
}