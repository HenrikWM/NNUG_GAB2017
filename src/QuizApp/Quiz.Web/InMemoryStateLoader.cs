using System;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Quiz;
using Quiz.DataAccess.Quiz.InMemory;

namespace Quiz.Web
{
    public static class InMemoryStateLoader
    {
        public static void Load()
        {
            var created = DateTime.UtcNow.AddDays(-1);
            var ended = DateTime.UtcNow.AddDays(-1).AddMinutes(2);

            // Create a Pub Quiz
            var quizItem = new QuizItem
            {
                Created = created,
                Name = "Pub Quiz"
            };

            var quizItemId = InMemoryQuizItemRepository.Instance.Add(quizItem);

            // Create Pub Quiz questions
            var quizItemQuestion1 = new QuizItemQuestion
            {
                Created = created,
                QuizItemId = quizItemId,
                Question = "What is the hightest mountain in Europe?",
                AnswerAlternative1 = "Mount Korab",
                IsAnswerAlternative1Correct = true,
                AnswerAlternative2 = "Kilimanjaro",
                AnswerAlternative3 = "Gaustadtoppen",
                AnswerAlternative4 = "Trysilfjellet"
            };
            var quizItemQuestion2 = new QuizItemQuestion
            {
                Created = created,
                QuizItemId = quizItemId,
                Question = "Which team won the Premier League in 2015/2016?",
                AnswerAlternative1 = "Liverpool",
                AnswerAlternative2 = "Manchester City",
                AnswerAlternative3 = "Leicester City",
                IsAnswerAlternative3Correct = true,
                AnswerAlternative4 = "Tottenham"
            };

            var quizItemQuestion1Id = InMemoryQuizItemQuestionRepository.Instance.Add(quizItemQuestion1);
            var quizItemQuestion2Id = InMemoryQuizItemQuestionRepository.Instance.Add(quizItemQuestion2);
            
            // Create a taken Pub Quiz
            var quizTaking = new QuizTaking
            {
                Created = created,
                Ended = ended,
                QuizItemId = quizItemId,
                ParticipantName = "Ola Nordmann",
                Score = 200
            };

            var quizTakingId = InMemoryQuizTakingRepository.Instance.Add(quizTaking);

            // Create answers for Pub Quiz question 1
            var quizItemQuestionAnswer1 = new QuizItemQuestionAnswer
            {
                QuizItemQuestionId = quizItemQuestion1Id,
                QuizTakingId = quizTakingId,
                UserSpecifiedAnswer = "Mount Korab"
            };

            InMemoryQuizItemQuestionAnswerRepository.Instance.Add(quizItemQuestionAnswer1);

            // Create answers for Pub Quiz question 2
            var quizItemQuestionAnswer2 = new QuizItemQuestionAnswer
            {
                QuizItemQuestionId = quizItemQuestion2Id,
                QuizTakingId = quizTakingId,
                UserSpecifiedAnswer = "Leicester City"
            };

            InMemoryQuizItemQuestionAnswerRepository.Instance.Add(quizItemQuestionAnswer2);
        }
    }
}