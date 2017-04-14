using System.Collections.Generic;
using System.Linq;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;

namespace Quiz.DataAccess.Ef.Quiz.SqlDb
{
    public class SqlDbQuizItemQuestionRepository : IQuizItemQuestionRepository
    {
        public IEnumerable<QuizItemQuestion> GetAll()
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizItemQuestions.ToList();
            }
        }

        public QuizItemQuestion Get(int id)
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizItemQuestions.FirstOrDefault(o => o.Id == id);
            }
        }

        public IEnumerable<QuizItemQuestion> GetQuestionsForQuizItem(int quizItemId)
        {
            return GetAll().Where(o => o.QuizItemId == quizItemId);
        }

        public int Add(QuizItemQuestion model)
        {
            using (var context = new QuizAppEntities())
            {
                context.QuizItemQuestions.Add(model);
                context.SaveChanges();

                return model.Id;
            }
        }

        public void Update(QuizItemQuestion model)
        {
            using (var context = new QuizAppEntities())
            {
                var existingEntity = context.QuizItemQuestions.FirstOrDefault(o => o.Id == model.Id);
                if (existingEntity == null)
                    return;

                context.Entry(existingEntity).CurrentValues.SetValues(model);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new QuizAppEntities())
            {
                var existingEntity = context.QuizItemQuestions.FirstOrDefault(o => o.Id == id);
                if (existingEntity != null)
                {
                    context.QuizItemQuestions.Remove(existingEntity);
                    context.SaveChanges();
                }
            }
        }
    }
}
