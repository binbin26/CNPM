using CNPM.BLL;
using CNPM.Models.Assignments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CNPM.Forms.Teacher
{
    public partial class MultipleChoiceProgress : Form
    {
        private int currentCourseId;
        private int currentAssignmentId;
        public MultipleChoiceProgress(int courseId)
        {
            InitializeComponent();
            currentCourseId = courseId;
            LoadAssignments();
        }


        private void LoadAssignments()
        {
            var allAssignments = new AssignmentBLL().GetAssignmentsByCourse(currentCourseId);
            var mcAssignments = allAssignments
                //.Where(a => a.AssignmentType == AssignmentTypes.TracNghiem)
                .Select(a => new KeyValuePair<int, string>(a.AssignmentID, a.Title))
                .ToList();

            cbAssignments.DataSource = mcAssignments;
            cbAssignments.DisplayMember = "Value";
            cbAssignments.ValueMember = "Key";
        }

        private void cbAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAssignments.SelectedValue != null)
            {
                int assignmentId = (int)cbAssignments.SelectedValue;
                LoadPerformanceData(assignmentId);
            }
        }

        private void dgvPerformance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentAssignmentId = Convert.ToInt32(dgvPerformance.Rows[e.RowIndex].Cells["AssignmentID"].Value);
            }
        }

        private void LoadPerformanceData(int assignmentId)
        {
            var data = new AssignmentBLL().GetPerformance(assignmentId);
            dgvPerformance.DataSource = data;
            DrawChart(data);
        }

        private void DrawChart(List<QuestionStatsDTO> data)
        {
            chartPerformance.Series.Clear();
            var series = new Series("Correct %");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.SeaGreen;

            foreach (var item in data)
            {
                series.Points.AddXY($"Q{item.QuestionID}", item.CorrectRate);
            }

            chartPerformance.Series.Add(series);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (currentAssignmentId == 0)
            {
                MessageBox.Show("Vui lòng chọn một bài tập để xuất báo cáo.");
                return;
            }

            var bll = new AssignmentBLL();
            var data = bll.GetPerformance(currentAssignmentId);
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Chọn nơi lưu báo cáo";
                saveDialog.FileName = "PhanTichCauHoi.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    bll.ExportQuestionStatsToExcel(data, saveDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!");
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cbAssignments.SelectedValue != null)
            {
                int assignmentId = (int)cbAssignments.SelectedValue;
                LoadPerformanceData(assignmentId);
            }
        }
    }
}