
using FloorplanClassLibrary;
using FloorplanUserControlLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public static class UITheme
    {
       

        
        public static Font MainFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        public static Font LargeFont = new Font("Segoe UI", 18f, FontStyle.Bold);
        public static Font SmallerFont = new Font("Segoe UI", 10f, FontStyle.Bold);
        public static Font CustomFont(float size, FontStyle fontStyle)
        {
            Font result = new Font("Segoe UI", size, fontStyle);
            return result;
        }
        

        public static Color MainColor = Color.FromArgb(245,245,245);
        public static Color MainFontColor = Color.Black;

        public static Color SelectedColor = Color.FromArgb(255, 103, 0);

        public static Color SecondColor = Color.FromArgb(180,190,200);
        public static Color SecondFontColor = Color.Black;

        public static Color CanvasColor = Color.FromArgb(245, 245, 245);
        public static Color CanvasFontColor = Color.Black;

        public static Color AccentColor = Color.FromArgb(225, 225, 225);
        public static Color AccentFontColor = Color.White;

        public static Color ButtonColor = Color.FromArgb(160,160,160);
        public static Color ButtonFontColor = Color.White;

        public static Color CTAColor = Color.FromArgb(100,130,180);
        public static Color CTAFontColor = Color.White;

        public static Color YesColor = Color.FromArgb(120, 180, 120);
        public static Color YesFontColor = Color.White;

        public static Color NoColor = Color.FromArgb(190, 80, 70);
        public static Color NoFontColor = Color.White;

        public static Color CautionColor = Color.FromArgb(254,185,95);
        public static Color CautionFontColor = Color.White;

        public static Color HighlightColor = Color.FromArgb(242,143,59);
        public static Color HighlightFontColor = Color.White;

        public static Color HighlightColor2 = Color.FromArgb(237, 174, 73);

        public static Color WarningColor = Color.FromArgb(169, 135, 67);
        public static void FormatTempLabelColor(Control ctl, int temp)
        {
            if (temp >= 90)
            {
                ctl.BackColor = Color.FromArgb(245, 59, 59);
                ctl.ForeColor = Color.Black;
            }
            else if (temp >= 80)
            {
                ctl.BackColor = Color.FromArgb(255, 213, 18);
                ctl.ForeColor = Color.Black;
            }
            else if (temp >= 70)
            {
                ctl.BackColor = Color.FromArgb(239, 242, 177);
                ctl.ForeColor = Color.Black;
            }
            else if (temp >= 60)
            {
                ctl.BackColor = Color.FromArgb(161, 237, 222);
                ctl.ForeColor = Color.Black;
            }
            else if (temp >= 50)
            {
                ctl.BackColor = Color.FromArgb(75, 220, 227);
                ctl.ForeColor = Color.Black;
            }
            else
            {
                ctl.BackColor = Color.FromArgb(77, 147, 221);
                ctl.ForeColor = Color.Black;
            }
            if(ctl is  Label label)
            {
                label.Text = temp.ToString() + "°";
            }
            //ctl.Text = 
             
            
        }
       
        public static void FormateWindLabel(Control ctl, int wind)
        {
            if (wind >= 30)
            {
                ctl.BackColor = Color.FromArgb(229, 31, 31);
                ctl.ForeColor = Color.Black;
            }
            else if (wind >= 25)
            {
                ctl.BackColor = Color.FromArgb(242, 161, 52);
                ctl.ForeColor = Color.Black;
            }
            else if (wind >= 15)
            {
                ctl.BackColor = Color.FromArgb(247, 227, 121);
                ctl.ForeColor = Color.Black;
            }
            else if (wind >= 10)
            {
                ctl.BackColor = Color.FromArgb(187, 219, 68);
                ctl.ForeColor = Color.Black;
            }           
            else
            {
                ctl.BackColor = Color.FromArgb(68, 206, 27);
                ctl.ForeColor = Color.Black;
            }
            if(ctl is Label label)
            {
                label.Text = wind.ToString();
            }
            
        }
        public static void FormatePrecipChanceLabel(Control ctl, int precipChance)
        {
            if (precipChance >= 95)
            {
                ctl.BackColor = Color.FromArgb(229, 31, 31);
                ctl.ForeColor = Color.Black;
                if(ctl is PictureBox pictureBox)
                {
                    pictureBox.Image = Resources.rain_23px;
                }
            }
            else if (precipChance >= 75)
            {
                ctl.BackColor = Color.FromArgb(242, 161, 52);
                ctl.ForeColor = Color.Black;
                if (ctl is PictureBox pictureBox)
                {
                    pictureBox.Image = Resources.rain_23px;
                }

                
            }
            else if (precipChance >= 55)
            {
                ctl.BackColor = Color.FromArgb(247, 227, 121);
                ctl.ForeColor = Color.Black;
                
                if (ctl is PictureBox pictureBox)
                {
                    pictureBox.Image = Resources.rain_23px;
                }
            }
            else if (precipChance >= 30)
            {
                ctl.BackColor = Color.FromArgb(187, 219, 68);
                ctl.ForeColor = Color.Black;
                if (ctl is PictureBox pictureBox)
                {
                    pictureBox.Image = Resources.rain_23px;
                }
            }
            else
            {
                ctl.BackColor = Color.FromArgb(68, 206, 27);
                ctl.ForeColor = Color.Black;
                if (ctl is PictureBox pictureBox)
                {
                    pictureBox.Image = null;
                }
            }
            if (ctl is Label label)
            {
                label.Text = precipChance.ToString() + "%";
            }
            
        }
        public static void FormatePrecipAmountLabel(Label label, float precipAmount)
        {
            if (precipAmount >= 1f)
            {
                label.BackColor = Color.FromArgb(229, 31, 31);
                label.ForeColor = Color.Black;
            }
            else if (precipAmount >= .5f)
            {
                label.BackColor = Color.FromArgb(242, 161, 52);
                label.ForeColor = Color.Black;
            }
            else if (precipAmount >= .2f)
            {
                label.BackColor = Color.FromArgb(247, 227, 121);
                label.ForeColor = Color.Black;
            }
            else if (precipAmount > 0f)
            {
                label.BackColor = Color.FromArgb(187, 219, 68);
                label.ForeColor = Color.Black;
            }
            else
            {
                label.BackColor = Color.FromArgb(68, 206, 27);
                label.ForeColor = Color.Black;
            }
            label.Text = precipAmount.ToString("f2") + "\"";
        }

        public static Image erase = Resources.erase;
        public static Image save = Resources.ExtraSmallSave;
        public static Image inside = Resources.Inside;
        public static Image Sun = Resources.noun_sun_3805502;
        public static Image sales = Resources.sales;
        public static Image waiter = Resources.waiter;
        public static Image tray = Resources.trey;
        public static Image covers = Resources.Chair;

        //242,193,78---255,188,66---242,193,78
        public  static Color MuteColor(float amount, Color Color)
        {
            // 'amount' is a value between 0 and 1, where 0 is completely grey and 1 is the original color

            // Calculate the greyscale value of the original color
            float greyScale = (Color.R * 0.3f + Color.G * 0.59f +   Color.B * 0.11f) / 255;

            // Interpolate between the greyscale and the original color
            int muteR = (int)(Color.R * amount + greyScale * (1 - amount) * 255);
            int muteG = (int)(Color.G * amount + greyScale * (1 - amount) * 255);
            int muteB = (int)(Color.B * amount + greyScale * (1 - amount) * 255);

            // Ensure the RGB values are within the valid range
            muteR = Math.Min(255, Math.Max(0, muteR));
            muteG = Math.Min(255, Math.Max(0, muteG));
            muteB = Math.Min(255, Math.Max(0, muteB));

            return Color.FromArgb(Color.A, muteR, muteG, muteB);
        }
        public static Color DarkenColor(float percentage, Color color)
        {
            // Ensure the percentage is between 0 and 1
            percentage = Math.Clamp(percentage, 0, 1);

            int r = (int)(color.R * (1 - percentage));
            int g = (int)(color.G * (1 - percentage));
            int b = (int)(color.B * (1 - percentage));

            return Color.FromArgb(color.A, r, g, b);
        }
        public static void FormatMainButton(Control c)
        {
            c.BackColor = ButtonColor;
            c.ForeColor = ButtonFontColor;
            if (c is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
            }
        }
        public static void FormatCTAButton(Control c)
        {
            c.BackColor = CTAColor;
            c.ForeColor = CTAFontColor;
            if(c is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
            }
            if (c is RadioButton rdobutton)
            {
                rdobutton.Appearance = Appearance.Button;
                rdobutton.FlatStyle = FlatStyle.Flat;
                rdobutton.FlatAppearance.BorderSize = 0;
               
            }
        }
        public static void FormatMain(Control c)
        {
            c.BackColor = MainColor;
            c.ForeColor = MainFontColor;
        }
        public static void FormatSecondColor(Control c)
        {
            c.BackColor = SecondColor;
            c.ForeColor = SecondFontColor;
        }
        public static void FormatAccentColor(Control c)
        {
            c.BackColor = AccentColor;
            c.ForeColor = AccentFontColor;
        }
        public static void FormatCanvasColor(Control c)
        {
            c.BackColor = CanvasColor;
            c.ForeColor = Color.Black;
        }
        public static PictureBox GetPictureBox(Image img, int width, int height)
        {
            PictureBox pictureBox = new PictureBox
            {
                //Image = img,
                Width = width,
                Height = height,
                SizeMode = PictureBoxSizeMode.Zoom
            };


            return pictureBox;
        }
        public static Color GetDiningAreaColor(DiningArea area)
        {
            if (area.ID == 1) {
                return Color.Red;
            }
            if (area.ID == 2) {
                return Color.Blue;
            }
            if (area.ID == 3) {
                return Color.Green;
            }
            if (area.ID == 4) {
                return Color.Gray;
            }
            if (area.ID == 5) {
                return Color.DarkOrange;
            }
            if (area.ID == 6) {
                return Color.Black;
            }
            else {
                return Color.White;
            }
        }
        public static Image GetDiningAreaImage(DiningArea area)
        {
            if (area.ID == 1)
            {
                return Resources.InsideDining;
            }
            if (area.ID == 2)
            {
                return Resources.OutsideDining;
            }
            if (area.ID == 3)
            {
                return Resources.OutsideCocktail;
            }
            if (area.ID == 4)
            {
                return Resources.Inside_Bar;
            }
            if (area.ID == 5)
            {
                return Resources.UpperLevel;
            }
            if (area.ID == 6)
            {
                return Resources.Banquet;
            }
            else
            {
                return Resources.Temporary;
            }
        }

    }
}
