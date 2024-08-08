using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public static class ColorExtentions
    {
        public static XColor ToXColor(this Color color)
        {
            return XColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
