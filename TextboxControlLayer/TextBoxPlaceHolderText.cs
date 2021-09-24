using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExamApp.TextboxControlLayer
{
    static public class TextBoxPlaceHolderText
    {
        public static Color TextBoxForeColor { get; set; }
        private static List<string[]> TextBoxInformation { get; set; } = new List<string[]>();
        public static void SetPlaceHolderText(this TextBox txt, string PlaceHolderText, Color textBoxForeColor = default, bool PasswordChar = false)
        {
            if (!(txt.GetPlaceHolderText() is null))
                txt.RemovePlaceHolderText();
            TextBoxForeColor = textBoxForeColor;
            string[] information = new string[5];
            information[0] = txt.Name;
            information[1] = txt.FindForm().Name;
            information[2] = PlaceHolderText;
            information[3] = TextBoxForeColor.Name;
            information[4] = PasswordChar.ToString();
            TextBoxInformation.Add(information);
            txt.Click += textbox_Click;
            txt.KeyPress += textbox_KeyPress;
            txt.Leave += textbox_Leave;
            txt.Text = PlaceHolderText;
            txt.ForeColor = Color.Gray;
        }
        public static void RemovePlaceHolderText(this TextBox txt)
        {
            if (!(txt.GetPlaceHolderText() is null))
            {
                foreach (string[] item in TextBoxInformation)
                {
                    if (item[0] == txt.Name && item[1] == txt.FindForm().Name)
                    {
                        TextBoxInformation.Remove(item);
                        break;
                    }
                }
                txt.Click -= textbox_Click;
                txt.KeyPress -= textbox_KeyPress;
                txt.Leave -= textbox_Leave;
            }
        }
        public static string GetPlaceHolderText(this TextBox txt)
        {
            foreach (string[] item in TextBoxInformation)
            {
                if (item[0] == txt.Name && item[1] == txt.FindForm().Name)
                    return item[2];
            }
            return null;
        }
        private static void textbox_Click(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string placeHolderText = GetPlaceHolderText(txt);
            if (txt.Text.Contains(placeHolderText))
                txt.SelectionStart = 0;
        }
        private static void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            string textboxText = default;
            bool textboxPasswordChar = default;
            foreach (string[] item in TextBoxInformation)
            {
                if (item[0] == txt.Name && item[1] == txt.FindForm().Name)
                {
                    textboxText = item[2];
                    TextBoxForeColor = Color.FromName(item[3]);
                    textboxPasswordChar = bool.Parse(item[4]);
                    break;
                }
            }
            if (txt.Text.Contains(textboxText))
            {
                txt.Clear();
                if (textboxPasswordChar)
                {
                    txt.UseSystemPasswordChar = true;
                    if (Char.IsLetterOrDigit(e.KeyChar))
                        txt.Text = e.KeyChar.ToString();
                }
                txt.ForeColor = TextBoxForeColor;
                txt.SelectionStart = txt.SelectionLength + 1;
            }
            else if (txt.Text.Equals(string.Empty) && char.IsControl(e.KeyChar))
            {
                if (Control.ModifierKeys == Keys.Control)
                    return;
                txt.Text = textboxText;
                txt.ForeColor = Color.Gray;
                txt.UseSystemPasswordChar = false;
                txt.SelectionStart = 0;
            }
        }
        private static void textbox_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text.Equals(string.Empty))
            {
                string textboxText = GetPlaceHolderText(txt);
                txt.Text = textboxText;
                txt.ForeColor = Color.Gray;
                txt.UseSystemPasswordChar = false;
            }
        }
    }
}
