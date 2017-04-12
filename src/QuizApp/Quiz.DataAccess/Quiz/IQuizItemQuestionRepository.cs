using System.Collections.Generic;

namespace Quiz.DataAccess.Quiz
{
    public interface IQuizItemQuestionRepository
    {
        IEnumerable<QuizItemQuestion> GetAll();
        QuizItemQuestion Get(int id);
        IEnumerable<QuizItemQuestion> GetQuestionsForQuizItem(int quizItemId);
        int Add(QuizItemQuestion model);
        void Update(QuizItemQuestion model);
        void Delete(int id);
    }
}