using System.Collections.Generic;

namespace Quiz.DataAccess.Quiz
{
    public interface IQuizRepository
    {
        IEnumerable<QuizItem> GetAll();
        QuizItem Get(int id);
        void Add(QuizItem model);
        void Update(QuizItem model);
        void Delete(int id);
    }
}