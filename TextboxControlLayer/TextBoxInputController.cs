using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExamApp.TextboxControlLayer
{
    static class TextBoxInputController
    {
        public static bool IsEmpty(this TextBox txt, ErrorProvider provider = null)
        {
            if (txt.GetPlaceHolderText() != null)
            {
                if (provider != null)
                {
                    if (txt.Text.Equals(txt.GetPlaceHolderText()) || Regex.IsMatch(txt.Text, "^\\s*$"))
                    {
                        provider.SetError(txt, $"{txt.GetPlaceHolderText()} is Required!");
                        return true;
                    }
                }
                else
                {
                    if (txt.Text.Equals(txt.GetPlaceHolderText()) || Regex.IsMatch(txt.Text, "^\\s*$"))
                        return true;
                }
                return false;
            }
            else
            {
                if (provider != null)
                {
                    if (Regex.IsMatch(txt.Text, "^\\s*$"))
                    {
                        provider.SetError(txt, $"{txt.Name} is Required!");
                        return true;
                    }
                }
                else
                {
                    if (Regex.IsMatch(txt.Text, "^\\s*$"))
                        return true;
                }
                return false;
            }
        }
        public static bool IsMatch(this TextBox txt, string value)
        {
            if (txt.Text.Equals(value))
                return true;
            return false;
        }
    }
}
