using System;

namespace Quiz.DataAccess.Quiz
{
    public class QuizItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
