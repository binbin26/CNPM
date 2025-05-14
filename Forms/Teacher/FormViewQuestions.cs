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
    public partial class FormViewQuestions : Form
    {
        public FormViewQuestions(List<Question> questions)
        {
            InitializeComponent();
            LoadQuestions(questions);
        }

        private void LoadQuestions(List<Question> questions)
        {
            flowLayoutPanel1.Controls.Clear();
            int index = 1;
            foreach (var q in questions)
            {
                var lbl = new Label
                {
                    AutoSize = true,
                    Text = $"Câu {index++}: {q.Content}\n" +
                           $"A. {q.OptionA}\n" +
                           $"B. {q.OptionB}\n" +
                           $"C. {q.OptionC}\n" +
                           $"D. {q.OptionD}\n" +
                           $"Đáp án đúng: {q.CorrectAnswer}",
                    Margin = new Padding(10),
                };
                flowLayoutPanel1.Controls.Add(lbl);
            }
        }
    }
}
