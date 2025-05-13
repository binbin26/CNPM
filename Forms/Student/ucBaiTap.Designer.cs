using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    partial class ucBaiTap
    {
        private void InitializeCustomComponents()
        {
            // Panel chính
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            this.Controls.Add(mainPanel);

            // DataGridView hiển thị danh sách bài tập
            dgvAssignments = new DataGridView();
            dgvAssignments.Dock = DockStyle.Fill;
            dgvAssignments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAssignments.MultiSelect = false;
            dgvAssignments.ReadOnly = true;
            dgvAssignments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAssignments.CellDoubleClick += DgvAssignments_CellDoubleClick;
            mainPanel.Controls.Add(dgvAssignments);
        }
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ucBaiTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucBaiTap";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
