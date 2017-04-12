using System.Collections.Generic;
using System.Linq;

namespace Quiz.DataAccess.Quiz
{
    public sealed class InMemoryQuizItemQuestionAnswerRepository : IQuizItemQuestionAnswerRepository
    {
        private static volatile InMemoryQuizItemQuestionAnswerRepository _instance;
        private static readonly object SyncRoot = new object();

        private InMemoryQuizItemQuestionAnswerRepository()
        {
            Items = new List<QuizItemQuestionAnswer>();
        }

        public static InMemoryQuizItemQuestionAnswerRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new InMemoryQuizItemQuestionAnswerRepository();
                    }
                }

                return _instance;
            }
        }

        public static List<QuizItemQuestionAnswer> Items { get; set; }

        public IEnumerable<QuizItemQuestionAnswer> GetAll()
        {
            return Items;
        }

        public QuizItemQuestionAnswer Get(int id)
        {
            return Items.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<QuizItemQuestionAnswer> GetQuestionAnswersForQuizItemQuestion(int quizItemQuestionId)
        {
            return GetAll().Where(o => o.QuizItemQuestionId == quizItemQuestionId);
        }

        public void Add(QuizItemQuestionAnswer model)
        {
            model.Id = GetNextId();
            Items.Add(model);
        }
        
        private int GetNextId()
        {
            return GetAll().Any() ? GetAll().Max(o => o.Id) + 1 : 1;
        }
    }
}