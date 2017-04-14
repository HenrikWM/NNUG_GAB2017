using System.Data.Entity;
using Quiz.Core.Quiz;

namespace Quiz.DataAccess.Ef
{
    public class QuizAppEntities : DbContext
    {
        public DbSet<QuizItem> QuizItems { get; set; }
        public DbSet<QuizItemQuestion> QuizItemQuestions { get; set; }
        public DbSet<QuizItemQuestionAnswer> QuizItemQuestionAnswers { get; set; }
        public DbSet<QuizTaking> QuizTakings { get; set; }

        public QuizAppEntities() : base("QuizAppSqlDb")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizItem>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<QuizItemQuestion>()
                .HasKey(o => o.Id)
                .HasRequired(o => o.QuizItem);

            modelBuilder.Entity<QuizItemQuestionAnswer>()
                .HasKey(o => o.Id)
                .HasRequired(o => o.QuizItemQuestion);

            modelBuilder.Entity<QuizTaking>()
                .HasKey(o => o.Id)
                .HasRequired(o => o.QuizItem)
                .WithMany(o => o.QuizTakings)
                .WillCascadeOnDelete(false);
        }
    }
}
