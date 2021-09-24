using ExamApp.Controllers;
using ExamApp.Dto;
using ExamApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Controllers
{
    public class ExamSessionController : Server
    {
        private static Exam Exam { get; set; }
        public static bool ExamSessionIsStarted { get; set; }
        public static bool ExamIsStarted { get; set; }
        public static bool ExamIsLocked { get; set; }
        public static List<User> ConnectedStudents { get; set; } = new List<User>();
        public static List<User> StudentsTakingTheExam { get; set; } = new List<User>();
        public static List<User> StudentsFinishedExam { get; set; } = new List<User>();
        public static bool StartExamSession(User user)
        {
            if (ExamSessionIsStarted || user is null || user.UserRole is null || !user.UserRole.Role.Equals(UserRole.RoleEnum.academician))
                return false;
            ExamSessionIsStarted = true;
            SetupServer(user);
            AcademicianForm.UpdateFormControlsWhenServerStarted();
            return true;
        }
        public static bool StopExamSession()
        {
            if (!ExamSessionIsStarted)
                return false;
            if (StudentsTakingTheExam.Count > 0)
            {
                switch (MessageBox.Show(AcademicianForm, "There are students/students who continue the exam. Are you sure you want to log out?",
                    "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        ExamSessionIsStarted = ExamIsLocked = ExamIsStarted = false;
                        CloseServer();
                        return true;
                    default: return false;
                }
            }
            ExamSessionIsStarted = ExamIsLocked = ExamIsStarted = false;
            CloseServer();
            ConnectedStudents.Clear();
            StudentsTakingTheExam.Clear();
            StudentsFinishedExam.Clear();
            Exam = null;
            AcademicianForm.UpdateFormControlsWhenServerStoped();
            return true;
        }
        public static bool StartExam(Exam exam)
        {
            if (!ExamSessionIsStarted || ExamIsStarted)
                return false;
            StudentsFinishedExam.Clear();
            Exam = exam;
            AcademicianForm.UpdateFormControlsWhenExamStarted();
            ExamIsStarted = true;
            SendExam(exam);
            return true;
        }

        public static bool FinishTheExam()
        {            
            if (!ExamIsStarted || !ExamSessionIsStarted)
                return false;
            if (StudentsTakingTheExam.Count > 0)
            {
                switch (MessageBox.Show(AcademicianForm, "There are students/students attending the exam. Are you sure you want to finish the exam?",
                   "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        ExamIsLocked = ExamIsStarted = false;
                        AcademicianForm.UpdateFormControlsWhenExamFinished();
                        return true;
                    default: return false;
                }
            }
            string responseText = default;
            Models.Message response = SessionStopedResponse;
            JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
            foreach (var item in ClientSockets)
            {
                response.Data = Encoding.ASCII.GetBytes(responseText);
                response.ReceiverUser = FindUser(item);
                response.SenderUser = User;
                SendResponse(response);
                DataProvider.AddMessage(response);
            }
            Exam = null;
            StudentsTakingTheExam.Clear();
            ExamIsLocked = ExamIsStarted = false;
            AcademicianForm.UpdateFormControlsWhenExamFinished();
            return true;
        }
        internal static void SendMessage(Models.Message response)
        {
            SendResponse(response);
        }

        internal static void Message_Received(Models.Message request)
        {
            AcademicianForm.GetMessage(request);
        }

        internal static void Student_Connected(User student)
        {
            AcademicianForm.UpdateFormControlsWhenClientConnectted(student);
        }

        internal static void Exam_Entered(User student)
        {
            AcademicianForm.StudentEnteredToExam(student);
        }

        internal static void Student_Ready(User student)
        {
            AcademicianForm.StudentIsReadyForExam(student);
        }

        internal static void Student_FinishedExam(User student)
        {
            AcademicianForm.SubmitExamResult(student);
        }

        internal static void Student_Disconnected(User student)
        {
            AcademicianForm.UpdateFormControlsWhenClientDisonnectted(student);
        }
    }
}
