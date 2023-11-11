using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public static class AppColors
    {
        //public static Color MainColor = Color.FromArgb(49,56,82);
        ////public static Color CTAColor = Color.FromArgb(255,103,0);
        //public static Color SecondColor = Color.FromArgb(178, 87, 46);
        ////(165,64,45) (196,73,0)burned orange
        //public static Color CanvasColor = Color.FromArgb(192, 192, 192);
        //public static Color AccentColor = Color.FromArgb(75, 74, 103);

        //public static Color ButtonColor = Color.FromArgb(158, 171, 222);
        //public static Color CTAColor = Color.FromArgb(83,216,251);
        //public static Font MainFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        //public static Font LargeFont = new Font("Segoe UI", 18f, FontStyle.Bold);
        ////Light blue 46,134,171

        public static Color MainColor = Color.FromArgb(2, 43, 58);
        public static Color MainFontColor = Color.White;

        public static Color SecondColor = Color.FromArgb(13, 72, 87);
        public static Color SecondFontColor = Color.White;

        public static Color CanvasColor = Color.FromArgb(191, 219, 247);
        public static Color CanvasFontColor = Color.Black;

        public static Color AccentColor = Color.FromArgb(31, 122, 140);
        public static Color AccentFontColor = Color.Black;

        public static Color ButtonColor = Color.FromArgb(75, 179, 253);
        public static Color ButtonFontColor = Color.Black;

        public static Color CTAColor = Color.FromArgb(255, 200, 87);
        public static Color CTAFontColor = Color.Black;

        public static Font MainFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        public static Font LargeFont = new Font("Segoe UI", 18f, FontStyle.Bold);

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
