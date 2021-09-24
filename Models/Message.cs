using System;

namespace ExamApp.Models
{
    [Serializable]
    public class Message
    {
        public int Id { get; set; }
        public int MCode { get; set; }
        public byte[] Data { get; set; }
        public DateTime Time { get; set; }
        public string SenderUserID { get; set; }
        public string ReceiverUserID { get; set; }
        public virtual User SenderUser { get; set; }
        public virtual User ReceiverUser { get; set; }
    }
}
