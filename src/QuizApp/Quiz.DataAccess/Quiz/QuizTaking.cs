using System;

namespace Quiz.DataAccess.Quiz
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