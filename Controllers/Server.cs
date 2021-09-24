using ExamApp.DataProviders;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ExamApp.Models;
using ExamApp.Entity;
using ExamApp.Managers;
using System.Drawing;
using ExamApp.Dto;

namespace ExamApp.Controllers
{
    public class Server
    {
        public static IEfDataProvider DataProvider { get; set; }
        protected static JsonDataProvider JsonConfig { get; set; } = new JsonDataProvider();
        private static AddressFamily AddressFamily { get; set; } = (AddressFamily)Enum.Parse(typeof(AddressFamily), JsonConfig.Config.AddressFamily);
        private static SocketType SocketType { get; set; } = (SocketType)Enum.Parse(typeof(SocketType), JsonConfig.Config.SocketType);
        private static ProtocolType ProtocolType { get; set; } = (ProtocolType)Enum.Parse(typeof(ProtocolType), JsonConfig.Config.ProtocolType);
        private static Socket ServerSocket { get; set; } = new Socket(AddressFamily, SocketType, ProtocolType);
        protected static List<Socket> ClientSockets { get; set; } = new List<Socket>();
        private static byte[] DataBuffer { get; set; } = new byte[JsonConfig.Config.DataByteCount];
        protected static Models.Message OkResponse { get => new Models.Message() { MCode = 200 }; }
        protected static Models.Message AcceptedResponse { get => new Models.Message() { MCode = 201 }; }
        protected static Models.Message WaitingToLoginResponse { get => new Models.Message() { MCode = 100 }; }
        protected static Models.Message WaitingToRegisterResponse { get => new Models.Message() { MCode = 101 }; }
        protected static Models.Message ExamDocumentResponse { get => new Models.Message() { MCode = 102 }; }
        protected static Models.Message AcademicianNoteResponse { get => new Models.Message() { MCode = 103 }; }
        protected static Models.Message AnswerToQuestionResponse { get => new Models.Message() { MCode = 104 }; }
        protected static Models.Message SessionStopedResponse { get => new Models.Message() { MCode = 105 }; }
        protected static Models.Message ExamFinishedResponse { get => new Models.Message() { MCode = 106 }; }
        protected static Models.Message NotOkResponse { get => new Models.Message() { MCode = 400 }; }
        protected static Models.Message NotAcceptedResponse { get => new Models.Message() { MCode = 401 }; }
        protected static Models.Message FailedResponse { get => new Models.Message() { MCode = 402 }; }
        protected static User User { get; set; }
        public static AcademicianForm AcademicianForm { get; set; }

        protected static void SendResponse(Models.Message response)
        {
            Socket ClientSocket = FindSocket(response.ReceiverUser);
            if (ClientSocket is null)
                return;
            response.Time = DateTime.Now;
            byte[] SendingData = response.ObjectSerializer();
            ClientSocket.Send(SendingData, 0, SendingData.Length, SocketFlags.None);
            DataProvider.AddMessage(response);
        }
        protected static void SendExam(Exam exam)
        {
            Models.Message response = ExamDocumentResponse;
            response.Data = exam.ObjectSerializer();
            response.SenderUser = User;
            foreach (var item in ExamSessionController.StudentsTakingTheExam)
            {
                response.ReceiverUser = item;
                SendResponse(response);
            }
        }
        private static IPEndPoint GetUserIPEndPoint(User student)
        {
            IPEndPoint ip = null;
            for (int i = student.IP.Length - 1; i >= 0; i--)
            {
                if (student.IP[i].Equals(':'))
                {
                    ip = new IPEndPoint(IPAddress.Parse(student.IP.Substring(0, i)), int.Parse(student.IP.Substring(i + 1)));
                    break;
                }
            }
            return ip;
        }
        private static Socket FindSocket(User student)
        {
            Socket ClientSocket = null;
            IPEndPoint ip = GetUserIPEndPoint(student);
            foreach (var item in ClientSockets)
            {
                if (item.RemoteEndPoint.Equals(ip))
                {
                    ClientSocket = item;
                    break;
                }
            }
            return ClientSocket;
        }

        protected static User FindUser(Socket clientSocket)
        {
            List<object> students = DataProvider.FindUsersByRole(UserRole.RoleEnum.student);
            foreach (User item in students)
            {
                if (item.IP.Equals(clientSocket.RemoteEndPoint.ToString()))
                    return item;
            }
            return null;
        }

