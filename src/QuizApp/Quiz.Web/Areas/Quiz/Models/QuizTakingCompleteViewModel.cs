using Quiz.Core.Quiz;

namespace Quiz.Web.Areas.Quiz.Models
{
    public class QuizTakingCompleteViewModel
    {
        public string ParticipantName { get; set; }
        public string QuizItemName { get; set; }
        public int QuizItemId { get; set; }

        public static QuizTakingCompleteViewModel MapFromDataModel(QuizTaking model)
        {
            return new QuizTakingCompleteViewModel
            {
                QuizItemId = model.QuizItemId,
                ParticipantName = model.ParticipantName
            };
        }
    }
}