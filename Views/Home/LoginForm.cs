using ExamApp.DataProviders;
using ExamApp.Entity;
using ExamApp.Managers;
using ExamApp.Models;
using ExamApp.Resources;
using ExamApp.TextboxControlLayer;
using ExamApp.Views.Admin;
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
    public partial class LoginForm : Form
    {
        private ComponentResourceManager resources = null;
        private IEfDataProvider DataProvider { get; set; }

        public LoginForm(IEfDataProvider dataProvider)
        {
            InitializeComponent();
            JsonDataProvider provider = new JsonDataProvider();
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
            DataProvider = dataProvider;
        }

        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnLoginForm));
            lblHeader.Location = new Point(64, 9);
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrLoginForm));
            lblHeader.Location = new Point(5, 9);
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            txtErrorProvider.Clear();
            this.Text = resources.GetString("FormText");
            txtUserName.SetPlaceHolderText(resources.GetString("UsernameTextBoxPlaceHolderText"), Color.DarkBlue);
            txtPassword.SetPlaceHolderText(resources.GetString("PasswordTextBoxPlaceHolderText"), Color.DarkGoldenrod);
            txtUserName.Focus();
            lblHeader.Text = resources.GetString("HeaderLabel");
            btnLogin.Text = resources.GetString("LoginButton");
            linkLblRegister.Text = resources.GetString("RegisterLinkLabel");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtErrorProvider.Clear();
            if (!txtUserName.IsEmpty(txtErrorProvider) & !txtPassword.IsEmpty(txtErrorProvider))
            {
                User user = DataProvider.FindUserByEMail(txtUserName.Text) as User;
                if (user != null)
                {
                    if(txtPassword.IsMatch(user.Password))
                    {
                        if (user.Login())
                        {
                            switch (SignInManager.LoggedUser.UserRole.Role)
                            {
                                case UserRole.RoleEnum.admin:
                                    ChangeLanguage();
                                    AdminUserForm adminForm = new AdminUserForm(DataProvider);
                                    adminForm.Tag = this;
                                    this.Owner = adminForm;
                                    this.Hide();
                                    adminForm.Show();
                                    break;
                                case UserRole.RoleEnum.academician:
                                    ChangeLanguage();
                                    AcademicianForm academicianForm = new AcademicianForm(DataProvider);
                                    academicianForm.Tag = this;
                                    this.Owner = academicianForm;
                                    this.Hide();
                                    academicianForm.Show();
                                    break;
                                case UserRole.RoleEnum.student:
                                    ChangeLanguage();
                                    StudentForm studentForm = new StudentForm(DataProvider);
                                    studentForm.Tag = this;
                                    this.Owner = studentForm;
                                    this.Hide();
                                    studentForm.Show();
                                    break;
                                default:
                                    MessageBox.Show("Invalid Role!", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                        else
                            MessageBox.Show("Couldn't Login!", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        txtErrorProvider.SetError(txtPassword, "Password is wrong!");
                }
                else
                    txtErrorProvider.SetError(txtUserName, "Username is wrong!");
            }
        }

        private void linkLblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(DataProvider);
            registerForm.Tag = this;
            this.Hide();
            registerForm.Show();
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