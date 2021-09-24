using ExamApp.DataProviders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ExamApp.Models
{
    [Serializable]
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            JsonDataProvider provider = new JsonDataProvider();
            IPHostEntry Host = Dns.GetHostEntry(provider.Config.HostName);
            IPAddress UserIP = null;
            string defaultDhcpFormat = provider.Config.DefaultServerDHCPFormat;
            foreach (var item in Host.AddressList)
            {
                if (defaultDhcpFormat.Equals(item.ToString().Substring(0, defaultDhcpFormat.Length)))
                    UserIP = item;
            }
            IP = new IPEndPoint(UserIP, provider.Config.DefaultServerPort).ToString();

        }
        [DatabaseGenerated(DatabaseGeneratedOption.None), Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
        public int UserRoleID { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
