using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class QuestionEditorForm : Form
    {
        public QuestionEditorForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetPlaceholder(txtQuestion, "Nhập câu hỏi");
            SetPlaceholder(txtAnswer, "Đáp án đúng");

            txtQuestion.Enter += RemovePlaceholder;
            txtQuestion.Leave += AddPlaceholder;

            txtAnswer.Enter += RemovePlaceholder;
            txtAnswer.Leave += AddPlaceholder;
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Tag = placeholder; // lưu placeholder trong Tag
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == (string)textBox.Tag)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = (string)textBox.Tag;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text;
            string answer = txtAnswer.Text;
            // Save to DB or file
            MessageBox.Show("Đã lưu câu hỏi và đáp án.");
            this.Close();
        }
    }
}
