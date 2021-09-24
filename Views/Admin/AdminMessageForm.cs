using ExamApp.DataProviders;
using ExamApp.Entity;
using ExamApp.Managers;
using ExamApp.Models;
using ExamApp.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Views.Admin
{
    public partial class AdminMessageForm : Form
    {
        private Form RoutedFrom { get; set; }
        private IEfDataProvider DataProvider { get; set; }
        private ComponentResourceManager resources { get; set; }

        public AdminMessageForm(IEfDataProvider provider)
        {
            InitializeComponent();
            DataProvider = provider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }

        private void AdminMessageForm_Load(object sender, EventArgs e)
        {
            RoutedFrom = this.Tag as Form;
            this.Tag = null;
            List<object> messages = DataProvider.AllMessages();
            ListViewItem item;
            foreach (var message in messages)
            {
                item = new ListViewItem((message as Models.Message).Id.ToString());
                item.SubItems.Add((message as Models.Message).MCode.ToString());
                if ((message as Models.Message).Data is null)
                    item.SubItems.Add("Null");
                else
                    item.SubItems.Add((message as Models.Message).Data.GetType().ToString());
                item.SubItems.Add((message as Models.Message).Time.ToString());
                item.SubItems.Add((message as Models.Message).SenderUser.EMail);
                if(!((message as Models.Message).ReceiverUser is null))
                    item.SubItems.Add((message as Models.Message).ReceiverUser.EMail);
                else
                    item.SubItems.Add("Null");
                listViewMessages.Items.Add(item);
            }
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnAdminMessageForm));
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrAdminMessageForm));
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            this.Text = resources.GetString("FormText");
            listViewMessages.Columns[0].Text = resources.GetString("ListViewColumnID");
            listViewMessages.Columns[1].Text = resources.GetString("ListViewColumnCode");
            listViewMessages.Columns[2].Text = resources.GetString("ListViewColumnData");
            listViewMessages.Columns[3].Text = resources.GetString("ListViewColumnDate");
            listViewMessages.Columns[4].Text = resources.GetString("ListViewColumnSenderEMail");
            listViewMessages.Columns[5].Text = resources.GetString("ListViewColumnReceiverEMail");
            btnRoles.Text = resources.GetString("ButtonRoles");
            btnUsers.Text = resources.GetString("ButtonUsers");
        }

        private void AdminMessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!RoutedFrom.IsDisposed)
                RoutedFrom.Show();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            if (RoutedFrom is AdminUserForm)
            {
                AdminRoleForm frm = new AdminRoleForm(DataProvider);
                frm.Tag = this;
                this.Hide();
                frm.Show();
            }
            else
                this.Close();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (RoutedFrom is AdminUserForm)
                this.Close();
            else
            {
                this.Close();
                RoutedFrom.Close();
            }
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SignInManager.Language = Convert.ToBoolean(cmbLanguage.SelectedIndex);
            switch (cmbLanguage.SelectedIndex)
            {
                case 0:
                    SetLanguageToEnglish();
                    break;
                case 1:
                    SetLanguageToTurkish();
                    break;
                default:
                    break;
            }
        }
    }
}
