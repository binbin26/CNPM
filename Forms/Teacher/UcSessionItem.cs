using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class UcSessionItem : UserControl
    {
        public string SessionTitle { get; set; }

        public UcSessionItem(string title)
        {
            InitializeComponent();
            SessionTitle = title;
            lblSessionTitle.Text = title;
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ListBoxItem item = new ListBoxItem(dialog.SafeFileName, dialog.FileName);
                listBoxFiles.Items.Add(item);
            }
        }
        private void listBoxAssignments_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                // Nếu là bài trắc nghiệm
                if (item.Tag is List<Question> questions)
                {
                    var form = new FormViewQuestions(questions);
                    form.ShowDialog();
                }
                // Nếu là bài tự luận
                else if (item.Tag is string filepath && File.Exists(filepath))
                {
                    var confirm = MessageBox.Show("Bạn có muốn mở đề bài?", "Xem file", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filepath); // Mở bằng app mặc định
                    }
                }
            }
        }

        private void OnRenameAssignment(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                FormRenameAssignment frm = new FormRenameAssignment(item.Display);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    item.Display = frm.NewName.Trim();
                    int index = listBoxAssignments.SelectedIndex;
                    listBoxAssignments.Items[index] = item; // cập nhật lại để hiển thị
                }
            }
        }
        private void OnDeleteAssignment(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn xóa bài tập này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    listBoxAssignments.Items.Remove(item);
                }
            }
        }

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            FormChooseAssignmentType chooseForm = new FormChooseAssignmentType();
            if (chooseForm.ShowDialog() == DialogResult.OK)
            {
                string assignmentTitle = "Bài tập " + (listBoxAssignments.Items.Count + 1);

                if (chooseForm.SelectedType == FormChooseAssignmentType.AssignmentType.MultipleChoice)
                {
                    FormSetupQuiz setup = new FormSetupQuiz();
                    if (setup.ShowDialog() == DialogResult.OK)
                    {
                        FormCreateQuizQuestions createQuiz = new FormCreateQuizQuestions(setup.QuestionCount);
                        if (createQuiz.ShowDialog() == DialogResult.OK)
                        {
                            List<Question> questions = createQuiz.CreatedQuestions;
                            var item = new ListBoxItem($"{assignmentTitle} (Trắc nghiệm - {setup.Duration} phút)", "");
                            item.Tag = questions; // 👈 Gắn danh sách câu hỏi vào đây
                            listBoxAssignments.Items.Add(item);
                        }
                    }
                }
                else if (chooseForm.SelectedType == FormChooseAssignmentType.AssignmentType.Essay)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string filename = Path.GetFileName(dialog.FileName);
                        var item = new ListBoxItem($"{assignmentTitle} (Tự luận)", filename);
                        item.Tag = dialog.FileName; // 👈 Gắn đường dẫn thật
                        listBoxAssignments.Items.Add(item);
                    }
                }

                // Đẩy các nút xuống cuối cùng
                Controls.SetChildIndex(listBoxAssignments, 0);
                Controls.SetChildIndex(btnCreateAssignment, Controls.Count - 1);
                Controls.SetChildIndex(btnAttachFile, Controls.Count - 1);
            }
        }
    }

    public class ListBoxItem
    {
        public string Display { get; set; }
        public string FilePath { get; set; }
        public object Tag { get; set; } // ✨ Cho phép gắn dữ liệu bất kỳ, như List<Question>

        public ListBoxItem(string display, string path)
        {
            Display = display;
            FilePath = path;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
