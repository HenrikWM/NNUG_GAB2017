using System.Collections.Generic;
using System.Linq;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;

namespace Quiz.DataAccess.Ef.Quiz.SqlDb
{
    public class SqlDbQuizTakingRepository : IQuizTakingRepository
    {
        public IEnumerable<QuizTaking> GetAll()
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizTakings.ToList();
            }
        }

        public QuizTaking Get(int id)
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizTakings.FirstOrDefault(o => o.Id == id);
            }
        }

        public IEnumerable<QuizTaking> GetTakingsForQuizItem(int quizItemId)
        {
            return GetAll().Where(o => o.QuizItemId == quizItemId);
        }

        public int Add(QuizTaking model)
        {
            using (var context = new QuizAppEntities())
            {
                context.QuizTakings.Add(model);
                context.SaveChanges();

                return model.Id;
            }
        }

        public void Update(QuizTaking model)
        {
            using (var context = new QuizAppEntities())
            {
                var existingEntity = context.QuizTakings.FirstOrDefault(o => o.Id == model.Id);
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
                var existingEntity = context.QuizTakings.FirstOrDefault(o => o.Id == id);
                if (existingEntity != null)
                {
                    context.QuizTakings.Remove(existingEntity);
                    context.SaveChanges();
                }
            }
        }
    }
}
