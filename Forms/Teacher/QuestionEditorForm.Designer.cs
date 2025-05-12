using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class QuestionEditorForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtQuestion;
        private TextBox txtAnswer;
        private Button btnSave;
        private void InitializeComponent()
        {
            this.txtQuestion = new TextBox();
            this.txtAnswer = new TextBox();
            this.btnSave = new Button();

            this.SuspendLayout();

            this.txtQuestion.Location = new System.Drawing.Point(20, 20);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Size = new System.Drawing.Size(400, 60);

            this.txtAnswer.Location = new System.Drawing.Point(20, 100);
            this.txtAnswer.Size = new System.Drawing.Size(400, 25);

            this.btnSave.Location = new System.Drawing.Point(20, 140);
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(450, 200);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.btnSave);
            this.Text = "Trình soạn thảo câu hỏi";

            this.ResumeLayout(false);
        }
    }
}