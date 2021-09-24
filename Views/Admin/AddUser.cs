using ExamApp.ComboBoxControlLayer;
using ExamApp.DataProviders;
using ExamApp.Entity;
using ExamApp.Managers;
using ExamApp.Models;
using ExamApp.Resources;
using ExamApp.TextboxControlLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Views.Admin
{
    public partial class AddUser : Form
    {
        private Form RoutedForm { get; set; }
        private IEfDataProvider DataProvider { get; set; }
        private ComponentResourceManager resources { get; set; }

        public AddUser(IEfDataProvider provider)
        {
            InitializeComponent();
            DataProvider = provider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }
        private void AdminAddUserForm_Load(object sender, EventArgs e)
        {
            RoutedForm = this.Tag as Form;
            this.Tag = null;
            List<object> roles = DataProvider.AllUserRoles();
            foreach (var item in roles)
                cmbRoles.Items.Add((item as UserRole).Role.ToString());
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnAddUser));
            lblHeader.Location = new Point(69, 12);
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrAddUser));
            lblHeader.Location = new Point(5, 12);
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            this.Text = resources.GetString("FormText");
            lblHeader.Text = resources.GetString("LabelHeader");            
            txtName.SetPlaceHolderText(resources.GetString("TextBoxNamePlaceHolderText"));
            txtSurname.SetPlaceHolderText(resources.GetString("TextBoxSurnamePlaceHolderText"));
            txtEMail.SetPlaceHolderText(resources.GetString("TextBoxEMailPlaceHolderText"));
            txtPassword.SetPlaceHolderText(resources.GetString("TextBoxPasswordPlaceHolderText"));
            cmbRoles.SetPlaceHolderText(resources.GetString("ComboBoxRolePlaceHolderText"));
            btnAdd.Text = resources.GetString("ButtonAdd");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtErrorProvider.Clear();
            if (!txtName.IsEmpty(txtErrorProvider) & !txtSurname.IsEmpty(txtErrorProvider) & !txtEMail.IsEmpty(txtErrorProvider) & !txtPassword.IsEmpty(txtErrorProvider)
                & !cmbRoles.IsEmpty(txtErrorProvider))
            {
                UserRole role = (UserRole)DataProvider.FindUserRoleByRole(UserRole.RoleEnum.student);
                User addToUser = new User()
                {
                    Name = txtName.Text,
                    Surname = txtSurname.Text,
                    EMail = txtEMail.Text,
                    Password = txtPassword.Text,
                    UserRole = role,
                    UserRoleID = role.Id
                };
                if (DataProvider.AddUser(addToUser))
                {
                    MessageBox.Show(resources.GetString("ConfirmToAddUserText"), resources.GetString("ConfirmToAddUserTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((AdminUserForm)RoutedForm).RefreshListView();
                }
                else
                    MessageBox.Show(resources.GetString("FailedToAddUserText"), resources.GetString("FailedToAddUserTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            RoutedForm.Focus();
        }
        private void cmbLanguage_SelectedIndexChanged_1(object sender, EventArgs e)
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
