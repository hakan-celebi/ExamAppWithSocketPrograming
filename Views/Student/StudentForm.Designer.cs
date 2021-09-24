
namespace ExamApp
{
    partial class StudentForm
    {
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
            this.fileDialogExam = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSendExamResultDocument = new System.Windows.Forms.Button();
            this.btnAskQuestionToAcademician = new System.Windows.Forms.Button();
            this.btnInbox = new System.Windows.Forms.Button();
            this.btnConnectDisconnectSession = new System.Windows.Forms.Button();
            this.listViewSessions = new System.Windows.Forms.ListView();
            this.columnAcademicianMail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAvailable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblSessionInformation = new System.Windows.Forms.Label();
            this.lblListViewExamResult = new System.Windows.Forms.Label();
            this.checkBoxManuel = new System.Windows.Forms.CheckBox();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefreshSession = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnScanForNewSessions = new System.Windows.Forms.Button();
            this.btnEnterTheExam = new System.Windows.Forms.Button();
            this.btnReady = new System.Windows.Forms.Button();
            this.btnOpenExamDocument = new System.Windows.Forms.Button();
            this.lblExamStateInformation = new System.Windows.Forms.Label();
            this.btnReadExamNote = new System.Windows.Forms.Button();
            this.btnReplyByTyping = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRemainingTime = new System.Windows.Forms.Label();
            this.timerExamTime = new System.Windows.Forms.Timer(this.components);
            this.lblHaveMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // fileDialogExam
            // 
            this.fileDialogExam.FileName = "openFileDialog1";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnSendExamResultDocument
            // 
            this.btnSendExamResultDocument.Enabled = false;
            this.btnSendExamResultDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendExamResultDocument.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnSendExamResultDocument.ForeColor = System.Drawing.Color.Gray;
            this.btnSendExamResultDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSendExamResultDocument.Location = new System.Drawing.Point(546, 449);
            this.btnSendExamResultDocument.Name = "btnSendExamResultDocument";
            this.btnSendExamResultDocument.Size = new System.Drawing.Size(319, 31);
            this.btnSendExamResultDocument.TabIndex = 19;
            this.btnSendExamResultDocument.Text = "Send Exam Result Document";
            this.btnSendExamResultDocument.UseVisualStyleBackColor = true;
            this.btnSendExamResultDocument.Click += new System.EventHandler(this.btnSendExamResultDocument_Click);
            // 
            // btnAskQuestionToAcademician
            // 
            this.btnAskQuestionToAcademician.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAskQuestionToAcademician.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnAskQuestionToAcademician.ForeColor = System.Drawing.Color.DarkMagenta;
            this.btnAskQuestionToAcademician.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAskQuestionToAcademician.Location = new System.Drawing.Point(642, 557);
            this.btnAskQuestionToAcademician.Name = "btnAskQuestionToAcademician";
            this.btnAskQuestionToAcademician.Size = new System.Drawing.Size(140, 42);
            this.btnAskQuestionToAcademician.TabIndex = 17;
            this.btnAskQuestionToAcademician.Text = "Ask Question";
            this.btnAskQuestionToAcademician.UseVisualStyleBackColor = true;
            this.btnAskQuestionToAcademician.Click += new System.EventHandler(this.btnAskQuestionToAcademician_Click);
            // 
            // btnInbox
            // 
            this.btnInbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInbox.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnInbox.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnInbox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInbox.Location = new System.Drawing.Point(899, 34);
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.Size = new System.Drawing.Size(140, 42);
            this.btnInbox.TabIndex = 16;
            this.btnInbox.Text = "Inbox";
            this.btnInbox.UseVisualStyleBackColor = true;
            this.btnInbox.Click += new System.EventHandler(this.btnInbox_Click);
            // 
            // btnConnectDisconnectSession
            // 
            this.btnConnectDisconnectSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectDisconnectSession.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnConnectDisconnectSession.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnConnectDisconnectSession.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnConnectDisconnectSession.Location = new System.Drawing.Point(603, 114);
            this.btnConnectDisconnectSession.Name = "btnConnectDisconnectSession";
            this.btnConnectDisconnectSession.Size = new System.Drawing.Size(204, 45);
            this.btnConnectDisconnectSession.TabIndex = 14;
            this.btnConnectDisconnectSession.Text = "Connect Session";
            this.btnConnectDisconnectSession.UseVisualStyleBackColor = true;
            this.btnConnectDisconnectSession.Click += new System.EventHandler(this.btnConnectDisconnectSession_Click);
            // 
            // listViewSessions
            // 
            this.listViewSessions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAcademicianMail,
            this.columnIP,
            this.columnAvailable});
            this.listViewSessions.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.listViewSessions.FullRowSelect = true;
            this.listViewSessions.HideSelection = false;
            this.listViewSessions.Location = new System.Drawing.Point(11, 34);
            this.listViewSessions.MultiSelect = false;
            this.listViewSessions.Name = "listViewSessions";
            this.listViewSessions.Size = new System.Drawing.Size(526, 582);
            this.listViewSessions.TabIndex = 12;
            this.listViewSessions.UseCompatibleStateImageBehavior = false;
            this.listViewSessions.View = System.Windows.Forms.View.Details;
            // 
            // columnAcademicianMail
            // 
            this.columnAcademicianMail.Text = "Academician E-Mail";
            this.columnAcademicianMail.Width = 250;
            // 
            // columnIP
            // 
            this.columnIP.Text = "IP";
            this.columnIP.Width = 150;
            // 
            // columnAvailable
            // 
            this.columnAvailable.Text = "Is Available";
            this.columnAvailable.Width = 120;
            // 
            // lblSessionInformation
            // 
            this.lblSessionInformation.AutoSize = true;
            this.lblSessionInformation.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblSessionInformation.ForeColor = System.Drawing.Color.Red;
            this.lblSessionInformation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSessionInformation.Location = new System.Drawing.Point(638, 44);
            this.lblSessionInformation.Name = "lblSessionInformation";
            this.lblSessionInformation.Size = new System.Drawing.Size(127, 22);
            this.lblSessionInformation.TabIndex = 9;
            this.lblSessionInformation.Text = "Not Connected";
            // 
            // lblListViewExamResult
            // 
            this.lblListViewExamResult.AutoSize = true;
            this.lblListViewExamResult.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblListViewExamResult.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblListViewExamResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblListViewExamResult.Location = new System.Drawing.Point(208, 9);
            this.lblListViewExamResult.Name = "lblListViewExamResult";
            this.lblListViewExamResult.Size = new System.Drawing.Size(128, 22);
            this.lblListViewExamResult.TabIndex = 6;
            this.lblListViewExamResult.Text = "Exam Sessions";
            // 
            // checkBoxManuel
            // 
            this.checkBoxManuel.AutoSize = true;
            this.checkBoxManuel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxManuel.Location = new System.Drawing.Point(543, 84);
            this.checkBoxManuel.Name = "checkBoxManuel";
            this.checkBoxManuel.Size = new System.Drawing.Size(136, 21);
            this.checkBoxManuel.TabIndex = 24;
            this.checkBoxManuel.Text = "Manuel Address:";
            this.checkBoxManuel.UseVisualStyleBackColor = true;
            this.checkBoxManuel.CheckedChanged += new System.EventHandler(this.checkBoxManuel_CheckedChanged);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Enabled = false;
            this.txtIPAddress.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtIPAddress.Location = new System.Drawing.Point(679, 78);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(183, 30);
            this.txtIPAddress.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(-354, -4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Refresh Sessions";
            // 
            // btnRefreshSession
            // 
            this.btnRefreshSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshSession.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnRefreshSession.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnRefreshSession.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRefreshSession.Location = new System.Drawing.Point(11, 622);
            this.btnRefreshSession.Name = "btnRefreshSession";
            this.btnRefreshSession.Size = new System.Drawing.Size(526, 41);
            this.btnRefreshSession.TabIndex = 18;
            this.btnRefreshSession.Text = "Refresh Sessions";
            this.btnRefreshSession.UseVisualStyleBackColor = true;
            this.btnRefreshSession.Click += new System.EventHandler(this.btnRefreshSession_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label2.ForeColor = System.Drawing.Color.DarkOrange;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(-354, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Scan for New Sessions";
            // 
            // btnScanForNewSessions
            // 
            this.btnScanForNewSessions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScanForNewSessions.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnScanForNewSessions.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnScanForNewSessions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnScanForNewSessions.Location = new System.Drawing.Point(11, 669);
            this.btnScanForNewSessions.Name = "btnScanForNewSessions";
            this.btnScanForNewSessions.Size = new System.Drawing.Size(526, 41);
            this.btnScanForNewSessions.TabIndex = 18;
            this.btnScanForNewSessions.Text = "Scan for New Sessions";
            this.btnScanForNewSessions.UseVisualStyleBackColor = true;
            // 
            // btnEnterTheExam
            // 
            this.btnEnterTheExam.Enabled = false;
            this.btnEnterTheExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnterTheExam.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnEnterTheExam.ForeColor = System.Drawing.Color.Red;
            this.btnEnterTheExam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEnterTheExam.Location = new System.Drawing.Point(619, 165);
            this.btnEnterTheExam.Name = "btnEnterTheExam";
            this.btnEnterTheExam.Size = new System.Drawing.Size(169, 45);
            this.btnEnterTheExam.TabIndex = 18;
            this.btnEnterTheExam.Text = "Enter The Exam";
            this.btnEnterTheExam.UseVisualStyleBackColor = true;
            this.btnEnterTheExam.Click += new System.EventHandler(this.btnEnterTheExam_Click);
            // 
            // btnReady
            // 
            this.btnReady.Enabled = false;
            this.btnReady.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReady.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnReady.ForeColor = System.Drawing.Color.Red;
            this.btnReady.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReady.Location = new System.Drawing.Point(619, 215);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(169, 45);
            this.btnReady.TabIndex = 18;
            this.btnReady.Text = "Ready";
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
            // 
            // btnOpenExamDocument
            // 
            this.btnOpenExamDocument.Enabled = false;
            this.btnOpenExamDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenExamDocument.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnOpenExamDocument.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnOpenExamDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenExamDocument.Location = new System.Drawing.Point(543, 315);
            this.btnOpenExamDocument.Name = "btnOpenExamDocument";
            this.btnOpenExamDocument.Size = new System.Drawing.Size(140, 65);
            this.btnOpenExamDocument.TabIndex = 18;
            this.btnOpenExamDocument.Text = "Open Exam Document";
            this.btnOpenExamDocument.UseVisualStyleBackColor = true;
            this.btnOpenExamDocument.Click += new System.EventHandler(this.btnOpenExamDocument_Click);
            // 
            // lblExamStateInformation
            // 
            this.lblExamStateInformation.AutoSize = true;
            this.lblExamStateInformation.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblExamStateInformation.ForeColor = System.Drawing.Color.Red;
            this.lblExamStateInformation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblExamStateInformation.Location = new System.Drawing.Point(620, 290);
            this.lblExamStateInformation.Name = "lblExamStateInformation";
            this.lblExamStateInformation.Size = new System.Drawing.Size(168, 22);
            this.lblExamStateInformation.TabIndex = 10;
            this.lblExamStateInformation.Text = "Exam is Not Started";
            // 
            // btnReadExamNote
            // 
            this.btnReadExamNote.Enabled = false;
            this.btnReadExamNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadExamNote.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnReadExamNote.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnReadExamNote.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReadExamNote.Location = new System.Drawing.Point(722, 315);
            this.btnReadExamNote.Name = "btnReadExamNote";
            this.btnReadExamNote.Size = new System.Drawing.Size(140, 65);
            this.btnReadExamNote.TabIndex = 18;
            this.btnReadExamNote.Text = "Read Exam Note";
            this.btnReadExamNote.UseVisualStyleBackColor = true;
            this.btnReadExamNote.Click += new System.EventHandler(this.btnReadExamNote_Click);
            // 
            // btnReplyByTyping
            // 
            this.btnReplyByTyping.Enabled = false;
            this.btnReplyByTyping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplyByTyping.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.btnReplyByTyping.ForeColor = System.Drawing.Color.Gray;
            this.btnReplyByTyping.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReplyByTyping.Location = new System.Drawing.Point(546, 486);
            this.btnReplyByTyping.Name = "btnReplyByTyping";
            this.btnReplyByTyping.Size = new System.Drawing.Size(319, 31);
            this.btnReplyByTyping.TabIndex = 19;
            this.btnReplyByTyping.Text = "Reply by Typing";
            this.btnReplyByTyping.UseVisualStyleBackColor = true;
            this.btnReplyByTyping.Click += new System.EventHandler(this.btnReplyByTyping_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label5.ForeColor = System.Drawing.Color.DarkOrange;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(599, 412);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 22);
            this.label5.TabIndex = 6;
            this.label5.Text = "Remaining Exam Time: ";
            // 
            // lblRemainingTime
            // 
            this.lblRemainingTime.AutoSize = true;
            this.lblRemainingTime.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblRemainingTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblRemainingTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRemainingTime.Location = new System.Drawing.Point(804, 412);
            this.lblRemainingTime.Name = "lblRemainingTime";
            this.lblRemainingTime.Size = new System.Drawing.Size(20, 22);
            this.lblRemainingTime.TabIndex = 6;
            this.lblRemainingTime.Text = "0";
            // 
            // timerExamTime
            // 
            this.timerExamTime.Interval = 1000;
            this.timerExamTime.Tick += new System.EventHandler(this.timerExamTime_Tick);
            // 
            // lblHaveMessage
            // 
            this.lblHaveMessage.AutoSize = true;
            this.lblHaveMessage.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHaveMessage.ForeColor = System.Drawing.Color.Red;
            this.lblHaveMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHaveMessage.Location = new System.Drawing.Point(879, 9);
            this.lblHaveMessage.Name = "lblHaveMessage";
            this.lblHaveMessage.Size = new System.Drawing.Size(171, 16);
            this.lblHaveMessage.TabIndex = 9;
            this.lblHaveMessage.Text = "You have an unread message";
            this.lblHaveMessage.Visible = false;
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1051, 727);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.checkBoxManuel);
            this.Controls.Add(this.btnReplyByTyping);
            this.Controls.Add(this.btnSendExamResultDocument);
            this.Controls.Add(this.btnScanForNewSessions);
            this.Controls.Add(this.btnRefreshSession);
            this.Controls.Add(this.btnEnterTheExam);
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.btnReadExamNote);
            this.Controls.Add(this.btnOpenExamDocument);
            this.Controls.Add(this.btnAskQuestionToAcademician);
            this.Controls.Add(this.btnInbox);
            this.Controls.Add(this.btnConnectDisconnectSession);
            this.Controls.Add(this.listViewSessions);
            this.Controls.Add(this.lblExamStateInformation);
            this.Controls.Add(this.lblHaveMessage);
            this.Controls.Add(this.lblSessionInformation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRemainingTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblListViewExamResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StudentForm";
            this.Text = "Student";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentForm_FormClosing);
            this.Load += new System.EventHandler(this.StudentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog fileDialogExam;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnSendExamResultDocument;
        private System.Windows.Forms.Button btnAskQuestionToAcademician;
        private System.Windows.Forms.Button btnInbox;
        private System.Windows.Forms.Button btnConnectDisconnectSession;
        private System.Windows.Forms.ListView listViewSessions;
        private System.Windows.Forms.ColumnHeader columnAcademicianMail;
        private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.Label lblSessionInformation;
        private System.Windows.Forms.Label lblListViewExamResult;
        private System.Windows.Forms.CheckBox checkBoxManuel;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Button btnReplyByTyping;
        private System.Windows.Forms.Button btnScanForNewSessions;
        private System.Windows.Forms.Button btnRefreshSession;
        private System.Windows.Forms.Button btnEnterTheExam;
        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.Button btnReadExamNote;
        private System.Windows.Forms.Button btnOpenExamDocument;
        private System.Windows.Forms.ColumnHeader columnAvailable;
        private System.Windows.Forms.Label lblExamStateInformation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRemainingTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timerExamTime;
        private System.Windows.Forms.Label lblHaveMessage;
    }
}