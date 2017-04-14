namespace Quiz.Core.Quiz
{
    public class QuizItemQuestionAnswer
    {
        public int Id { get; set; }
        public int QuizItemQuestionId { get; set; }
        public int QuizTakingId { get; set; }
        public string UserSpecifiedAnswer { get; set; }
    }
}