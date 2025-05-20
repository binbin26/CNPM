using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcCourses
    {
        private FlowLayoutPanel flowPanelCourses;

        private void InitializeComponent()
        {
            this.flowPanelCourses = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThongke = new System.Windows.Forms.Button();
            this.flowPanelCourses.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowPanelCourses
            // 
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.Controls.Add(this.btnThongke);
            this.flowPanelCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelCourses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelCourses.Location = new System.Drawing.Point(0, 0);
            this.flowPanelCourses.Name = "flowPanelCourses";
            this.flowPanelCourses.Size = new System.Drawing.Size(800, 600);
            this.flowPanelCourses.TabIndex = 0;
            this.flowPanelCourses.WrapContents = false;
            // 
            // btnThongke
            // 
            this.btnThongke.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnThongke.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnThongke.Location = new System.Drawing.Point(3, 3);
            this.btnThongke.Name = "btnThongke";
            this.btnThongke.Size = new System.Drawing.Size(145, 62);
            this.btnThongke.TabIndex = 0;
            this.btnThongke.Text = "Thống kê";
            this.btnThongke.UseVisualStyleBackColor = true;
            // 
            // UcCourses
            // 
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcCourses";
            this.Size = new System.Drawing.Size(800, 600);
            this.flowPanelCourses.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Button btnThongke;
    }
}