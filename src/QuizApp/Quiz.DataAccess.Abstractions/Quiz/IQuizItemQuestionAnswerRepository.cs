using System.Collections.Generic;
using Quiz.Core.Quiz;

namespace Quiz.DataAccess.Abstractions.Quiz
{
    public interface IQuizItemQuestionAnswerRepository
    {
        IEnumerable<QuizItemQuestionAnswer> GetAll();
        QuizItemQuestionAnswer Get(int id);
        IEnumerable<QuizItemQuestionAnswer> GetQuestionAnswersForQuizItemQuestion(int quizItemQuestionId);
        void Add(QuizItemQuestionAnswer model);
        void Delete(int id);
    }
}