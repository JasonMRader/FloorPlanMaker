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
        //______________********** Original
        //public static Color MainColor = Color.FromArgb(49,56,82);
        ////public static Color CTAColor = Color.FromArgb(255,103,0);
        //public static Color SecondColor = Color.FromArgb(178, 87, 46);
        ////(165,64,45) (196,73,0)burned orange
        //public static Color CanvasColor = Color.FromArgb(192, 192, 192);
        //public static Color AccentColor = Color.FromArgb(75, 74, 103);

        //public static Color ButtonColor = Color.FromArgb(158, 171, 222);
        //public static Color CTAColor = Color.FromArgb(83,216,251);        
        ////Light blue 46,134,171

        //_______________*********** BLue Teal Gold
        //public static Color MainColor = Color.FromArgb(2, 43, 58);
        //public static Color MainFontColor = Color.White;

        //public static Color SecondColor = Color.FromArgb(13, 72, 87);
        //public static Color SecondFontColor = Color.White;

        //public static Color CanvasColor = Color.FromArgb(191, 219, 247);
        //public static Color CanvasFontColor = Color.Black;

        //public static Color AccentColor = Color.FromArgb(31, 122, 140);
        //public static Color AccentFontColor = Color.Black;

        //public static Color ButtonColor = Color.FromArgb(75, 179, 253);
        //public static Color ButtonFontColor = Color.Black;

        //public static Color CTAColor = Color.FromArgb(255, 200, 87);
        //public static Color CTAFontColor = Color.Black;


        ////_______________*********** Blurple
        //public static Font MainFont = new Font("Segoe UI", 12f, FontStyle.Bold);
        //public static Font LargeFont = new Font("Segoe UI", 18f, FontStyle.Bold);

        //public static Color MainColor = Color.FromArgb(15, 1, 105);
        //public static Color MainFontColor = Color.White;

        //public static Color SecondColor = Color.FromArgb(20, 33, 74);
        //public static Color SecondFontColor = Color.White;

        //public static Color CanvasColor = Color.FromArgb(189, 213, 234);
        //public static Color CanvasFontColor = Color.White;

        //public static Color AccentColor = Color.FromArgb(74, 44, 145);
        //public static Color AccentFontColor = Color.White;

        //public static Color ButtonColor = Color.FromArgb(99, 75, 156);
        //public static Color ButtonFontColor = Color.White;

        //public static Color CTAColor = Color.FromArgb(164, 11, 230);
        //public static Color CTAFontColor = Color.White;

        
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
