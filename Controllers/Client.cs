using ExamApp.DataProviders;
using ExamApp.Dto;
using ExamApp.Managers;
using ExamApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Controllers
{
    public class Client
    {
        public static IEfDataProvider DataProvider { get; set; }
        private static JsonDataProvider JsonConfig { get; set; } = new JsonDataProvider();
        private static AddressFamily AddressFamily { get; set; } = (AddressFamily)Enum.Parse(typeof(AddressFamily), JsonConfig.Config.AddressFamily);
        private static SocketType SocketType { get; set; } = (SocketType)Enum.Parse(typeof(SocketType), JsonConfig.Config.SocketType);
        private static ProtocolType ProtocolType { get; set; } = (ProtocolType)Enum.Parse(typeof(ProtocolType), JsonConfig.Config.ProtocolType);
        public static User Academician { get; private set; }
        private static Socket ClientSocket { get; set; } = new Socket(AddressFamily, SocketType, ProtocolType);
        private static byte[] DataBuffer { get; set; } = new byte[JsonConfig.Config.DataByteCount];
        public static Exam Exam { get; private set; }
        private static Models.Message LastRequest { get; set; }
        public static Form Context { get; set; }
        public static bool IsConnected { get; private set; }
        public static bool IsEnteredExam { get; private set; }
        public static bool IsReady { get; private set; }
        public static string ExamDocumentFile { get; private set; }
        public static string ExamNote { get; private set; }
        public static List<Models.Message> AcademicianNotes { get; private set; } = new List<Models.Message>();
        public static List<Models.Message> ReadedMessages { get; set; } = new List<Models.Message>();
        private static Models.Message ConnectRequest { get => new Models.Message() { MCode = 200 }; }
        private static Models.Message DisconnectRequest { get => new Models.Message() { MCode = 201 }; }
        private static Models.Message LoginRequest { get => new Models.Message() { MCode = 202 }; }
        private static Models.Message RegisterRequest { get => new Models.Message() { MCode = 203 }; }
        private static Models.Message TakeTheExamRequest { get => new Models.Message() { MCode = 204 }; }
        private static Models.Message QuestionRequest { get => new Models.Message() { MCode = 205 }; }
        private static Models.Message WaitingForExamStartRequest { get => new Models.Message() { MCode = 100 }; }
        private static Models.Message SubmitExamResultRequest { get => new Models.Message() { MCode = 101 }; }
        public static void SendRequest(Models.Message request)
        {
            request.Time = DateTime.Now;
            byte[] SendingData = request.ObjectSerializer();
            ClientSocket.Send(SendingData, 0, SendingData.Length, SocketFlags.None);
            DataProvider.AddMessage(request);
            LastRequest = request;
        }
        public static void EnterExam()
        {
            if (IsEnteredExam)
                return;
            string requestText = default;
            Models.Message request = TakeTheExamRequest;
            JsonConfig.Config.RequestCodes.TryGetValue(request.MCode.ToString(), out requestText);
            request.Data = Encoding.ASCII.GetBytes(requestText);
            request.SenderUser = SignInManager.LoggedUser;
            request.SenderUserID = request.SenderUser.Id;
            request.ReceiverUser = Academician;
            request.ReceiverUserID = Academician.Id;
            SendRequest(request);
        }
        public static void ReadyExam()
        {
            if (!IsEnteredExam && IsReady)
                return;
            string requestText = default;
            Models.Message request = WaitingForExamStartRequest;
            JsonConfig.Config.RequestCodes.TryGetValue(request.MCode.ToString(), out requestText);
            request.Data = Encoding.ASCII.GetBytes(requestText);
            request.SenderUser = SignInManager.LoggedUser;
            request.ReceiverUser = Academician;
            request.SenderUserID = request.SenderUser.Id;
            request.ReceiverUserID = Academician.Id;
            IsReady = true;
            SendRequest(request);
        }
        public static void FinishExam(Exam exam)
        {
            if (!IsConnected || !IsEnteredExam)
                return;
            Models.Message request = SubmitExamResultRequest;
            request.Data = exam.ObjectSerializer();
            request.SenderUser = SignInManager.LoggedUser;
            request.ReceiverUser = Academician;
            request.SenderUserID = request.SenderUser.Id;
            request.ReceiverUserID = Academician.Id;
            SendRequest(request);
        }
        public static void AskQuestion(string question)
        {
            Models.Message request = QuestionRequest;
            request.Data = Encoding.ASCII.GetBytes(question);
            request.SenderUser = SignInManager.LoggedUser;
            request.ReceiverUser = Academician;
            request.SenderUserID = request.SenderUser.Id;
            request.ReceiverUserID = Academician.Id;
            SendRequest(request);
        }

        #region Getting IPEndPoint Object from String
        private static IPEndPoint GetIPEndPointByIPString(string ipString)
        {
            IPEndPoint ip = null;
            for (int i = ipString.Length - 1; i >= 0; i--)
            {
                if (ipString[i].Equals(':'))
                {
                    ip = new IPEndPoint(IPAddress.Parse(ipString.Substring(0, i)), int.Parse(ipString.Substring(i + 1)));
                    break;
                }
            }
            return ip;
        }
        #endregion

        #region Scaning for Available Session from ListView
        public static void RefreshListView(ListView listView)
        {
            if (IsConnected)
                return;
            foreach (ListViewItem item in listView.Items)
            {
                try
                {
                    ClientSocket.Connect(GetIPEndPointByIPString(item.SubItems[1].Text));
                }
                catch (SocketException)
                {
                    item.SubItems[2].Text = "Not Available";
                    item.SubItems[2].ForeColor = Color.Red;
                }
                if (ClientSocket.Connected)
                {
                    item.SubItems[2].Text = "Available";
                    item.SubItems[2].ForeColor = Color.Green;
                    ClientSocket.Close();
                }
            }
            ClientSocket = new Socket(AddressFamily, SocketType, ProtocolType);
        }
        #endregion

        #region Scaning for New Sessions
        public static void LoopConnect(ListView listView)
        {
            if (IsConnected)
                return;
            int ip = JsonConfig.Config.IPRangeForScanStart;
            ListViewItem listViewContainsItem = null;
            while (ip != JsonConfig.Config.IPRangeForScanEnd)
            {
                try
                {
                    listViewContainsItem = null;
                    foreach (ListViewItem item in listView.Items)
                    {
                        if (item.SubItems[1].Text.Equals(JsonConfig.Config.DefaultServerDHCPFormat + ip.ToString()))
                        {
                            listViewContainsItem = item;
                            break;
                        }
                    }
                    ClientSocket.Connect(IPAddress.Parse(JsonConfig.Config.DefaultServerDHCPFormat + ip.ToString()), JsonConfig.Config.DefaultServerPort);

                }
                catch (SocketException)
                {
                    if (!(listViewContainsItem is null))
                    {
                        listViewContainsItem.SubItems[2].Text = "Not Available";
                        listViewContainsItem.SubItems[2].ForeColor = Color.Red;
                    }
                }
                if (ClientSocket.Connected)
                {
                    if (!(listViewContainsItem is null))
                    {
                        listViewContainsItem.SubItems[2].Text = "Available";
                        listViewContainsItem.SubItems[2].ForeColor = Color.Green;
                    }
                    else
                    {
                        Models.Message response;
                        Models.Message request = TakeTheExamRequest;
                        request.Data = Encoding.ASCII.GetBytes("Test Connection");
                        request.ReceiverUser = null;
                        request.SenderUser = SignInManager.LoggedUser;
                        SendRequest(request);
                        ClientSocket.Receive(DataBuffer, SocketFlags.None);
                        response = DataBuffer.ObjectDeserializer() as Models.Message;
                        DataProvider.AddUser(response.SenderUser);
                        ListViewItem temp = new ListViewItem();
                        temp.Text = response.SenderUser.EMail;
                        temp.SubItems.Add(response.SenderUser.IP);
                        temp.SubItems.Add("Available");
                        temp.UseItemStyleForSubItems = false;
                        temp.SubItems[2].ForeColor = Color.Green;
                        listView.Items.Add(temp);
                        ClientSocket.Shutdown(SocketShutdown.Both);
                        ClientSocket.Close();
                        ClientSocket = new Socket(AddressFamily, SocketType, ProtocolType);
                    }
                }
                ip++;
            }
        }
        #endregion

        #region CreatingRequest
        private static Models.Message CreateRequest(Models.Message response)
        {
            if (response is null || response.SenderUser is null)
                return null;
            Academician = DataProvider.FindUserByEMail(response.SenderUser.EMail) as User;
            if (Academician is null)
            {
                Academician = response.SenderUser;
                if (DataProvider.AddUser(Academician))
                    return null;
            }
            Models.Message request = null;
            string requestText = default;
            switch (response.MCode)
            {
                case 200: /*OK Response*/
                    if (LastRequest is null)
                        break;
                    switch (LastRequest.MCode)
                    {
                        case 200:
                            (Context as StudentForm).Session_Connected();
                            IsConnected = true;
                            break;
                        case 201:
                            SocketClose();
                            (Context as StudentForm).Session_Disconnected();
                            break;
                    }
                    break;
                case 201: /*Accepted Response*/
                    switch (LastRequest.MCode)
                    {
                        case 202: /*Login Request*/
                            (Context as StudentForm).Session_Connected();
                            IsConnected = true;
                            break;
                        case 203: /*Register Request*/
                            (Context as StudentForm).Session_Connected();
                            IsConnected = true;
                            break;
                        case 204: /*Take the exam Request*/
                            IsEnteredExam = true;
                            (Context as StudentForm).Exam_Entered();
                            break;
                        case 101: /*Submit exam result Request*/
                            IsEnteredExam = false;
                            (Context as StudentForm).Exam_Submited();
                            break;
                    }
                    break;
                case 100: /*waiting to login Response*/
                    request = LoginRequest;
                    JsonConfig.Config.RequestCodes.TryGetValue(LoginRequest.MCode.ToString(), out requestText);
                    break;
                case 101: /*waiting to register Response*/
                    request = RegisterRequest;
                    JsonConfig.Config.RequestCodes.TryGetValue(LoginRequest.MCode.ToString(), out requestText);
                    break;
                case 102: /*Exam document Response*/
                    Exam = response.Data.ObjectDeserializer() as Exam;
                    string filePath = default;
                    if (Exam is null)
                        break;
                    string time = response.Time.ToString("dd.MM.yy-HH.mm.ss");
                    filePath = JsonConfig.Config.DefaultExamDocumentPath + "/" + Exam.LessonName + time + Exam.FileExtension;
                    Exam.ExamDocument.FileDeserializer(filePath);
                    (Context as StudentForm).Exam_Started(Exam.ExamTime.ToString());
                    ExamDocumentFile = AppDomain.CurrentDomain.BaseDirectory + filePath;
                    ExamNote = Exam.AcademicianNote;
                    Process.Start(ExamDocumentFile);
                    break;
                case 103: /*Academician note Response*/
                    AcademicianNotes.Add(response);
                    (Context as StudentForm).AcademicianNote_Received(response);
                    break;
                case 104: /*Answer to question Response*/
                    MessageBox.Show(Encoding.ASCII.GetString(response.Data), "Answer to Your Question. Form -> " + response.SenderUser.EMail, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    AcademicianNotes.Add(response);
                    break;
                case 105: /*Session Stopped Response*/
                    MessageBox.Show("Acamician stopped The Session", "Session Stoped!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    (Context as StudentForm).Session_Disconnected();
                    break;
                case 106: /*Exam Finished Response*/
                    MessageBox.Show("Acamician finished The Exam", "Exam Stoped!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    (Context as StudentForm).Exam_Finished();
                    break;
                case 400: /*Not OK Response*/
                    MessageBox.Show(LastRequest.MCode.ToString() + " - " + Encoding.ASCII.GetString(LastRequest.Data), "Not OK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 401: /*Not Accepted Response*/
                    MessageBox.Show(LastRequest.MCode.ToString() + " - " + Encoding.ASCII.GetString(LastRequest.Data), "Not Accepted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 402: /*Failed Response*/
                    MessageBox.Show(LastRequest.MCode.ToString() + " - " + Encoding.ASCII.GetString(LastRequest.Data), "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
            if (request is null)
                return null;
            request.Data = Encoding.ASCII.GetBytes(requestText);
            request.ReceiverUser = response.SenderUser;
            request.SenderUser = SignInManager.LoggedUser;
            request.SenderUserID = request.SenderUser.Id;
            request.ReceiverUserID = Academician.Id;
            return request;
        }
        #endregion
        public static bool Connect(string ip)
        {
            IPEndPoint ipEndPoint = null;
            foreach (var item in ip)
            {
                if (item.Equals(':'))
                {
                    ipEndPoint = GetIPEndPointByIPString(ip);
                    break;
                }
            }
            if (ipEndPoint is null)
                ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), JsonConfig.Config.DefaultServerPort);
            try
            {
                ClientSocket.BeginConnect(ipEndPoint, new AsyncCallback(ConnectCallback), null);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public static void Disconnect()
        {
            if (!IsConnected)
                return;
            string requestText = default;
            Models.Message request = DisconnectRequest;
            JsonConfig.Config.RequestCodes.TryGetValue(request.MCode.ToString(), out requestText);
            request.SenderUser = SignInManager.LoggedUser;
            request.ReceiverUser = Academician;
            request.SenderUserID = SignInManager.LoggedUser.Id;
            request.ReceiverUserID = Academician.Id;
            request.Time = DateTime.Now;
            SendRequest(request);
        }
        private static void SocketClose()
        {
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            ClientSocket = new Socket(AddressFamily, SocketType, ProtocolType);
            IsConnected = IsEnteredExam = false;
        }

        private static void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                ClientSocket.EndConnect(AR);
                SignInManager.LoggedUser.IP = ClientSocket.LocalEndPoint.ToString();
                DataProvider.UpdateUser(SignInManager.LoggedUser);
                Models.Message request = ConnectRequest;
                string requestText = default;
                JsonConfig.Config.RequestCodes.TryGetValue(request.MCode.ToString(), out requestText);
                request.Data = Encoding.ASCII.GetBytes(requestText);
                request.SenderUser = SignInManager.LoggedUser;
                request.SenderUserID = SignInManager.LoggedUser.Id;
                ClientSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                SendRequest(request);  
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                (Context as StudentForm).Session_Disconnected();
            }
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                int received = ClientSocket.EndReceive(AR);
                if (received == 0)
                    return;
                Models.Message receivedMessage = DataBuffer.ObjectDeserializer() as Models.Message;
                DataProvider.AddMessage(receivedMessage);
                Models.Message request = CreateRequest(receivedMessage);
                if (!(request is null))
                    SendRequest(request);
                if (ClientSocket.Connected)
                    ClientSocket.BeginReceive(DataBuffer, 0, DataBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
