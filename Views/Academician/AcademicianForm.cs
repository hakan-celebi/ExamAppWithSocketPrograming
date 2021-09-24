using ExamApp.Controllers;
using ExamApp.DataProviders;
using ExamApp.Dto;
using ExamApp.Entity;
using ExamApp.Managers;
using ExamApp.Models;
using ExamApp.TextboxControlLayer;
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

namespace ExamApp
{
    public partial class AcademicianForm : Form
    {
        private Form RoutedFrom { get; set; }
        private JsonDataProvider JsonDataProvider { get; set; } = new JsonDataProvider();
        private IEfDataProvider DataProvider { get; set; }
        private Exam Exam { get; set; } = new Exam();
        public AcademicianForm(IEfDataProvider dataProvider)
        {
            InitializeComponent();
            DataProvider = dataProvider;
            RoutedFrom = this.Tag as Form;
            this.Tag = null;
            txtLessonName.SetPlaceHolderText("Lesson");
            txtExamText.SetPlaceHolderText("Note");
            Server.AcademicianForm = this;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void AcademicianForm_Load(object sender, EventArgs e)
        {
            //ResponsiveSize();
            this.Text = SignInManager.LoggedUser.EMail;
            List<object> students = DataProvider.FindUsersByRole(UserRole.RoleEnum.student);
            ListViewItem item;
            foreach (var student in students)
            {
                item = new ListViewItem((student as User).EMail);
                item.SubItems.Add((student as User).IP);
                item.SubItems.Add("Not Connected");
                item.SubItems.Add("Not Started");
                item.ForeColor = Color.Red;
                listViewStudents.Items.Add(item);
            }
        }
        public void UpdateFormControlsWhenServerStarted()
        {
            lblExamSessionInfo.Text = "Exam Session Started!";
            lblExamSessionInfo.ForeColor = Color.Green;
            txtExamText.Enabled = txtLessonName.Enabled = btnExam.Enabled = btnUploadFile.Enabled = numExamTime.Enabled = cbStudentsCanJoin.Enabled = true;
            btnExamSession.Text = "Close";
            btnExamSession.ForeColor = Color.Red;
        }
        public void UpdateFormControlsWhenServerStoped()
        {
            lblExamSessionInfo.Text = "Exam Session Not Started!";
            lblExamSessionInfo.ForeColor = Color.Red;
            txtExamText.Enabled = txtLessonName.Enabled = btnExam.Enabled = btnUploadFile.Enabled = numExamTime.Enabled = cbStudentsCanJoin.Enabled = false;
            btnExamSession.Text = "Start";
            btnExamSession.ForeColor = Color.Green;
        }
        public void UpdateFormControlsWhenClientDisonnectted(object sender)
        {
            User student = sender as User;
            foreach (ListViewItem item in listViewStudents.Items)
            {
                if (item.Text.Equals(student.EMail))
                {
                    item.SubItems[1].Text = student.IP;
                    item.SubItems[2].Text = "Disconnected";
                    item.ForeColor = Color.Red;
                    break;
                }
            }
        }
        public void UpdateFormControlsWhenClientConnectted(object sender)
        {
            User student = sender as User;
            foreach (ListViewItem item in listViewStudents.Items)
            {
                if (item.Text.Equals(student.EMail))
                {
                    item.SubItems[1].Text = student.IP;
                    item.SubItems[2].Text = "Connected";
                    item.ForeColor = Color.Orange;
                    break;
                }
            }
        }
        public void UpdateFormControlsWhenExamStarted()
        {
            lblExamInfo.Text = "The Exam Has Started";
            lblExamInfo.ForeColor = Color.Green;
            txtExamText.Enabled = txtLessonName.Enabled = btnUploadFile.Enabled = numExamTime.Enabled = cbStudentsCanJoin.Enabled = false;
            btnExam.ForeColor = Color.Red;
            btnExam.Text = "Finish";
        }
        public void UpdateFormControlsWhenExamFinished()
        {
            lblExamInfo.Text = "The Exam Has Finished";
            lblExamInfo.ForeColor = Color.Red;
            txtExamText.Enabled = txtLessonName.Enabled = btnUploadFile.Enabled = numExamTime.Enabled = cbStudentsCanJoin.Enabled = true;
            btnExam.ForeColor = Color.Green;
            btnExam.Text = "Start";
            fileDialogExam.Reset();
            txtLessonName.SetPlaceHolderText("Lesson");
            txtExamText.SetPlaceHolderText("Note");
            numExamTime.Value = numExamTime.Minimum;
        }
        public void StudentEnteredToExam(object sender)
        {
            User student = sender as User;
            foreach (ListViewItem item in listViewStudents.Items)
            {
                if (item.SubItems[0].Text.Equals(student.EMail))
                {
                    item.SubItems[3].Text = "Not-Ready";
                    item.ForeColor = Color.DarkBlue;
                    break;
                }
            }
        }
        public void StudentIsReadyForExam(object sender)
        {
            User student = sender as User;
            foreach (ListViewItem item in listViewStudents.Items)
            {
                if (item.SubItems[0].Text.Equals(student.EMail))
                {
                    item.SubItems[3].Text = "Ready";
                    item.ForeColor = Color.Green;
                    break;
                }
            }
        }
        public void GetMessage(object sender)
        {
            Models.Message received = sender as Models.Message;
            MessageBox.Show($"Message received {received.MCode}, {Encoding.ASCII.GetString(received.Data)}");
        }
        public void SubmitExamResult(object sender)
        {

        }
        //private void ResponsiveSize()
        //{
        //    listViewExamResults.Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.40);
        //    listViewStudents.Width = (int)(Screen.PrimaryScreen.Bounds.Width * 0.32);
        //    listViewExamResults.Height = this.Height - 125;
        //    listViewStudents.Height = this.Height - 125;
        //    listViewStudents.Location = new Point(listViewExamResults.Width + 345, listViewStudents.Location.Y);
        //    lblListViewExamResult.Location = new Point(listViewExamResults.Location.X + (listViewExamResults.Width / 2) - (lblListViewExamResult.Width / 2), lblListViewExamResult.Location.Y);
        //    lblListViewStudent.Location = new Point(listViewStudents.Location.X + (listViewStudents.Width / 2) - (lblListViewStudent.Width / 2), lblListViewStudent.Location.Y);
        //    lblExamSessionInfo.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (lblExamSessionInfo.Width / 2), lblExamSessionInfo.Location.Y);
        //    lblExamInfo.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (lblExamInfo.Width / 2), lblExamInfo.Location.Y);
        //    btnStartExam.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (btnStartExam.Width / 2), btnStartExam.Location.Y);
        //    btnStartSession.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (btnStartSession.Width / 2), btnStartSession.Location.Y);
        //    txtExamText.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (txtExamText.Width / 2), txtExamText.Location.Y);
        //    cbStudentsCanJoin.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (cbStudentsCanJoin.Width / 2), cbStudentsCanJoin.Location.Y);
        //    btnUploadFile.Location = new Point(listViewExamResults.Location.X + listViewExamResults.Width + 15, btnUploadFile.Location.Y);
        //    lblExamTime.Location = new Point(btnUploadFile.Location.X + btnUploadFile.Width + 10, lblExamTime.Location.Y);
        //    numExamTime.Location = new Point(lblExamTime.Location.X + lblExamTime.Width, lblExamTime.Location.Y);
        //    lblSelectedStudent.Location = new Point(Screen.PrimaryScreen.Bounds.Width - lblSelectedStudent.Width - 15, lblSelectedStudent.Location.Y);
        //    btnInformation.Location = new Point(Screen.PrimaryScreen.Bounds.Width - btnInformation.Width - 15, btnInformation.Location.Y);
        //    btnSentMessage.Location = new Point(Screen.PrimaryScreen.Bounds.Width - btnSentMessage.Width - 15, btnSentMessage.Location.Y);
        //    btnMessageHistory.Location = new Point(Screen.PrimaryScreen.Bounds.Width - btnMessageHistory.Width - 15, btnSentMessage.Location.Y);
        //    btnAllStudents.Location = new Point(Screen.PrimaryScreen.Bounds.Width - btnAllStudents.Width - 15, listViewStudents.Location.Y + listViewStudents.Height - btnAllStudents.Height);
        //    btnQuestions.Location = new Point(Screen.PrimaryScreen.Bounds.Width - btnQuestions.Width - 15, listViewStudents.Location.Y + listViewStudents.Height - btnAllStudents.Height - btnQuestions.Height - 5);
        //    txtLessonName.Location = new Point((listViewExamResults.Location.X + listViewExamResults.Width + (330 / 2)) - (txtLessonName.Width / 2), txtLessonName.Location.Y);
        //}

        private void btnStartSession_Click(object sender, EventArgs e)
        {
            if (ExamSessionController.ExamSessionIsStarted)
                ExamSessionController.StopExamSession();
            else
                ExamSessionController.StartExamSession(SignInManager.LoggedUser);
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (fileDialogExam.ShowDialog() == DialogResult.OK)
            {
                string path = fileDialogExam.FileName;
                if (path is null)
                    return;
                Exam.FileExtension = Path.GetExtension(path);
                Exam.ExamDocument = path.FileSerializer();
            }
        }

        private void btnExam_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn is null)
                return;
            if (ExamSessionController.ExamIsStarted)
            {
                if (!ExamSessionController.ExamSessionIsStarted)
                    return;
                Exam = new Exam();
                ExamSessionController.FinishTheExam();
                return;
            }
            if (txtLessonName.IsEmpty(errorProvider))
                return;
            if (!ExamSessionController.ExamSessionIsStarted)
                return;
            Exam.LessonName = txtLessonName.Text;
            Exam.AcademicianNote = txtExamText.Text;
            Exam.ExamTime = numExamTime.Value;
            ExamSessionController.ExamIsLocked = !cbStudentsCanJoin.Checked;
            ExamSessionController.StartExam(Exam);
        }
    }
}
