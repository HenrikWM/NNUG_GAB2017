using System.ComponentModel;
using Quiz.DataAccess.Quiz;

namespace Quiz.Web.Areas.Quiz.Models
{
    public class QuestionWithAnswersInputViewModel
    {
        public int QuizItemQuestionId { get; set; }

        [DisplayName("Answer 1")]
        public string Answer1 { get; set; }

        [DisplayName("Answer 2")]
        public string Answer2 { get; set; }

        [DisplayName("Answer 3")]
        public string Answer3 { get; set; }

        [DisplayName("Answer 4")]
        public string Answer4 { get; set; }

        public string Question { get; set; }

        public static QuestionWithAnswersInputViewModel MapFrom(QuizItemQuestion model)
        {
            return new QuestionWithAnswersInputViewModel
            {
                QuizItemQuestionId = model.Id,
                Question = model.Question,
                Answer1 = model.AnswerAlternative1,
                Answer2 = model.AnswerAlternative2,
                Answer3 = model.AnswerAlternative3,
                Answer4 = model.AnswerAlternative4
            };
        }
    }
}