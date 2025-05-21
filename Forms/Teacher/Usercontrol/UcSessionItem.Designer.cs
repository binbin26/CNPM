using System.Windows.Forms;
using System.Drawing;
using System;
using FontAwesome.Sharp;

namespace CNPM.Forms.Teacher
{
    partial class UcSessionItem
    {
        private Label lblSessionTitle;
        private FlowLayoutPanel flowPanelAssignments;
        private Panel panelScrollableContent;
        private IconButton btnAttachFile;
        private IconButton btnCreateAssignment;
        private IconButton btnDeleteSession;

        private void InitializeComponent()
        {
            this.lblSessionTitle = new Label();
            this.btnAttachFile = new IconButton();
            this.btnCreateAssignment = new IconButton();
            this.flowPanelAssignments = new FlowLayoutPanel();
            this.panelScrollableContent = new Panel();
            this.btnDeleteSession = new IconButton();

            // lblSessionTitle
            this.lblSessionTitle.Text = "Buổi học";
            this.lblSessionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblSessionTitle.Dock = DockStyle.Top;
            this.lblSessionTitle.Height = 40;
            this.lblSessionTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSessionTitle.Padding = new Padding(10, 0, 0, 0);
            this.lblSessionTitle.ForeColor = Color.FromArgb(30, 30, 60);

            // btnDeleteSession
            this.btnDeleteSession = new IconButton();
            this.btnDeleteSession.IconChar = IconChar.Times;
            this.btnDeleteSession.IconColor = Color.Red;
            this.btnDeleteSession.IconSize = 16;
            this.btnDeleteSession.Size = new Size(30, 30);
            this.btnDeleteSession.FlatStyle = FlatStyle.Flat;
            this.btnDeleteSession.FlatAppearance.BorderSize = 0;
            this.btnDeleteSession.BackColor = Color.Transparent;
            this.btnDeleteSession.Dock = DockStyle.Right;
            this.btnDeleteSession.Cursor = Cursors.Hand;
            this.btnDeleteSession.Click += btnDeleteSession_Click;

            // flowPanelAssignments
            this.flowPanelAssignments.AutoSize = true;
            this.flowPanelAssignments.Dock = DockStyle.Top;
            this.flowPanelAssignments.FlowDirection = FlowDirection.TopDown;
            this.flowPanelAssignments.WrapContents = false;
            this.flowPanelAssignments.Padding = new Padding(10);

            // panelScrollableContent
            this.panelScrollableContent.Dock = DockStyle.Fill;
            this.panelScrollableContent.AutoScroll = true;
            this.panelScrollableContent.Controls.Add(this.flowPanelAssignments);

            // btnAttachFile
            this.btnAttachFile.Text = " Đính kèm tài liệu";
            this.btnAttachFile.IconChar = IconChar.Paperclip;
            this.btnAttachFile.IconColor = Color.White;
            this.btnAttachFile.IconFont = IconFont.Auto;
            this.btnAttachFile.IconSize = 20;
            this.btnAttachFile.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnAttachFile.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnAttachFile.TextAlign = ContentAlignment.MiddleLeft;
            this.btnAttachFile.BackColor = Color.FromArgb(60, 179, 113);
            this.btnAttachFile.ForeColor = Color.White;
            this.btnAttachFile.FlatStyle = FlatStyle.Flat;
            this.btnAttachFile.FlatAppearance.BorderSize = 0;
            this.btnAttachFile.Padding = new Padding(10, 5, 10, 5);
            this.btnAttachFile.AutoSize = true;
            this.btnAttachFile.Cursor = Cursors.Hand;

            // btnCreateAssignment
            this.btnCreateAssignment.Text = " Tạo bài tập";
            this.btnCreateAssignment.IconChar = IconChar.ClipboardList;
            this.btnCreateAssignment.IconColor = Color.White;
            this.btnCreateAssignment.IconFont = IconFont.Auto;
            this.btnCreateAssignment.IconSize = 20;
            this.btnCreateAssignment.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnCreateAssignment.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnCreateAssignment.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCreateAssignment.BackColor = Color.SteelBlue;
            this.btnCreateAssignment.ForeColor = Color.White;
            this.btnCreateAssignment.FlatStyle = FlatStyle.Flat;
            this.btnCreateAssignment.FlatAppearance.BorderSize = 0;
            this.btnCreateAssignment.Padding = new Padding(10, 5, 10, 5);
            this.btnCreateAssignment.AutoSize = true;
            this.btnCreateAssignment.Cursor = Cursors.Hand;

            // UcSessionItem
            this.Controls.Add(this.panelScrollableContent);
            this.Controls.Add(this.lblSessionTitle);
            this.Controls.Add(this.btnDeleteSession);
            this.Padding = new Padding(5);
            this.Height = 260;
            this.Width = 300; // 👈 thu chiều rộng lại để gọn hơn
            this.BackColor = Color.White;
            this.Margin = new Padding(10);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            this.Name = "UcSessionItem";
        }
    }
}