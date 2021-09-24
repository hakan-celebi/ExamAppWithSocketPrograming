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

namespace ExamApp
{
    public partial class RegisterForm : Form
    {
        private ComponentResourceManager resources = null;
        private IEfDataProvider DataProvider { get; set; }
        private LoginForm RoutedFrom { get; set; }
        public RegisterForm(IEfDataProvider dataProvider)
        {
            InitializeComponent();
            DataProvider = dataProvider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            RoutedFrom = this.Tag as LoginForm;
            this.Tag = null;
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnRegisterForm));
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrRegisterForm));
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            txtErrorProvider.Clear();
            this.Text = resources.GetString("FormText");
            txtName.SetPlaceHolderText(resources.GetString("NameTextBoxPlaceHolderText"), Color.DarkBlue);
            txtSurname.SetPlaceHolderText(resources.GetString("SurnameTextBoxPlaceHolderText"), Color.DarkBlue);
            txtEMail.SetPlaceHolderText(resources.GetString("EMailTextBoxPlaceHolderText"), Color.DarkBlue);
            txtPassword.SetPlaceHolderText(resources.GetString("PasswordTextBoxPlaceHolderText"), Color.DarkGoldenrod);
            lblHeader.Text = resources.GetString("HeaderLabel");
            btnRegister.Text = resources.GetString("RegisterButton");
            linkLblLogin.Text = resources.GetString("LoginLinkLabel");
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

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RoutedFrom.Show();
        }

        private void linkLblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RoutedFrom.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            txtErrorProvider.Clear();
            if (!txtName.IsEmpty(txtErrorProvider) & !txtSurname.IsEmpty(txtErrorProvider) & !txtEMail.IsEmpty(txtErrorProvider) & !txtPassword.IsEmpty(txtErrorProvider))
            {
                UserRole role = (UserRole)DataProvider.FindUserRoleByRole(UserRole.RoleEnum.student);
                User addToUser = new User()
                {
                    Name = txtName.Text,
                    Surname = txtSurname.Text,
                    EMail = txtEMail.Text,
                    Password = txtPassword.Text,
                    UserRoleID = role.Id,
                    UserRole = role
                };
                if (DataProvider.AddUser(addToUser))
                {
                    MessageBox.Show("Registration Successful!", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RoutedFrom.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Registration Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
