using System.Collections.Generic;
using System.Linq;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;

namespace Quiz.DataAccess.Ef.Quiz.SqlDb
{
    public class SqlDbQuizItemQuestionAnswerRepository : IQuizItemQuestionAnswerRepository
    {
        public IEnumerable<QuizItemQuestionAnswer> GetAll()
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizItemQuestionAnswers.ToList();
            }
        }

        public QuizItemQuestionAnswer Get(int id)
        {
            using (var context = new QuizAppEntities())
            {
                return context.QuizItemQuestionAnswers.FirstOrDefault(o => o.Id == id);
            }
        }

        public IEnumerable<QuizItemQuestionAnswer> GetQuestionAnswersForQuizItemQuestion(int quizItemQuestionId)
        {
            return GetAll().Where(o => o.QuizItemQuestionId == quizItemQuestionId);
        }
        
        public void Add(QuizItemQuestionAnswer model)
        {
            using (var context = new QuizAppEntities())
            {
                context.QuizItemQuestionAnswers.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new QuizAppEntities())
            {
                var existingEntity = context.QuizItemQuestionAnswers.FirstOrDefault(o => o.Id == id);
                if (existingEntity != null)
                {
                    context.QuizItemQuestionAnswers.Remove(existingEntity);
                    context.SaveChanges();
                }
            }
        }
    }
}
