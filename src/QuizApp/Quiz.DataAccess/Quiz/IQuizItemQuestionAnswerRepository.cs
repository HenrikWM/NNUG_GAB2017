using System.Collections.Generic;

namespace Quiz.DataAccess.Quiz
{
    public interface IQuizItemQuestionAnswerRepository
    {
        IEnumerable<QuizItemQuestionAnswer> GetAll();
        QuizItemQuestionAnswer Get(int id);
        IEnumerable<QuizItemQuestionAnswer> GetQuestionAnswersForQuizItemQuestion(int quizItemQuestionId);
        void Add(QuizItemQuestionAnswer model);
    }
}