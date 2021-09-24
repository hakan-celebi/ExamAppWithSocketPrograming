using ExamApp.DataProviders;
using ExamApp.Entity;
using ExamApp.Managers;
using ExamApp.Models;
using ExamApp.Resources;
using ExamApp.Views.Admin;
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
    public partial class AdminUserForm : Form
    {
        private Form RoutedFrom { get; set; }
        private IEfDataProvider DataProvider { get; set; }
        private ComponentResourceManager resources { get; set; }

        public AdminUserForm(IEfDataProvider dataProvider)
        {
            InitializeComponent();
            DataProvider = dataProvider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            RoutedFrom = this.Tag as Form;
            this.Tag = null;
            RefreshListView();
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnAdminUserForm));
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrAdminUserForm));
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            this.Text = resources.GetString("FormText");
            listViewUsers.Columns[0].Text = resources.GetString("ListViewColumnID");
            listViewUsers.Columns[1].Text = resources.GetString("ListViewColumnName");
            listViewUsers.Columns[2].Text = resources.GetString("ListViewColumnSurname");
            listViewUsers.Columns[3].Text = resources.GetString("ListViewColumnEMail");
            listViewUsers.Columns[4].Text = resources.GetString("ListViewColumnIP");
            listViewUsers.Columns[5].Text = resources.GetString("ListViewColumnRole");
            btnAdd.Text = resources.GetString("ButtonAdd");
            btnEdit.Text = resources.GetString("ButtonEdit");
            btnDelete.Text = resources.GetString("ButtonDelete");
            btnRoles.Text = resources.GetString("ButtonRoles");
            btnMessages.Text = resources.GetString("ButtonMessages");
            btnLogout.Text = resources.GetString("ButtonLogout");
        }

        public void RefreshListView()
        {
            listViewUsers.Items.Clear();
            List<Object> users = DataProvider.FindUsersByRole(UserRole.RoleEnum.admin);
            ListViewItem item;
            foreach (var user in users)
            {
                if ((user as User).IsLogged())
                    continue;
                item = new ListViewItem((user as User).Id);
                item.SubItems.Add((user as User).Name);
                item.SubItems.Add((user as User).Surname);
                item.SubItems.Add((user as User).EMail);
                item.SubItems.Add((user as User).IP);
                item.SubItems.Add((user as User).UserRole.Role.ToString());
                listViewUsers.Items.Add(item);
            }
            users = DataProvider.FindUsersByRole(UserRole.RoleEnum.academician);
            foreach (var user in users)
            {
                item = new ListViewItem((user as User).Id);
                item.SubItems.Add((user as User).Name);
                item.SubItems.Add((user as User).Surname);
                item.SubItems.Add((user as User).EMail);
                item.SubItems.Add((user as User).IP);
                item.SubItems.Add((user as User).UserRole.Role.ToString());
                listViewUsers.Items.Add(item);
            }
            users = DataProvider.FindUsersByRole(UserRole.RoleEnum.student);
            foreach (var user in users)
            {
                item = new ListViewItem((user as User).Id);
                item.SubItems.Add((user as User).Name);
                item.SubItems.Add((user as User).Surname);
                item.SubItems.Add((user as User).EMail);
                item.SubItems.Add((user as User).IP);
                item.SubItems.Add((user as User).UserRole.Role.ToString());
                listViewUsers.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUser frm = new AddUser(DataProvider);
            frm.Tag = this;
            frm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(listViewUsers.SelectedItems.Count > 0)
            {
                EditUser frm = new EditUser(DataProvider);
                frm.EditToUser = DataProvider.FindUserById(listViewUsers.SelectedItems[0].Text) as User;
                frm.Tag = this;
                frm.Show();
            }
            else
                MessageBox.Show(resources.GetString("ButtonEditAndDeleteClickListViewErrorText"),
                    resources.GetString("ButtonEditAndDeleteClickListViewErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewUsers.SelectedItems.Count > 0)
            {
                User deleteToUser = DataProvider.FindUserById(listViewUsers.SelectedItems[0].Text) as User;
                DialogResult result;
                result = MessageBox.Show($"{resources.GetString("ButtonDeleteClickText")} ({deleteToUser.EMail})",
                    resources.GetString("ButtonDeleteClickTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataProvider.DeleteUser(deleteToUser);
                    RefreshListView();
                }
            }
            else
                MessageBox.Show(resources.GetString("ButtonEditAndDeleteClickListViewErrorText"),
                    resources.GetString("ButtonEditAndDeleteClickListViewErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            EditUser frm = new EditUser(DataProvider);
            frm.EditToUser = SignInManager.LoggedUser;
            frm.Tag = this;
            frm.Show();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            AdminRoleForm frm = new AdminRoleForm(DataProvider);
            frm.Tag = this;
            this.Hide();
            frm.Show();
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            AdminMessageForm frm = new AdminMessageForm(DataProvider);
            frm.Tag = this;
            this.Hide();
            frm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SignInManager.LoggedUser.Logout();
            this.RemoveOwnedForm(RoutedFrom);
            RoutedFrom.Show();
            this.Close();
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