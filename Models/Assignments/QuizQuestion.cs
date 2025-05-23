using System;
using System.Collections.Generic;

namespace CNPM.Models.Assignments
{
    public class QuizQuestion
    {
        public int QuestionID { get; set; }
        public int AssignmentID { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public int Score { get; set; }
    }
} 