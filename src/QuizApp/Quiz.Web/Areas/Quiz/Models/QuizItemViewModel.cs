using System.ComponentModel;
using System.Linq;

namespace Quiz.Web.Areas.Quiz.Models
{
    public class QuizItemViewModel
    {
        public static QuizItemViewModel MapFrom(DataAccess.Quiz.QuizItem model)
        {
            return new QuizItemViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Created = model.Created.ToShortDateString() + " " + model.Created.ToShortTimeString(),
                Modified = model.Modified?.ToShortDateString() + " " + model.Modified?.ToShortTimeString(),
                QuestionCount = model.Questions.Count()
            };
        }

        [DisplayName("No. of question")]
        public int QuestionCount { get; set; }

        [DisplayName("Modified at")]
        public string Modified { get; set; }

        [DisplayName("Created at")]
        public string Created { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Id")]
        public int Id { get; set; }
    }
}