namespace Quiz.DataAccess.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizItemQuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizItemQuestionId = c.Int(nullable: false),
                        QuizTakingId = c.Int(nullable: false),
                        UserSpecifiedAnswer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizItemQuestions", t => t.QuizItemQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.QuizTakings", t => t.QuizTakingId, cascadeDelete: true)
                .Index(t => t.QuizItemQuestionId)
                .Index(t => t.QuizTakingId);
            
            CreateTable(
                "dbo.QuizItemQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        QuizItemId = c.Int(nullable: false),
                        AnswerAlternative1 = c.String(),
                        AnswerAlternative2 = c.String(),
                        AnswerAlternative3 = c.String(),
                        AnswerAlternative4 = c.String(),
                        IsAnswerAlternative1Correct = c.Boolean(nullable: false),
                        IsAnswerAlternative2Correct = c.Boolean(nullable: false),
                        IsAnswerAlternative3Correct = c.Boolean(nullable: false),
                        IsAnswerAlternative4Correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizItems", t => t.QuizItemId, cascadeDelete: true)
                .Index(t => t.QuizItemId);
            
            CreateTable(
                "dbo.QuizItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuizTakings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizItemId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Ended = c.DateTime(),
                        ParticipantName = c.String(),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizItems", t => t.QuizItemId)
                .Index(t => t.QuizItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizItemQuestionAnswers", "QuizTakingId", "dbo.QuizTakings");
            DropForeignKey("dbo.QuizItemQuestionAnswers", "QuizItemQuestionId", "dbo.QuizItemQuestions");
            DropForeignKey("dbo.QuizItemQuestions", "QuizItemId", "dbo.QuizItems");
            DropForeignKey("dbo.QuizTakings", "QuizItemId", "dbo.QuizItems");
            DropIndex("dbo.QuizTakings", new[] { "QuizItemId" });
            DropIndex("dbo.QuizItemQuestions", new[] { "QuizItemId" });
            DropIndex("dbo.QuizItemQuestionAnswers", new[] { "QuizTakingId" });
            DropIndex("dbo.QuizItemQuestionAnswers", new[] { "QuizItemQuestionId" });
            DropTable("dbo.QuizTakings");
            DropTable("dbo.QuizItems");
            DropTable("dbo.QuizItemQuestions");
            DropTable("dbo.QuizItemQuestionAnswers");
        }
    }
}
