using ExamApp.Controllers;
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

namespace ExamApp.Views.Inbox
{
    public partial class Inbox : Form
    {
        public Inbox()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = listViewMessages.SelectedItems[0];
            int index = listViewMessages.Items.IndexOf(item);
            Models.Message temp = Client.AcademicianNotes[index];
            Client.AcademicianNotes.RemoveAt(index);
            Client.ReadedMessages.Add(temp);
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            if(listViewMessages.SelectedItems.Count > 0)
            {
                if (txtAnswer.IsEmpty(errorProvider))
                    return;
                Client.AskQuestion(txtAnswer.Text);
            }
        }

        private void Inbox_Load(object sender, EventArgs e)
        {
            ListViewItem item;
            foreach (var message in Client.AcademicianNotes)
            {
                item = new ListViewItem(message.SenderUser.EMail);
                item.SubItems.Add(Encoding.ASCII.GetString(message.Data));
            }
            foreach (var message in Client.ReadedMessages)
            {
                item = new ListViewItem(message.SenderUser.EMail);
                item.ForeColor = Color.Gray;
                item.SubItems.Add(Encoding.ASCII.GetString(message.Data));
            }
        }
    }
}
