using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.DataAccess.Quiz
{
    public class InMemoryQuizRepository : IQuizRepository
    {
        public static List<QuizItem> Items { get; set; }
        
        public InMemoryQuizRepository()
        {
            Items = new List<QuizItem>();
        }

        public IEnumerable<QuizItem> GetAll()
        {
            return Items;
        }

        public QuizItem Get(int id)
        {
            return Items.FirstOrDefault(o => o.Id == id);
        }

        public void Add(QuizItem model)
        {
            Items.Add(model);
        }

        public void Update(QuizItem model)
        {
            Delete(model.Id);

            Items.Add(model);
        }

        public void Delete(int id)
        {
            var existingItem = Get(id);
            if (existingItem == null)
                return;

            Items.Remove(existingItem);
        }
    }
}