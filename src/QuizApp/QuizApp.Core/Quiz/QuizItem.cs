using System;

namespace Quiz.Core.Quiz
{
    public class QuizItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
