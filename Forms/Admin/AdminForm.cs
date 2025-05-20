using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using CNPM.DAL;
using CNPM.BLL;
using CNPM.Models.Users;
using CNPM.Models.Courses;
using CNPM.Forms.Shared;

namespace CNPM.Forms.Admin
{
    public partial class AdminForm : Form
    {
        // Các thành phần được thiết kế tự động
        private System.ComponentModel.IContainer components = null;

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
            this.SuspendLayout();
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AdminForm";
            this.Text = "FormAdmin";
            this.ResumeLayout(false);
        }
    }
}

