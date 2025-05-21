namespace CNPM.Models.Assignments
{
    public class QuestionStatsDTO
    {
        public int QuestionID { get; set; }

        public string Content { get; set; }

        public int TotalAnswers { get; set; }

        public double CorrectRate { get; set; }

        public double WrongRate
        {
            get { return 100 - CorrectRate; }
        }

        public string Difficulty { get; set; }
    }
}