        private static Models.Message CreateResponse(Models.Message request)
        {
            if (request is null || request.SenderUser is null)
                return null;
            User student = DataProvider.FindUserByEMail(request.SenderUser.EMail) as User;
            string responseText = default;
            Models.Message response = null;
            if (student is null)
            {
                switch (request.MCode)
                {
                    case 200: /*Connect Request*/
                        /*Register Response*/
                        response = WaitingToRegisterResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                    case 201: /*Disconnect Request*/
                        /*Ok(Disconnect) Response*/
                        response = OkResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        ExamSessionController.Student_Disconnected(student);
                        break;
                    case 203: /*Register Request*/
                        student = request.SenderUser;
                        if (!DataProvider.AddUser(student))
                        {
                            /*Register Not Accepted Response*/
                            response = NotAcceptedResponse;
                            JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                            break;
                        }
                        /*Accepted(Register) Response*/
                        ExamSessionController.ConnectedStudents.Add(student);
                        ExamSessionController.Student_Connected(student);
                        response = AcceptedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                    default:
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                }
                response.Data = Encoding.ASCII.GetBytes(responseText);
                response.ReceiverUser = request.SenderUser;
                response.SenderUser = User;
                return response;
            }
            if(!student.IP.Equals(request.SenderUser.IP))
            {
                student.IP = request.SenderUser.IP;
                DataProvider.UpdateUser(student);
            }
            switch (request.MCode)
            {
                case 200: /*Connect Request*/
                    if (ExamSessionController.ConnectedStudents.Contains(student))
                    {
                        /*Failed Response*/
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                    }
                    /*Login Response*/
                    response = WaitingToLoginResponse;
                    JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    break;
                case 201: /*Disconnect Request*/
                    /*Ok(Disconnect) Response*/
                    response = OkResponse;
                    JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    ExamSessionController.Student_Disconnected(student);
                    break;
                case 202: /*Login Request*/
                    if (ExamSessionController.ConnectedStudents.Contains(student))
                    {
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                    }
                    /*Accepted(Login) Response*/
                    student.IP = request.SenderUser.IP;
                    DataProvider.UpdateUser(student);
                    ExamSessionController.ConnectedStudents.Add(student);
                    ExamSessionController.Student_Connected(student);
                    response = AcceptedResponse;
                    JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    break;
                case 204: /*Take the exam Request*/
                    if (!ExamSessionController.ExamSessionIsStarted || (ExamSessionController.ExamIsStarted && ExamSessionController.ExamIsLocked)
                        || ExamSessionController.StudentsTakingTheExam.Contains(student))
                    {
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                    }
                    ExamSessionController.StudentsTakingTheExam.Add(student);
                    ExamSessionController.Exam_Entered(student);
                    response = AcceptedResponse;
                    JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    break;
                case 205: /*Ask the academician a question Request*/
                    if (!ExamSessionController.StudentsTakingTheExam.Contains(student))
                    {
                        /*Failed Response*/
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                        break;
                    }
                    /*Getting Message*/
                    ExamSessionController.Message_Received(request);
                    break;
                case 100: /*Waiting for the exam to start*/
                    if (!ExamSessionController.StudentsTakingTheExam.Contains(student))
                    {
                        /*Failed Response*/
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    }
                    ExamSessionController.Student_Ready(student);
                    break;
                case 101: /*Submit exam result Request*/
                    if (!ExamSessionController.StudentsTakingTheExam.Contains(student))
                    {
                        /*Failed Response*/
                        response = FailedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    }
                    else
                    {
                        /*Accepted Response*/
                        ExamSessionController.StudentsTakingTheExam.Remove(student);
                        ExamSessionController.StudentsFinishedExam.Add(student);
                        ExamSessionController.Student_FinishedExam(student);
                        response = AcceptedResponse;
                        JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    }
                    break;
                default:
                    /*Failed Response*/
                    response = FailedResponse;
                    JsonConfig.Config.ResponseCodes.TryGetValue(response.MCode.ToString(), out responseText);
                    break;
            }
            if (response is null)
                return null;
            response.Data = Encoding.ASCII.GetBytes(responseText);
            response.ReceiverUser = request.SenderUser;
            response.SenderUser = User;
            return response;
        }

        private static void ReceiveCallBack(IAsyncResult AR)
        {
            try
            {
                Socket clientSocket = (Socket)AR.AsyncState;
                int received = clientSocket.EndReceive(AR);
                if (received == 0)
                {
                    if (clientSocket is null)
                        return;
                    ClientSockets.Remove(clientSocket);
                    return;
                }
                byte[] TemproraryDataBuffer = new byte[received];
                Array.Copy(DataBuffer, TemproraryDataBuffer, received);
                Models.Message receivedMessage = TemproraryDataBuffer.ObjectDeserializer() as Models.Message;
                DataProvider.AddMessage(receivedMessage);
                Models.Message response = CreateResponse(receivedMessage);
                if (!(response is null))
                    SendResponse(response);
                if(!(clientSocket is null) && clientSocket.Connected)
                    clientSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                Socket clientSocket = ServerSocket.EndAccept(AR);
                ClientSockets.Add(clientSocket);
                clientSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);
                ServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected static void SetupServer(User user)
        {
            if (SignInManager.LoggedUser.UserRole.Role != UserRole.RoleEnum.academician)
                return;
            IPEndPoint ip = default;
            for (int i = user.IP.Length - 1; i >= 0; i--)
            {
                if (user.IP[i].Equals(':'))
                {
                    ip = new IPEndPoint(IPAddress.Parse(user.IP.Substring(0, i)), int.Parse(user.IP.Substring(i + 1)));
                    break;
                }
            }
            User = user;
            try
            {
                ServerSocket.Bind(ip);
                ServerSocket.Listen(0);
                ServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected static void CloseServer()
        {
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
                item.Shutdown(SocketShutdown.Receive);
                item.Close();
                ClientSockets.Remove(item);
            }
            ServerSocket.Close();
        }
    }
}