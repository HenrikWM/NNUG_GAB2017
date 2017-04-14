using System.Collections.Generic;
using System.Linq;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;

namespace Quiz.DataAccess.Quiz.InMemory
{
    public sealed class InMemoryQuizTakingRepository : IQuizTakingRepository
    {
        private static volatile InMemoryQuizTakingRepository _instance;
        private static readonly object SyncRoot = new object();

        private InMemoryQuizTakingRepository()
        {
            Items = new List<QuizTaking>();
        }

        public static InMemoryQuizTakingRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new InMemoryQuizTakingRepository();
                    }
                }

                return _instance;
            }
        }

        public static List<QuizTaking> Items { get; set; }
        
        public IEnumerable<QuizTaking> GetAll()
        {
            return Items;
        }

        public QuizTaking Get(int id)
        {
            return Items.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<QuizTaking> GetTakingsForQuizItem(int quizItemId)
        {
            return GetAll().Where(o => o.QuizItemId == quizItemId);
        }

        public int Add(QuizTaking model)
        {
            model.Id = GetNextId();
            Items.Add(model);

            return model.Id;
        }

        public void Update(QuizTaking model)
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
        
        private int GetNextId()
        {
            return GetAll().Any() ? GetAll().Max(o => o.Id) + 1 : 1;
        }
    }
}