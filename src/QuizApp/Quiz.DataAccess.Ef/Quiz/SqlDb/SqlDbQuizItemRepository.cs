using System.Collections.Generic;
using System.Linq;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;

namespace Quiz.DataAccess.Ef.Quiz.SqlDb
{
    public class SqlDbQuizItemRepository : IQuizItemRepository
    {
        public IEnumerable<QuizItem> GetAll()
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizItems.ToList();
            }
        }

        public QuizItem Get(int id)
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizItems.FirstOrDefault(o => o.Id == id);
            }
        }

        public int Add(QuizItem model)
        {
            using (var context = new QuizAppEntities())
            {
                context.QuizItems.Add(model);
                context.SaveChanges();

                return model.Id;
            }
        }

        public void Update(QuizItem model)
        {
            using (var context = new QuizAppEntities())
            {
                var existingEntity = context.QuizItems.FirstOrDefault(o => o.Id == model.Id);
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
                var existingEntity = context.QuizItems.FirstOrDefault(o => o.Id == id);
                if (existingEntity != null)
                {
                    context.QuizItems.Remove(existingEntity);
                    context.SaveChanges();
                }
            }
        }
    }
}
