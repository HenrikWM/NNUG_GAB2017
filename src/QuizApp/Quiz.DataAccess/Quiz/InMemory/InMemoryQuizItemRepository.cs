using System.Collections.Generic;
using System.Linq;
using Quiz.Core.Quiz;
using Quiz.DataAccess.Abstractions.Quiz;

namespace Quiz.DataAccess.Quiz.InMemory
{
    public sealed class InMemoryQuizItemRepository : IQuizItemRepository
    {
        private static volatile InMemoryQuizItemRepository _instance;
        private static readonly object SyncRoot = new object();

        private InMemoryQuizItemRepository()
        {
            Items = new List<QuizItem>();
        }

        public static InMemoryQuizItemRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new InMemoryQuizItemRepository();
                    }
                }

                return _instance;
            }
        }

        public static List<QuizItem> Items { get; set; }

        public IEnumerable<QuizItem> GetAll()
        {
            return Items;
        }

        public QuizItem Get(int id)
        {
            return Items.FirstOrDefault(o => o.Id == id);
        }

        public int Add(QuizItem model)
        {
            model.Id = GetNextId();
            Items.Add(model);

            return model.Id;
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

            // Cascading delete of foreign keys
            var quizItemQuestions =
                InMemoryQuizItemQuestionRepository.Instance.GetQuestionsForQuizItem(existingItem.Id).ToList();
            quizItemQuestions.ForEach(o => InMemoryQuizItemQuestionRepository.Instance.Delete(o.Id));

            var quizTakings =
                InMemoryQuizTakingRepository.Instance.GetTakingsForQuizItem(existingItem.Id).ToList();
            quizTakings.ForEach(o => InMemoryQuizTakingRepository.Instance.Delete(o.Id));
            
            Items.Remove(existingItem);
        }

        private int GetNextId()
        {
            return GetAll().Any() ? GetAll().Max(o => o.Id) + 1 : 1;
        }
    }
}