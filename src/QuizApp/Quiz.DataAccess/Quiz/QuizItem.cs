using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Quiz.DataAccess.Quiz
{
    public class QuizItem
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DisplayName("Modified")]
        public DateTime? Modified { get; set; }

        [DisplayName("No. of questions")]
        public IEnumerable<QuizItemQuestion> Questions { get; set; }
    }
}
