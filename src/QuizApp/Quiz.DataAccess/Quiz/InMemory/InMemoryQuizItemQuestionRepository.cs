using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.DataAccess.Quiz.InMemory
{
    public sealed class InMemoryQuizItemQuestionRepository : IQuizItemQuestionRepository
    {
        private static volatile InMemoryQuizItemQuestionRepository _instance;
        private static readonly object SyncRoot = new object();

        private InMemoryQuizItemQuestionRepository()
        {
            Items = new List<QuizItemQuestion>();
        }

        public static InMemoryQuizItemQuestionRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new InMemoryQuizItemQuestionRepository();
                    }
                }

                return _instance;
            }
        }

        public static List<QuizItemQuestion> Items { get; set; }
        
        public IEnumerable<QuizItemQuestion> GetAll()
        {
            return Items;
        }

        public QuizItemQuestion Get(int id)
        {
            return Items.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<QuizItemQuestion> GetQuestionsForQuizItem(int quizItemId)
        {
            return GetAll().Where(o => o.QuizItemId == quizItemId);
        }

        public int Add(QuizItemQuestion model)
        {
            model.Id = GetNextId();
            Items.Add(model);

            return model.Id;
        }

        public void Update(QuizItemQuestion model)
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