using System.Collections.Generic;

namespace Quiz.DataAccess.Quiz
{
    public interface IQuizItemRepository
    {
        IEnumerable<QuizItem> GetAll();
        QuizItem Get(int id);
        int Add(QuizItem model);
        void Update(QuizItem model);
        void Delete(int id);
    }
}