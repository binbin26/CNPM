namespace CNPM.Forms.Student
{
    partial class ucDiemTrungBinh
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
            this.dtGPoint = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGPoint
            // 
            this.dtGPoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGPoint.Location = new System.Drawing.Point(4, 100);
            this.dtGPoint.Name = "dtGPoint";
            this.dtGPoint.RowHeadersWidth = 51;
            this.dtGPoint.RowTemplate.Height = 24;
            this.dtGPoint.Size = new System.Drawing.Size(827, 496);
            this.dtGPoint.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(323, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kết quả học tập";
            // 
            // ucDiemTrungBinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGPoint);
            this.Name = "ucDiemTrungBinh";
            this.Size = new System.Drawing.Size(834, 596);
            ((System.ComponentModel.ISupportInitialize)(this.dtGPoint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGPoint;
        private System.Windows.Forms.Label label1;
    }
}
