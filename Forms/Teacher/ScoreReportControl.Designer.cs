namespace CNPM.Forms.Teacher
{
    partial class ScoreReportControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnViewScores;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnViewScores = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnViewScores
            this.btnViewScores.Text = "Xem báo cáo điểm";
            this.btnViewScores.Location = new System.Drawing.Point(20, 20);
            this.btnViewScores.Size = new System.Drawing.Size(150, 30);
            this.btnViewScores.Click += new System.EventHandler(this.btnViewScores_Click);

            // ScoreReportControl
            this.Controls.Add(this.btnViewScores);
            this.Name = "ScoreReportControl";
            this.Size = new System.Drawing.Size(800, 400);
            this.ResumeLayout(false);
        }
    }
}
