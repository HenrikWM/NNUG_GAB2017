namespace Quiz.DataAccess.Quiz
{
    public class QuizItemQuestionAnswer
    {
        public int Id { get; set; }
        public int QuizItemQuestionId { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
    }
}