using System.Windows.Forms;

namespace CNPM.Forms.Teacher.Usercontrol
{
    partial class UcSubmissions
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSelectAssignment;
        private System.Windows.Forms.ComboBox cboAssignments;
        private System.Windows.Forms.DataGridView dgvSubmissions;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSelectAssignment = new System.Windows.Forms.Label();
            this.cboAssignments = new System.Windows.Forms.ComboBox();
            this.dgvSubmissions = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuiz = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.nudScore = new System.Windows.Forms.NumericUpDown();
            this.btnSubmitScore = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectAssignment
            // 
            this.lblSelectAssignment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSelectAssignment.Location = new System.Drawing.Point(20, 10);
            this.lblSelectAssignment.Name = "lblSelectAssignment";
            this.lblSelectAssignment.Size = new System.Drawing.Size(122, 24);
            this.lblSelectAssignment.TabIndex = 0;
            this.lblSelectAssignment.Text = "Chọn bài tập:";
            // 
            // cboAssignments
            // 
            this.cboAssignments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAssignments.Location = new System.Drawing.Point(130, 12);
            this.cboAssignments.Name = "cboAssignments";
            this.cboAssignments.Size = new System.Drawing.Size(250, 24);
            this.cboAssignments.TabIndex = 1;
            this.cboAssignments.SelectedIndexChanged += new System.EventHandler(this.cboAssignments_SelectedIndexChanged);
            // 
            // dgvSubmissions
            // 
            this.dgvSubmissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubmissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSubmissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubmissions.Location = new System.Drawing.Point(20, 50);
            this.dgvSubmissions.Name = "dgvSubmissions";
            this.dgvSubmissions.ReadOnly = true;
            this.dgvSubmissions.RowHeadersWidth = 51;
            this.dgvSubmissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubmissions.Size = new System.Drawing.Size(1137, 596);
            this.dgvSubmissions.TabIndex = 0;
            this.dgvSubmissions.SelectionChanged += new System.EventHandler(this.dgvSubmissions_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(453, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Danh sách bài tập tự luận";
            // 
            // btnQuiz
            // 
            this.btnQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnQuiz.Location = new System.Drawing.Point(921, 664);
            this.btnQuiz.Name = "btnQuiz";
            this.btnQuiz.Size = new System.Drawing.Size(236, 27);
            this.btnQuiz.TabIndex = 12;
            this.btnQuiz.Text = "Danh sách bài trắc nghiệm";
            this.btnQuiz.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(20, 664);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 29);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Tải lại";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(130, 664);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(100, 29);
            this.btnOpenFile.TabIndex = 9;
            this.btnOpenFile.Text = "Xem bài nộp";
            // 
            // nudScore
            // 
            this.nudScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.nudScore.Location = new System.Drawing.Point(387, 667);
            this.nudScore.Name = "nudScore";
            this.nudScore.Size = new System.Drawing.Size(60, 26);
            this.nudScore.TabIndex = 10;
            // 
            // btnSubmitScore
            // 
            this.btnSubmitScore.Location = new System.Drawing.Point(262, 664);
            this.btnSubmitScore.Name = "btnSubmitScore";
            this.btnSubmitScore.Size = new System.Drawing.Size(100, 29);
            this.btnSubmitScore.TabIndex = 11;
            this.btnSubmitScore.Text = "Chấm điểm";
            // 
            // UcSubmissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnQuiz);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.nudScore);
            this.Controls.Add(this.btnSubmitScore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSelectAssignment);
            this.Controls.Add(this.cboAssignments);
            this.Controls.Add(this.dgvSubmissions);
            this.Name = "UcSubmissions";
            this.Size = new System.Drawing.Size(1187, 706);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubmissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button btnQuiz;
        private Button btnRefresh;
        private Button btnOpenFile;
        private NumericUpDown nudScore;
        private Button btnSubmitScore;
    }
}
