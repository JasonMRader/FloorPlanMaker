using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public static class AppColors
    {
       

        
        public static Font MainFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        public static Font LargeFont = new Font("Segoe UI", 18f, FontStyle.Bold);

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

    }
}
