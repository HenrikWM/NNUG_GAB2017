using System;

namespace Quiz.Core.Quiz
{
    public class QuizTaking
    {
        public int Id { get; set; }
        public int QuizItemId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Ended { get; set; }
        public string ParticipantName { get; set; }
        public double Score { get; set; }
    }
}