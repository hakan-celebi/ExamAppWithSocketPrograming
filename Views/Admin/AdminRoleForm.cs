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
    public partial class AdminRoleForm : Form
    {
        private IEfDataProvider DataProvider { get; set; }
        private Form RoutedForm { get; set; }
        private ComponentResourceManager resources { get; set; }

        public AdminRoleForm(IEfDataProvider provider)
        {
            InitializeComponent();
            DataProvider = provider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }

        private void AdminRoleForm_Load(object sender, EventArgs e)
        {
            RoutedForm = this.Tag as Form;
            this.Tag = null;
            RefreshListView();
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnAdminRoleForm));
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrAdminRoleForm));
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            this.Text = resources.GetString("FormText");
            listViewUserRoles.Columns[0].Text = resources.GetString("ListViewColumnID");
            listViewUserRoles.Columns[1].Text = resources.GetString("ListViewColumnRole");
            btnAdd.Text = resources.GetString("ButtonAdd");
            btnEdit.Text = resources.GetString("ButtonEdit");
            btnDelete.Text = resources.GetString("ButtonDelete");
            btnMessages.Text = resources.GetString("ButtonMessages");
            btnUsers.Text = resources.GetString("ButtonUsers");
        }
        public void RefreshListView()
        {
            listViewUserRoles.Items.Clear();
            List<Object> roles = DataProvider.AllUserRoles();
            ListViewItem item;
            foreach (var role in roles)
            {
                item = new ListViewItem((role as UserRole).Id.ToString());
                item.SubItems.Add((role as UserRole).Role.ToString());
                listViewUserRoles.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUserRole frm = new AddUserRole(DataProvider);
            frm.Tag = this;
            frm.Show();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listViewUserRoles.SelectedItems.Count > 0)
            {
                EditUserRole frm = new EditUserRole(DataProvider);
                frm.EditToUserRole = DataProvider.FindUserRoleById(int.Parse(listViewUserRoles.SelectedItems[0].Text)) as UserRole;
                frm.Tag = this;
                frm.Show();
            }
            else
                MessageBox.Show(resources.GetString("ButtonEditAndDeleteClickListViewErrorText"),
                    resources.GetString("ButtonEditAndDeleteClickListViewErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (listViewUserRoles.SelectedItems.Count > 0)
            {
                UserRole deleteToUserRole = DataProvider.FindUserRoleById(int.Parse(listViewUserRoles.SelectedItems[0].Text)) as UserRole;
                DialogResult result;
                result = MessageBox.Show($"{resources.GetString("ButtonDeleteClickText")} ({deleteToUserRole.Role})", 
                    resources.GetString("ButtonDeleteClickTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataProvider.DeleteUserRole(deleteToUserRole);
                    RefreshListView();
                }
            }
            else
                MessageBox.Show(resources.GetString("ButtonEditAndDeleteClickListViewErrorText"),
                    resources.GetString("ButtonEditAndDeleteClickListViewErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AdminRoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!RoutedForm.IsDisposed)
                RoutedForm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if(RoutedForm is AdminUserForm)
                this.Close();
            else
            {
                RoutedForm.Close();
                this.Close();
            }
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            AdminMessageForm frm = new AdminMessageForm(DataProvider);
            frm.Tag = this;
            this.Hide();
            frm.Show();
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
