
using FloorplanUserControlLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public static class UITheme
    {
       

        
        public static Font MainFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        public static Font LargeFont = new Font("Segoe UI", 18f, FontStyle.Bold);
        public static Font SmallerFont = new Font("Segoe UI", 10f, FontStyle.Bold);

        public static Color MainColor = Color.FromArgb(245,245,245);
        public static Color MainFontColor = Color.Black;

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
        public static Color HighLightFontColor = Color.White;

        public static Color HighlightColor2 = Color.FromArgb(237, 174, 73);

        public static Color WarningColor = Color.FromArgb(169, 135, 67);

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
        public static void FormatMainButton(Control c)
        {
            c.BackColor = ButtonColor;
            c.ForeColor = ButtonFontColor;
        }
        public static void FormatCTAButton(Control c)
        {
            c.BackColor = CTAColor;
            c.ForeColor = CTAFontColor;
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

    }
}
