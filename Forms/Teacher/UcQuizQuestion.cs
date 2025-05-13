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
    public partial class UcQuizQuestion : UserControl
    {
        public UcQuizQuestion(int index)
        {
            InitializeComponent();
            lblTitle.Text = $"Câu hỏi {index}";
        }

        public string Question => txtQuestion.Text;
        public string AnswerA => txtA.Text;
        public string AnswerB => txtB.Text;
        public string AnswerC => txtC.Text;
        public string AnswerD => txtD.Text;
        public string CorrectAnswer => txtCorrectAnswer.Text;
    }
}
