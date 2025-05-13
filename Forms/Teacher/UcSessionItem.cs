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

        /*private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            FormCreateAssignment form = new FormCreateAssignment();
            if (form.ShowDialog() == DialogResult.OK)
            {
                listBoxAssignments.Items.Add(form.AssignmentTitle);
            }
        }*/
    }

    public class ListBoxItem
    {
        public string Display { get; set; }
        public string FilePath { get; set; }

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
