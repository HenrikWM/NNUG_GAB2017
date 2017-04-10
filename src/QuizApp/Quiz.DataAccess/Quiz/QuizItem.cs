using System;
using System.Collections.Generic;

namespace Quiz.DataAccess.Quiz
{
    public class QuizItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public IEnumerable<QuizItemQuestion> Questions { get; set; }
    }
}
