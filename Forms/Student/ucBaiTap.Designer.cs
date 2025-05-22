using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    partial class ucBaiTap
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtGAssign = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGAssign)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGAssign
            // 
            this.dtGAssign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGAssign.Location = new System.Drawing.Point(0, 97);
            this.dtGAssign.Name = "dtGAssign";
            this.dtGAssign.RowHeadersWidth = 51;
            this.dtGAssign.RowTemplate.Height = 24;
            this.dtGAssign.Size = new System.Drawing.Size(792, 424);
            this.dtGAssign.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(264, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bài tập của Sinh viên";
            // 
            // ucBaiTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGAssign);
            this.Name = "ucBaiTap";
            this.Size = new System.Drawing.Size(792, 521);
            ((System.ComponentModel.ISupportInitialize)(this.dtGAssign)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AddButtonColumn()
        {
            if (!dtGAssign.Columns.Contains("btnLamBaiTap"))
            {
                DataGridViewButtonColumn btnLamBaiTap = new DataGridViewButtonColumn();
                btnLamBaiTap.HeaderText = "Thao tác";
                btnLamBaiTap.Text = "Làm bài tập";
                btnLamBaiTap.UseColumnTextForButtonValue = true;
                btnLamBaiTap.Name = "btnLamBaiTap";
                dtGAssign.Columns.Add(btnLamBaiTap);
            }

            dtGAssign.CellClick += dtGAssign_CellClick;
        }
        #endregion

        private System.Windows.Forms.DataGridView dtGAssign;
        private System.Windows.Forms.Label label1;
    }
}
