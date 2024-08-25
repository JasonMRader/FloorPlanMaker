using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary {
    public class PreviewTooltip : ToolTip {
        public PreviewTooltip(IContainer container) : base(container) {
            // Customize your tooltip here, such as setting initial properties
            this.OwnerDraw = true;
            this.Draw += new DrawToolTipEventHandler(CustomTooltip_Draw);
        }

        public void SetTooltip(Control control, string text, string hotKey = "") {
            string displayText = string.IsNullOrEmpty(hotKey) ? text : $"[{hotKey}]\n{text}";
            this.SetToolTip(control, displayText);
        }

        private void CustomTooltip_Draw(object sender, DrawToolTipEventArgs e) {
            // Customize the drawing of the tooltip here
            e.DrawBackground();
            e.DrawBorder();
            using (Font font = new Font("Arial", 10, FontStyle.Bold)) {
                e.Graphics.DrawString(e.ToolTipText, font, Brushes.Black, new PointF(2, 2));
            }
        }

        public void ShowAllTooltips(Form form) {
            foreach (Control control in form.Controls) {
                string tooltipText = this.GetToolTip(control);
                if (!string.IsNullOrEmpty(tooltipText)) {
                    this.Show(tooltipText, control, control.Width / 2, control.Height / 2);
                }
            }
        }
    }

}
