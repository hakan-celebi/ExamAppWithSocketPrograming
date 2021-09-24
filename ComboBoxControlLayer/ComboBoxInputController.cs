using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamApp.ComboBoxControlLayer
{
    static class ComboBoxInputController
    {
        public static bool IsEmpty(this ComboBox cmb, ErrorProvider provider = null)
        {
            if (cmb.GetPlaceHolderText() != null)
            {
                if (provider != null)
                {
                    if (cmb.SelectedItem.ToString().Equals(cmb.GetPlaceHolderText()) || Regex.IsMatch(cmb.SelectedItem.ToString(), "^\\s*$"))
                    {
                        provider.SetError(cmb, $"{cmb.GetPlaceHolderText()} is Required!");
                        return true;
                    }
                }
                else
                {
                    if (cmb.SelectedItem.ToString().Equals(cmb.GetPlaceHolderText()) || Regex.IsMatch(cmb.SelectedItem.ToString(), "^\\s*$"))
                        return true;
                }
                return false;
            }
            else
            {
                if (provider != null)
                {
                    if (Regex.IsMatch(cmb.SelectedItem.ToString(), "^\\s*$"))
                    {
                        provider.SetError(cmb, $"{cmb.Name} is Required!");
                        return true;
                    }
                }
                else
                {
                    if (Regex.IsMatch(cmb.SelectedItem.ToString(), "^\\s*$"))
                        return true;
                }
                return false;
            }
        }
    }
}
