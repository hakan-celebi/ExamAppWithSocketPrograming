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
    public partial class AddUserRole : Form
    {
        private IEfDataProvider DataProvider { get; set; }
        private Form RoutedForm { get; set; }
        private ComponentResourceManager resources { get; set; }
        public AddUserRole(IEfDataProvider provider)
        {
            InitializeComponent();
            DataProvider = provider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }

        private void AddUserRole_Load(object sender, EventArgs e)
        {
            RoutedForm = this.Tag as Form;
            this.Tag = null;
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnAddUserRole));
            lblHeader.Location = new Point(10, 12);
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrAddUserRole));
            lblHeader.Location = new Point(10, 12);
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            this.Text = resources.GetString("FormText");
            lblHeader.Text = resources.GetString("LabelHeader");
            txtRole.SetPlaceHolderText(resources.GetString("TextBoxRolePlaceHolderText"));
            btnAdd.Text = resources.GetString("ButtonAdd");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtErrorProvider.Clear();
            if (!txtRole.IsEmpty(txtErrorProvider))
            {
                UserRole addToUserRole = new UserRole()
                {
                    Role = (UserRole.RoleEnum)Enum.Parse(typeof(UserRole.RoleEnum), txtRole.Text)
                };
                if (DataProvider.AddUserRole(addToUserRole))
                {
                    MessageBox.Show(resources.GetString("ConfirmToAddUserRoleText"), resources.GetString("ConfirmToAddUserRoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((AdminRoleForm)RoutedForm).RefreshListView();
                }
                else
                    MessageBox.Show(resources.GetString("FailedToAddUserRoleText"), resources.GetString("FailedToAddUserRoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUserRole_FormClosing(object sender, FormClosingEventArgs e)
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
