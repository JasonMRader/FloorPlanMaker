using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary {
    public partial class CustomTooltip : UserControl {
        public CustomTooltip(Control control, int Width, int Height, string text, string hotKey) {
            InitializeComponent();
            this.Width = Width;
            this.Height = Height;
            int x = (control.Location.X - (this.Width / 2) + control.Width / 2);
            int y = (control.Location.Y - (this.Height / 2) + control.Height / 2);
            this.Location = new Point(x, y);
            this.text = text;
            this.hotKey = hotKey;
            if (hotKey != "" ) {
            lblDescription.Text = $"[{hotKey}]\n{text}";
            }
            else {
                lblDescription.Text = text;
            }
            
        }
        private string text { get; set; }
        private string hotKey { get; set; } = string.Empty;
    }
}
