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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.Views.Admin
{
    public partial class EditUser : Form
    {
        public User EditToUser { get; set; }
        private Form RoutedForm { get; set; }
        private IEfDataProvider DataProvider { get; set; }
        private ComponentResourceManager resources { get; set; }

        public EditUser(IEfDataProvider provider)
        {
            InitializeComponent();
            DataProvider = provider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }

        private void EditUser_Load(object sender, EventArgs e)
        {
            RoutedForm = this.Tag as Form;
            this.Tag = null;
            List<object> roles = DataProvider.AllUserRoles();
            foreach (var item in roles)
                cmbRoles.Items.Add((item as UserRole).Role.ToString());
            if(EditToUser != null)
            {
                txtName.Text = EditToUser.Name;
                txtSurname.Text = EditToUser.Surname;
                txtEMail.Text = EditToUser.EMail;
                txtPassword.Text = EditToUser.Password;
                cmbRoles.SelectedItem = EditToUser.UserRole.Role.ToString();
            }
            else
                MessageBox.Show(resources.GetString("ConfirmToEditUserText"), resources.GetString("ConfirmToEditUserTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtName.ForeColor = txtSurname.ForeColor = txtEMail.ForeColor = txtPassword.ForeColor = default;
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnEditUser));
            lblHeader.Location = new Point(69, 12);
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrEditUser));
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
            btnSave.Text = resources.GetString("ButtonSave");
            if (!(EditToUser is null))
            {
                txtName.Text = EditToUser.Name;
                txtSurname.Text = EditToUser.Surname;
                txtEMail.Text = EditToUser.EMail;
                txtPassword.Text = EditToUser.Password;
                cmbRoles.SelectedItem = EditToUser.UserRole.Role.ToString();
                txtName.ForeColor = txtSurname.ForeColor = txtEMail.ForeColor = txtPassword.ForeColor = default;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtName.IsEmpty(txtErrorProvider) & !txtSurname.IsEmpty(txtErrorProvider) & !txtEMail.IsEmpty(txtErrorProvider) & !txtPassword.IsEmpty(txtErrorProvider)
                & !cmbRoles.IsEmpty(txtErrorProvider))
            {
                UserRole role = DataProvider.FindUserRoleByRole((UserRole.RoleEnum)Enum.Parse(typeof(UserRole.RoleEnum), cmbRoles.SelectedItem.ToString())) as UserRole;
                EditToUser.Name = txtName.Text;
                EditToUser.Surname = txtSurname.Text;
                EditToUser.EMail = txtEMail.Text;
                EditToUser.Password = txtPassword.Text;
                EditToUser.UserRole = role;
                EditToUser.UserRoleID = role.Id;
                if (DataProvider.UpdateUser(EditToUser))
                {
                    MessageBox.Show(resources.GetString("ConfirmToEditUserText"), resources.GetString("ConfirmToEditUserTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((AdminUserForm)RoutedForm).RefreshListView();
                    this.Close();
                }
                else
                    MessageBox.Show(resources.GetString("FailedToEditUserText"), resources.GetString("FailedToEditUserTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            RoutedForm.Focus();
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