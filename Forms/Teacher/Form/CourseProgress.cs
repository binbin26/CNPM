using CNPM.Models.Courses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class CourseProgress : Form
    {
        private int _courseId;
        private readonly AssignmentBLL _assignmentBLL = new AssignmentBLL();

        public CourseProgress(int courseId, string courseName)
        {
            InitializeComponent();
            _courseId = courseId;
            lblCourseName.Text = $"Khóa học: {courseName}";
            LoadCourseProgress();
        }

        private void LoadCourseProgress()
        {
            var data = _assignmentBLL.GetCourseProgress(_courseId);

            dtGCourseProgress.DataSource = data;

            // Thiết lập các cột
            dtGCourseProgress.Columns["FullName"].HeaderText = "Tên sinh viên";
            dtGCourseProgress.Columns["TotalAssignments"].HeaderText = "Số bài tập đã giao";
            dtGCourseProgress.Columns["SubmittedAssignments"].HeaderText = "Số bài đã nộp";
            dtGCourseProgress.Columns["CompletionRate"].HeaderText = "Tỉ lệ hoàn thành";
            dtGCourseProgress.Columns["AverageGrade"].HeaderText = "Điểm trung bình";
            dtGCourseProgress.Columns["Rating"].HeaderText = "Xếp loại";

            ShowCharts(data);
        }
        private void ShowCharts(List<ProgressReportDTO> data)
        {
            // Tỉ lệ hoàn thành bài tập
            var series1 = new System.Windows.Forms.DataVisualization.Charting.Series("Tỉ lệ hoàn thành");
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            int totalSubmitted = data.Sum(x => x.SubmittedAssignments);
            int totalAssigned = data.Sum(x => x.TotalAssignments);
            int notSubmitted = totalAssigned - totalSubmitted;

            series1.Points.AddXY("Đã nộp", totalSubmitted);
            series1.Points.AddXY("Chưa nộp", notSubmitted);

            chartProgress.Series.Clear();
            chartProgress.Series.Add(series1);
            chartProgress.Titles.Clear();
            chartProgress.Titles.Add("Tỉ lệ nộp bài");

            // Phân bố xếp loại
            var groups = data.GroupBy(x => x.Rating)
                             .Select(g => new { Rating = g.Key, Count = g.Count() });

            var series2 = new System.Windows.Forms.DataVisualization.Charting.Series("Phân bố xếp loại");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (var group in groups)
                series2.Points.AddXY(group.Rating, group.Count);

            chartGradeDistribution.Series.Clear();
            chartGradeDistribution.Series.Add(series2);
            chartGradeDistribution.Titles.Clear();
            chartGradeDistribution.Titles.Add("Phân bố xếp loại");
        }
    }
}
