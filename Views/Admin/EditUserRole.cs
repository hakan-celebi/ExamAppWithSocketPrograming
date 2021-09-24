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
    public partial class EditUserRole : Form
    {
        private IEfDataProvider DataProvider { get; set; }
        private Form RoutedForm { get; set; }
        public UserRole EditToUserRole { get; set; }
        private ComponentResourceManager resources { get; set; }
        public EditUserRole(IEfDataProvider provider)
        {
            InitializeComponent();
            DataProvider = provider;
            cmbLanguage.SelectedIndex = Convert.ToByte((SignInManager.Language));
        }

        private void EditUserRole_Load(object sender, EventArgs e)
        {
            RoutedForm = this.Tag as Form;
            this.Tag = null;
            if(!(EditToUserRole is null))
            {
                txtRole.Text = EditToUserRole.Role.ToString();
            }
            else
                MessageBox.Show(resources.GetString("CouldntFindUserRoleText"), resources.GetString("CouldntFindUserRoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SetLanguageToEnglish()
        {
            resources = new ComponentResourceManager(typeof(EnEditUserRole));
            lblHeader.Location = new Point(10, 12);
            ChangeLanguage();
        }

        private void SetLanguageToTurkish()
        {
            resources = new ComponentResourceManager(typeof(TrEditUserRole));
            lblHeader.Location = new Point(10, 12);
            ChangeLanguage();
        }
        private void ChangeLanguage()
        {
            this.Text = resources.GetString("FormText");
            lblHeader.Text = resources.GetString("LabelHeader");
            txtRole.SetPlaceHolderText(resources.GetString("TextBoxRolePlaceHolderText"));
            btnSave.Text = resources.GetString("ButtonSave");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!txtRole.IsEmpty(txtErrorProvider))
            {
                EditToUserRole.Role = (UserRole.RoleEnum)Enum.Parse(typeof(UserRole.RoleEnum), txtRole.Text);
                if (DataProvider.UpdateUser(EditToUserRole))
                {
                    MessageBox.Show(resources.GetString("ConfirmToEditUserRoleText"), resources.GetString("ConfirmToEditUserRoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((AdminRoleForm)RoutedForm).RefreshListView();
                    this.Close();
                }
                else
                    MessageBox.Show(resources.GetString("FailedToEditUserRoleText"), resources.GetString("FailedToEditUserRoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditUserRole_FormClosing(object sender, FormClosingEventArgs e)
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
