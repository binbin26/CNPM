namespace CNPM.Forms.Teacher
{
    partial class MultipleChoiceProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbAssignments;
        private System.Windows.Forms.DataGridView dgvPerformance;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnRefresh;

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
            this.cbAssignments = new System.Windows.Forms.ComboBox();
            this.dgvPerformance = new System.Windows.Forms.DataGridView();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPerformance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.SuspendLayout();

            // ComboBox - Assignment selector
            this.cbAssignments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAssignments.Location = new System.Drawing.Point(20, 20);
            this.cbAssignments.Size = new System.Drawing.Size(300, 24);
            this.cbAssignments.SelectedIndexChanged += new System.EventHandler(this.cbAssignments_SelectedIndexChanged);

            // DataGridView
            this.dgvPerformance.Location = new System.Drawing.Point(20, 60);
            this.dgvPerformance.Size = new System.Drawing.Size(760, 200);
            this.dgvPerformance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPerformance.ReadOnly = true;

            // Chart
            this.chartPerformance.Location = new System.Drawing.Point(20, 270);
            this.chartPerformance.Size = new System.Drawing.Size(500, 230);
            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("MainArea");
            var legend = new System.Windows.Forms.DataVisualization.Charting.Legend("Legend1");
            this.chartPerformance.ChartAreas.Add(chartArea);
            this.chartPerformance.Legends.Add(legend);

            // Buttons
            this.btnExportExcel.Location = new System.Drawing.Point(540, 270);
            this.btnExportExcel.Size = new System.Drawing.Size(100, 40);
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.btnRefresh.Location = new System.Drawing.Point(540, 370);
            this.btnRefresh.Size = new System.Drawing.Size(100, 40);
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 526);
            this.Controls.Add(this.cbAssignments);
            this.Controls.Add(this.dgvPerformance);
            this.Controls.Add(this.chartPerformance);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnRefresh);
            this.Name = "MultipleChoiceProgress";
            this.Text = "Phân tích hiệu suất bài tập trắc nghiệm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPerformance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}