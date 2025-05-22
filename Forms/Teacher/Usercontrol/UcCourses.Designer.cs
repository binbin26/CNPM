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
            this.flowPanelCourses.Controls.Add(this.btnThongke);
            this.flowPanelCourses.Dock = DockStyle.Fill;
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.FlowDirection = FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;
            this.flowPanelCourses.Padding = new Padding(10, 10, 10, 10);
            this.flowPanelCourses.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this.Size = new System.Drawing.Size(980, 700); // phù hợp với form chính
            this.ResumeLayout(false);
        }
        private Button btnThongke;
    }
}