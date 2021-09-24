
namespace ExamApp
{
    partial class AcademicianForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AcademicianForm));
            this.lblListViewExamResult = new System.Windows.Forms.Label();
            this.listViewExamResults = new System.Windows.Forms.ListView();
            this.columnStudentMail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnExamResultDocument = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblExamSessionInfo = new System.Windows.Forms.Label();
            this.btnExamSession = new System.Windows.Forms.Button();
            this.lblExamInfo = new System.Windows.Forms.Label();
            this.fileDialogExam = new System.Windows.Forms.OpenFileDialog();
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.txtExamText = new System.Windows.Forms.TextBox();
            this.numExamTime = new System.Windows.Forms.NumericUpDown();
            this.lblExamTime = new System.Windows.Forms.Label();
            this.cbStudentsCanJoin = new System.Windows.Forms.CheckBox();
            this.btnExam = new System.Windows.Forms.Button();
            this.listViewStudents = new System.Windows.Forms.ListView();
            this.columnStudentEMail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStudentIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConnectionState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnExamState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblListViewStudent = new System.Windows.Forms.Label();
            this.lblSelectedStudent = new System.Windows.Forms.Label();
            this.btnSentMessage = new System.Windows.Forms.Button();
            this.btnQuestions = new System.Windows.Forms.Button();
            this.txtLessonName = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numExamTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblListViewExamResult
            // 
            resources.ApplyResources(this.lblListViewExamResult, "lblListViewExamResult");
            this.lblListViewExamResult.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblListViewExamResult.Name = "lblListViewExamResult";
            // 
            // listViewExamResults
            // 
            this.listViewExamResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnStudentMail,
            this.columnExamResultDocument,
            this.columnIP});
            resources.ApplyResources(this.listViewExamResults, "listViewExamResults");
            this.listViewExamResults.HideSelection = false;
            this.listViewExamResults.Name = "listViewExamResults";
            this.listViewExamResults.UseCompatibleStateImageBehavior = false;
            this.listViewExamResults.View = System.Windows.Forms.View.Details;
            // 
            // columnStudentMail
            // 
            resources.ApplyResources(this.columnStudentMail, "columnStudentMail");
            // 
            // columnExamResultDocument
            // 
            resources.ApplyResources(this.columnExamResultDocument, "columnExamResultDocument");
            // 
            // columnIP
            // 
            resources.ApplyResources(this.columnIP, "columnIP");
            // 
            // lblExamSessionInfo
            // 
            resources.ApplyResources(this.lblExamSessionInfo, "lblExamSessionInfo");
            this.lblExamSessionInfo.ForeColor = System.Drawing.Color.Red;
            this.lblExamSessionInfo.Name = "lblExamSessionInfo";
            // 
            // btnExamSession
            // 
            resources.ApplyResources(this.btnExamSession, "btnExamSession");
            this.btnExamSession.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnExamSession.Name = "btnExamSession";
            this.btnExamSession.UseVisualStyleBackColor = true;
            this.btnExamSession.Click += new System.EventHandler(this.btnStartSession_Click);
            // 
            // lblExamInfo
            // 
            resources.ApplyResources(this.lblExamInfo, "lblExamInfo");
            this.lblExamInfo.ForeColor = System.Drawing.Color.Red;
            this.lblExamInfo.Name = "lblExamInfo";
            // 
            // fileDialogExam
            // 
            this.fileDialogExam.FileName = "openFileDialog1";
            // 
            // btnUploadFile
            // 
            resources.ApplyResources(this.btnUploadFile, "btnUploadFile");
            this.btnUploadFile.ForeColor = System.Drawing.Color.Gray;
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.UseVisualStyleBackColor = true;
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click);
            // 
            // txtExamText
            // 
            resources.ApplyResources(this.txtExamText, "txtExamText");
            this.txtExamText.Name = "txtExamText";
            // 
            // numExamTime
            // 
            resources.ApplyResources(this.numExamTime, "numExamTime");
            this.numExamTime.ForeColor = System.Drawing.Color.Tomato;
            this.numExamTime.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numExamTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numExamTime.Name = "numExamTime";
            this.numExamTime.ReadOnly = true;
            this.numExamTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblExamTime
            // 
            resources.ApplyResources(this.lblExamTime, "lblExamTime");
            this.lblExamTime.Name = "lblExamTime";
            // 
            // cbStudentsCanJoin
            // 
            resources.ApplyResources(this.cbStudentsCanJoin, "cbStudentsCanJoin");
            this.cbStudentsCanJoin.Name = "cbStudentsCanJoin";
            this.cbStudentsCanJoin.UseVisualStyleBackColor = true;
            // 
            // btnExam
            // 
            resources.ApplyResources(this.btnExam, "btnExam");
            this.btnExam.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnExam.Name = "btnExam";
            this.btnExam.UseVisualStyleBackColor = true;
            this.btnExam.Click += new System.EventHandler(this.btnExam_Click);
            // 
            // listViewStudents
            // 
            this.listViewStudents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnStudentEMail,
            this.columnStudentIP,
            this.columnConnectionState,
            this.columnExamState});
            resources.ApplyResources(this.listViewStudents, "listViewStudents");
            this.listViewStudents.FullRowSelect = true;
            this.listViewStudents.HideSelection = false;
            this.listViewStudents.Name = "listViewStudents";
            this.listViewStudents.UseCompatibleStateImageBehavior = false;
            this.listViewStudents.View = System.Windows.Forms.View.Details;
            // 
            // columnStudentEMail
            // 
            resources.ApplyResources(this.columnStudentEMail, "columnStudentEMail");
            // 
            // columnStudentIP
            // 
            resources.ApplyResources(this.columnStudentIP, "columnStudentIP");
            // 
            // columnConnectionState
            // 
            resources.ApplyResources(this.columnConnectionState, "columnConnectionState");
            // 
            // columnExamState
            // 
            resources.ApplyResources(this.columnExamState, "columnExamState");
            // 
            // lblListViewStudent
            // 
            resources.ApplyResources(this.lblListViewStudent, "lblListViewStudent");
            this.lblListViewStudent.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblListViewStudent.Name = "lblListViewStudent";
            // 
            // lblSelectedStudent
            // 
            resources.ApplyResources(this.lblSelectedStudent, "lblSelectedStudent");
            this.lblSelectedStudent.ForeColor = System.Drawing.Color.Purple;
            this.lblSelectedStudent.Name = "lblSelectedStudent";
            // 
            // btnSentMessage
            // 
            resources.ApplyResources(this.btnSentMessage, "btnSentMessage");
            this.btnSentMessage.ForeColor = System.Drawing.Color.DarkMagenta;
            this.btnSentMessage.Name = "btnSentMessage";
            this.btnSentMessage.UseVisualStyleBackColor = true;
            // 
            // btnQuestions
            // 
            resources.ApplyResources(this.btnQuestions, "btnQuestions");
            this.btnQuestions.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnQuestions.Name = "btnQuestions";
            this.btnQuestions.UseVisualStyleBackColor = true;
            // 
            // txtLessonName
            // 
            resources.ApplyResources(this.txtLessonName, "txtLessonName");
            this.txtLessonName.Name = "txtLessonName";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // AcademicianForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cbStudentsCanJoin);
            this.Controls.Add(this.numExamTime);
            this.Controls.Add(this.txtLessonName);
            this.Controls.Add(this.txtExamText);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.btnExam);
            this.Controls.Add(this.btnSentMessage);
            this.Controls.Add(this.btnQuestions);
            this.Controls.Add(this.btnExamSession);
            this.Controls.Add(this.listViewStudents);
            this.Controls.Add(this.listViewExamResults);
            this.Controls.Add(this.lblExamInfo);
            this.Controls.Add(this.lblExamSessionInfo);
            this.Controls.Add(this.lblExamTime);
            this.Controls.Add(this.lblSelectedStudent);
            this.Controls.Add(this.lblListViewStudent);
            this.Controls.Add(this.lblListViewExamResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AcademicianForm";
            this.Load += new System.EventHandler(this.AcademicianForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numExamTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblListViewExamResult;
        private System.Windows.Forms.ColumnHeader columnStudentMail;
        private System.Windows.Forms.ColumnHeader columnExamResultDocument;
        private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.Button btnExamSession;
        private System.Windows.Forms.OpenFileDialog fileDialogExam;
        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.TextBox txtExamText;
        private System.Windows.Forms.NumericUpDown numExamTime;
        private System.Windows.Forms.Label lblExamTime;
        private System.Windows.Forms.CheckBox cbStudentsCanJoin;
        private System.Windows.Forms.Button btnExam;
        private System.Windows.Forms.ColumnHeader columnStudentEMail;
        private System.Windows.Forms.ColumnHeader columnStudentIP;
        private System.Windows.Forms.Label lblListViewStudent;
        private System.Windows.Forms.Label lblSelectedStudent;
        private System.Windows.Forms.Button btnSentMessage;
        private System.Windows.Forms.ColumnHeader columnConnectionState;
        private System.Windows.Forms.ColumnHeader columnExamState;
        private System.Windows.Forms.Button btnQuestions;
        private System.Windows.Forms.TextBox txtLessonName;
        private System.Windows.Forms.Label lblExamSessionInfo;
        private System.Windows.Forms.Label lblExamInfo;
        private System.Windows.Forms.ListView listViewStudents;
        private System.Windows.Forms.ListView listViewExamResults;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}