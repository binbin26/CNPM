namespace CNPM.Forms.Student
{
    partial class ucDanhSach
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtGDanhSach = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtGDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(281, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh sách khóa học của bạn";
            // 
            // dtGDanhSach
            // 
            this.dtGDanhSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGDanhSach.Location = new System.Drawing.Point(0, 80);
            this.dtGDanhSach.Name = "dtGDanhSach";
            this.dtGDanhSach.RowHeadersWidth = 51;
            this.dtGDanhSach.RowTemplate.Height = 24;
            this.dtGDanhSach.Size = new System.Drawing.Size(925, 537);
            this.dtGDanhSach.TabIndex = 1;
            // 
            // ucDanhSach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtGDanhSach);
            this.Controls.Add(this.label1);
            this.Name = "ucDanhSach";
            this.Size = new System.Drawing.Size(925, 587);
            ((System.ComponentModel.ISupportInitialize)(this.dtGDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtGDanhSach;
    }
}
