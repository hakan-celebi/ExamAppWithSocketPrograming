using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.ComboBoxControlLayer
{
    static class ComboBoxPlaceHolderText
    {
        private static List<string[]> ComboBoxInformation { get; set; } = new List<string[]>();
        public static void SetPlaceHolderText(this ComboBox cmb, string PlaceHolderText)
        {
            if (!(cmb.GetPlaceHolderText() is null))
                cmb.RemovePlaceHolderText();
            string[] information = new string[3];
            information[0] = cmb.Name;
            information[1] = cmb.FindForm().Name;
            information[2] = PlaceHolderText;
            ComboBoxInformation.Add(information);
            cmb.Items.Insert(0, PlaceHolderText);
            cmb.SelectedIndex = 0;
            cmb.Click += combobox_Click;
            cmb.DropDownClosed += combobox_DropDownClosed;
        }
        public static void RemovePlaceHolderText(this ComboBox cmb)
        {
            if (!(cmb.GetPlaceHolderText() is null))
            {
                foreach (string[] item in ComboBoxInformation)
                {
                    if (item[0] == cmb.Name && item[1] == cmb.FindForm().Name)
                    {
                        ComboBoxInformation.Remove(item);
                        if (cmb.Items.Count > 0 && cmb.Items[0].ToString().Equals(item[2]))
                            cmb.Items.RemoveAt(0);
                        break;
                    }
                }
                cmb.Click -= combobox_Click;
                cmb.DropDownClosed -= combobox_DropDownClosed;
            }
        }
        public static string GetPlaceHolderText(this ComboBox cmb)
        {
            foreach (string[] item in ComboBoxInformation)
            {
                if (item[0] == cmb.Name && item[1] == cmb.FindForm().Name)
                    return item[2];
            }
            return null;
        }
        private static void combobox_Click(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            string cmbPlaceHolderText = GetPlaceHolderText(cmb);
            if (cmb.Items.Count > 0 && cmb.Items[0].ToString() == cmbPlaceHolderText)
                cmb.Items.Remove(cmbPlaceHolderText);
        }
        private static void combobox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            string cmbPlaceHolderText = GetPlaceHolderText(cmb);
            if (cmb.SelectedItem is null)
                cmb.SetPlaceHolderText(cmbPlaceHolderText);
            else
                cmb.RemovePlaceHolderText();
        }


    }
}