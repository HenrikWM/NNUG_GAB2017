﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Quiz.Core.Quiz;

namespace Quiz.Web.Areas.Quiz.Models
{
    public class QuizItemViewModel
    {
        public QuizItemViewModel()
        {
            Questions = new List<QuizItemQuestionViewModel>();
        }

        public static QuizItemViewModel MapFromDataModel(QuizItem model)
        {
            return new QuizItemViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Created = model.Created.ToShortDateString() + " " + model.Created.ToShortTimeString(),
                Modified = model.Modified?.ToShortDateString() + " " + model.Modified?.ToShortTimeString()
            };
        }
        
        [DisplayName("Id")]
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        
        [DisplayName("Modified at")]
        public string Modified { get; set; }

        [DisplayName("Created at")]
        public string Created { get; set; }

        [DisplayName("No. of questions")]
        public int QuestionCount => Questions.Count();

        public IEnumerable<QuizItemQuestionViewModel> Questions { get; set; }
    }
}