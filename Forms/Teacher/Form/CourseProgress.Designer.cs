namespace CNPM.Forms.Teacher
{
    partial class CourseProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCourseName = new System.Windows.Forms.Label();
            this.dtGCourseProgress = new System.Windows.Forms.DataGridView();
            this.chartProgress = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartGradeDistribution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dtGCourseProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGradeDistribution)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(366, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thống kê khóa học";
            // 
            // lblCourseName
            // 
            this.lblCourseName.AutoSize = true;
            this.lblCourseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblCourseName.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblCourseName.Location = new System.Drawing.Point(31, 80);
            this.lblCourseName.Name = "lblCourseName";
            this.lblCourseName.Size = new System.Drawing.Size(81, 29);
            this.lblCourseName.TabIndex = 1;
            this.lblCourseName.Text = "label2";
            this.lblCourseName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtGCourseProgress
            // 
            this.dtGCourseProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGCourseProgress.Location = new System.Drawing.Point(2, 112);
            this.dtGCourseProgress.Name = "dtGCourseProgress";
            this.dtGCourseProgress.RowHeadersWidth = 51;
            this.dtGCourseProgress.RowTemplate.Height = 24;
            this.dtGCourseProgress.Size = new System.Drawing.Size(895, 248);
            this.dtGCourseProgress.TabIndex = 2;
            // 
            // chartProgress
            // 
            chartArea1.Name = "ChartArea1";
            this.chartProgress.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartProgress.Legends.Add(legend1);
            this.chartProgress.Location = new System.Drawing.Point(2, 364);
            this.chartProgress.Name = "chartProgress";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartProgress.Series.Add(series1);
            this.chartProgress.Size = new System.Drawing.Size(300, 193);
            this.chartProgress.TabIndex = 3;
            this.chartProgress.Text = "chartProgress";
            // 
            // chartGradeDistribution
            // 
            chartArea2.Name = "ChartArea1";
            this.chartGradeDistribution.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartGradeDistribution.Legends.Add(legend2);
            this.chartGradeDistribution.Location = new System.Drawing.Point(597, 364);
            this.chartGradeDistribution.Name = "chartGradeDistribution";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartGradeDistribution.Series.Add(series2);
            this.chartGradeDistribution.Size = new System.Drawing.Size(300, 193);
            this.chartGradeDistribution.TabIndex = 4;
            this.chartGradeDistribution.Text = "chartGradeDistribution";
            // 
            // CourseProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 561);
            this.Controls.Add(this.chartGradeDistribution);
            this.Controls.Add(this.chartProgress);
            this.Controls.Add(this.dtGCourseProgress);
            this.Controls.Add(this.lblCourseName);
            this.Controls.Add(this.label1);
            this.Name = "CourseProgress";
            this.Text = "CourseProgress";
            ((System.ComponentModel.ISupportInitialize)(this.dtGCourseProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGradeDistribution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCourseName;
        private System.Windows.Forms.DataGridView dtGCourseProgress;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartProgress;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGradeDistribution;
    }
}