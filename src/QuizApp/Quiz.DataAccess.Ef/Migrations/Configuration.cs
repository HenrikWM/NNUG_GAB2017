using Quiz.Core.Quiz;

namespace Quiz.DataAccess.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<QuizAppEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(QuizAppEntities context)
        {
            var created = DateTime.UtcNow.AddDays(-1);
            var ended = DateTime.UtcNow.AddDays(-1).AddMinutes(2);

            // Create a Pub Quiz
            var quizItem = new QuizItem
            {
                Created = created,
                Name = "Pub Quiz"
            };

            context.QuizItems.AddOrUpdate(o => new { o.Name }, quizItem);

            // Create Pub Quiz questions
            var quizItemQuestion1 = new QuizItemQuestion
            {
                Created = created,
                QuizItemId = quizItem.Id,
                QuizItem = quizItem,
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
                QuizItemId = quizItem.Id,
                QuizItem = quizItem,
                Question = "Which team won the Premier League in 2015/2016?",
                AnswerAlternative1 = "Liverpool",
                AnswerAlternative2 = "Manchester City",
                AnswerAlternative3 = "Leicester City",
                IsAnswerAlternative3Correct = true,
                AnswerAlternative4 = "Tottenham"
            };

            context.QuizItemQuestions.AddOrUpdate(o => new { o.Question }, quizItemQuestion1);
            context.QuizItemQuestions.AddOrUpdate(o => new { o.Question }, quizItemQuestion2);

            // Create a taken Pub Quiz
            var quizTaking = new QuizTaking
            {
                Created = created,
                Ended = ended,
                QuizItemId = quizItem.Id,
                QuizItem = quizItem,
                ParticipantName = "Ola Nordmann",
                Score = 200
            };

            context.QuizTakings.AddOrUpdate(o => new { o.ParticipantName }, quizTaking);

            // Create answers for Pub Quiz question 1
            var quizItemQuestionAnswer1 = new QuizItemQuestionAnswer
            {
                QuizItemQuestionId = quizItemQuestion1.Id,
                QuizItemQuestion = quizItemQuestion1,
                QuizTakingId = quizTaking.Id,
                QuizTaking = quizTaking,
                UserSpecifiedAnswer = "Mount Korab"
            };

            context.QuizItemQuestionAnswers.AddOrUpdate(o => new { o.UserSpecifiedAnswer }, quizItemQuestionAnswer1);

            // Create answers for Pub Quiz question 2
            var quizItemQuestionAnswer2 = new QuizItemQuestionAnswer
            {
                QuizItemQuestionId = quizItemQuestion2.Id,
                QuizItemQuestion = quizItemQuestion2,
                QuizTakingId = quizTaking.Id,
                QuizTaking = quizTaking,
                UserSpecifiedAnswer = "Leicester City"
            };

            context.QuizItemQuestionAnswers.AddOrUpdate(o => new { o.UserSpecifiedAnswer }, quizItemQuestionAnswer2);
        }
    }
}