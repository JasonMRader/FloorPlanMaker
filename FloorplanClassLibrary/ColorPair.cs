using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public struct ColorPair
    {
        public Color BackgroundColor { get; set; }
        public Color FontColor { get; set; }

        public ColorPair(Color backgroundColor, Color fontColor)
        {
            BackgroundColor = backgroundColor;
            FontColor = fontColor;
        }
    }

}
