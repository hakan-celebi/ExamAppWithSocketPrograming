using ExamApp.Controllers;
using ExamApp.DataProviders;
using ExamApp.Dto;
using ExamApp.Entity;
using ExamApp.Models;
using ExamApp.TextboxControlLayer;
using ExamApp.Views.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp
{
    public partial class StudentForm : Form
    {
        private Form RoutedFrom { get; set; }
        private Form Inbox { get; set; } = new Form();
        private IEfDataProvider DataProvider { get; set; }
        private Exam ExamResult { get; set; } = new Exam();
        private int RemainingMinute { get; set; }
        public StudentForm(IEfDataProvider dataProvider)
        {
            InitializeComponent();
            DataProvider = dataProvider;
            Client.Context = this;
            CheckForIllegalCrossThreadCalls = false;
        }
        private void StudentForm_Load(object sender, EventArgs e)
        {
            timerExamTime.Start();
            RoutedFrom = this.Tag as Form;
            this.Tag = null;
            List<object> academicians = DataProvider.FindUsersByRole(UserRole.RoleEnum.academician);
            ListViewItem item;
            foreach (User academician in academicians)
            {
                item = new ListViewItem(academician.EMail);
                item.SubItems.Add(academician.IP);
                item.SubItems.Add("null");
                item.UseItemStyleForSubItems = false;
                item.SubItems[2].ForeColor = Color.Red;
                listViewSessions.Items.Add(item);
            }
            Client.RefreshListView(listViewSessions);
        }

        private void btnConnectDisconnectSession_Click(object sender, EventArgs e)
        {
            if (Client.IsConnected)
            {
                Client.Disconnect();
                lblSessionInformation.Text = "Disconnecting to Session...";
                return;
            }
            if (checkBoxManuel.Checked)
            {
                if (!txtIPAddress.IsEmpty(errorProvider) && !Client.Connect(txtIPAddress.Text))
                    return;
                lblSessionInformation.Text = "Connecting to Session...";
                lblSessionInformation.ForeColor = Color.Orange;
                return;
            }
            if (listViewSessions.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Can't Find IP Address!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string ip = listViewSessions.SelectedItems[0].SubItems[1].Text;
            if (!Client.Connect(ip))
                return;
            lblSessionInformation.Text = "Connecting to Session...";
            lblSessionInformation.ForeColor = Color.Orange;
        }
        private void btnEnterTheExam_Click(object sender, EventArgs e)
        {
            ExamResult = new Exam();
            if (!Client.IsConnected || Client.IsEnteredExam)
                return;
            btnEnterTheExam.ForeColor = Color.Orange;
            Client.EnterExam();
        }
        private void btnRefreshSession_Click(object sender, EventArgs e)
        {
            Client.RefreshListView(listViewSessions);
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            if (Client.IsReady)
                return;
            btnReady.ForeColor = Color.Green;
            btnReady.Enabled = false;
            Client.ReadyExam();
        }
        private void btnSendExamResultDocument_Click(object sender, EventArgs e)
        {
            if (fileDialogExam.ShowDialog() != DialogResult.OK)
                return;
            string path = fileDialogExam.FileName;
            if (path is null)
                return;
            ExamResult.AcademicianNote = null;
            ExamResult.ExamDocument = path.FileSerializer();
            ExamResult.ExamTime = 0;
            ExamResult.FileExtension = Path.GetExtension(path);
            ExamResult.LessonName = Client.Exam.LessonName;
            Client.FinishExam(ExamResult);
        }
        private void btnReplyByTyping_Click(object sender, EventArgs e)
        {
            ReplyByTypingForm frm = new ReplyByTypingForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            string result = frm.Tag as string;
            ExamResult.AcademicianNote = result;
            ExamResult.ExamDocument = default;
            ExamResult.ExamTime = 0;
            ExamResult.FileExtension = null;
            ExamResult.LessonName = Client.Exam.LessonName;
            Client.FinishExam(ExamResult);
        }
        private void btnOpenExamDocument_Click(object sender, EventArgs e)
        {
            Process.Start(Client.ExamDocumentFile);
        }

        private void btnReadExamNote_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Client.ExamNote, "From -> " + Client.Academician.EMail, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAskQuestionToAcademician_Click(object sender, EventArgs e)
        {
            ReplyByTypingForm frm = new ReplyByTypingForm();
            frm.Text = "Ask Question To -> " + Client.Academician.EMail;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            Client.AskQuestion(frm.Tag as string);
        }
        private void checkBoxManuel_CheckedChanged(object sender, EventArgs e)
        {
            txtIPAddress.Enabled = !txtIPAddress.Enabled;
        }
        private void timerExamTime_Tick(object sender, EventArgs e)
        {
            if (RemainingMinute == 0)
                return;
            RemainingMinute--;
            lblRemainingTime.Text = RemainingMinute.ToString();
        }
        private void StudentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Client.IsConnected)
                return;
            Thread.Sleep(1000);
            Client.Disconnect();
        }
        public void Session_Connected()
        {
            lblSessionInformation.Text = "Connected to Session";
            lblSessionInformation.ForeColor = Color.Green;
            btnConnectDisconnectSession.Text = "Disconnect Session";
            btnConnectDisconnectSession.ForeColor = Color.Red;
            btnEnterTheExam.Enabled = btnInbox.Enabled = true;
            checkBoxManuel.Enabled = txtIPAddress.Enabled = false;
        }
        public void Session_Disconnected()
        {
            lblSessionInformation.Text = "Not Connected";
            lblSessionInformation.ForeColor = Color.Red;
            lblExamStateInformation.Text = "Exam is Not Started";
            lblExamStateInformation.ForeColor = Color.Red;
            btnConnectDisconnectSession.Text = "Connect Session";
            btnConnectDisconnectSession.ForeColor = Color.Green;
            btnEnterTheExam.Enabled = btnReady.Enabled = btnOpenExamDocument.Enabled = btnReadExamNote.Enabled = btnInbox.Enabled = false;
            btnSendExamResultDocument.Enabled = btnReplyByTyping.Enabled = btnAskQuestionToAcademician.Enabled = false;
            btnReady.ForeColor = btnEnterTheExam.ForeColor = Color.Red;
            lblRemainingTime.Text = "0";
        }
        public void Exam_Entered()
        {
            btnEnterTheExam.ForeColor = Color.Green;
            btnEnterTheExam.Enabled = false;
            btnReady.Enabled = true;
        }
        public void Exam_Submited()
        {
            btnEnterTheExam.Enabled = btnReady.Enabled = btnOpenExamDocument.Enabled = btnReadExamNote.Enabled = btnSendExamResultDocument.Enabled = false;
            btnReplyByTyping.Enabled = btnAskQuestionToAcademician.Enabled = false;
            btnEnterTheExam.ForeColor = Color.Red;
        }
        public void Exam_Started(string examTime)
        {
            RemainingMinute = int.Parse(examTime);
            lblRemainingTime.Text = RemainingMinute.ToString();
            lblExamStateInformation.Text = "Exam is Started!";
            lblExamStateInformation.ForeColor = Color.Green;
            btnAskQuestionToAcademician.Enabled = btnOpenExamDocument.Enabled = btnReadExamNote.Enabled = btnSendExamResultDocument.Enabled = btnReplyByTyping.Enabled = true;
        }

        public void Exam_Finished()
        {
            RemainingMinute = 0;
            lblRemainingTime.Text = RemainingMinute.ToString();
            lblExamStateInformation.Text = "Exam is Stoped!";
            lblExamStateInformation.ForeColor = Color.Red;
            btnAskQuestionToAcademician.Enabled = btnOpenExamDocument.Enabled = btnReadExamNote.Enabled = btnSendExamResultDocument.Enabled = btnReplyByTyping.Enabled = false;
        }
        public void AcademicianNote_Received(Models.Message response)
        {
            lblHaveMessage.Visible = true;
        }

        private void btnInbox_Click(object sender, EventArgs e)
        {
            RemainingMinute = 60;
        }
    }
}
