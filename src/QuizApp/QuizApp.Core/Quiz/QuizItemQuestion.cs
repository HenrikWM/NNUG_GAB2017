using System;

namespace Quiz.Core.Quiz
{
    public class QuizItemQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public int QuizItemId { get; set; }

        public string AnswerAlternative1 { get; set; }
        public string AnswerAlternative2 { get; set; }
        public string AnswerAlternative3 { get; set; }
        public string AnswerAlternative4 { get; set; }

        public bool IsAnswerAlternative1Correct { get; set; }
        public bool IsAnswerAlternative2Correct { get; set; }
        public bool IsAnswerAlternative3Correct { get; set; }
        public bool IsAnswerAlternative4Correct { get; set; }
    }
}