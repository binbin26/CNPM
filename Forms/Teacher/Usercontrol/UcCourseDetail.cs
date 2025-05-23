﻿using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CNPM.DAL;
using CNPM.Models.Courses;

namespace CNPM.Forms.Teacher
{
    public partial class UcCourseDetail : UserControl
    {
        private Course currentCourse;
        private int TeacherID => currentCourse.TeacherID;

        public UcCourseDetail(Course course)
        {
            InitializeComponent();
            currentCourse = course;
            lblCourseName.Text = course.CourseName;
            LoadSessions();
        }

        private void LoadSessions()
        {
            flowPanelSessions.Controls.Clear();
            string query = "SELECT SessionID, Title FROM Sessions WHERE CourseID = @CourseID ORDER BY CreatedAt ASC";

            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CourseID", currentCourse.CourseID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int sessionId = reader.GetInt32(0);
                        string title = reader.GetString(1);

                        var sessionItem = new UcSessionItem(currentCourse.TeacherID, currentCourse.CourseID, sessionId, title);
                        sessionItem.Width = flowPanelSessions.Width - 30;
                        flowPanelSessions.Controls.Add(sessionItem);
                    }
                }
            }
        }
        private void btnAddSession_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new FormInputTitle();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string title = form.InputTitle;
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        MessageBox.Show("Tiêu đề không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (title.Length > 100)
                    {
                        MessageBox.Show("Tiêu đề quá dài! Tối đa 100 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string query = "INSERT INTO Sessions (CourseID, Title, CreatedAt) OUTPUT INSERTED.SessionID VALUES (@CourseID, @Title, GETDATE())";
                    using (var conn = DatabaseHelper.GetConnection())
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourseID", currentCourse.CourseID);
                        cmd.Parameters.AddWithValue("@Title", title);
                        conn.Open();
                        int sessionId = (int)cmd.ExecuteScalar();

                        var sessionItem = new UcSessionItem(currentCourse.TeacherID, currentCourse.CourseID, sessionId, title);
                        sessionItem.Width = flowPanelSessions.Width - 30;
                        flowPanelSessions.Controls.Add(sessionItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm buổi học:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}