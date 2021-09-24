using ExamApp.Controllers;
using ExamApp.DataProviders;
using ExamApp.Entity;
using ExamApp.Views.Student;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Initializing Database & Json Config
            JsonDataProvider provider = new JsonDataProvider();
            JsonConfig config = provider.Config;
            if (!config.HostName.Equals(Dns.GetHostName()))
            {
                config.HostName = Dns.GetHostName();
                config.DefaultServerDHCPFormat = null;
                foreach (var item in Dns.GetHostAddresses(config.HostName))
                {
                    string ip = item.ToString();
                    for (int i = ip.Length - 1; i >= 0; i--)
                    {
                        if (ip[i].Equals('.'))
                        {
                            config.DefaultServerDHCPFormat = ip.Substring(0, i + 1);
                            break;
                        }
                    }
                    if (config.DefaultServerDHCPFormat != null)
                        break;
                }
                provider.UpdateJsonConfig(config);
            }
            Directory.CreateDirectory(config.DefaultExamDocumentPath);
            Database.SetInitializer<ExamAppDbContext>(new ExamAppContextInitializer());
            using (ExamAppDbContext context = new ExamAppDbContext())
            {
                context.Database.Initialize(true);
            }
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EfSqlDataProvider dataProvider = new EfSqlDataProvider();
            Server.DataProvider = dataProvider;
            Client.DataProvider = dataProvider;
            Application.Run(new LoginForm(dataProvider));
        }
    }
}
