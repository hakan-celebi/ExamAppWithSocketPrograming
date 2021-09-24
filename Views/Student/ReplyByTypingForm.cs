using ExamApp.TextboxControlLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Views.Student
{
    public partial class ReplyByTypingForm : Form
    {
        public ReplyByTypingForm()
        {
            InitializeComponent();
        }

        private void ReplyByTypingForm_Load(object sender, EventArgs e)
        {
            txtExamResult.Size = new Size(this.Width - (txtExamResult.Location.X * 2) - (btnSend.Width + 15), this.Height - txtExamResult.Location.Y - 50);
            btnSend.Location = new Point(txtExamResult.Location.X + txtExamResult.Width + 3, txtExamResult.Location.Y);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtExamResult.IsEmpty(errorProvider))
                return;
            this.DialogResult = DialogResult.OK;
            this.Tag = txtExamResult.Text;
            this.Close();
        }
    }
}
