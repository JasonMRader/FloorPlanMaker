using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary
{
    public partial class SectionTabs : UserControl
    {
        public SectionTabs()
        {
            InitializeComponent();
        }
        private Floorplan floorplan { get; set; }
        public void SetFloorplanToNone()
        {
            flowLayoutPanel.Controls.Clear();
        }
        public void SetFloorplan(Floorplan floorplan)
        {
            this.floorplan = floorplan;
            int xLocation = 0;
            foreach(Section section in floorplan.Sections) {
                Button button = new Button() {
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    FlatStyle = FlatStyle.Flat,
                    Height = this.Height,
                    Width = (this.Width/floorplan.Sections.Count),
                    Location = new Point(xLocation, 0),                    
                };
                xLocation += button.Width;
                button.FlatAppearance.BorderSize = 0;
                flowLayoutPanel.Controls.Add(button);
            }
            
        }
        public static void FormatSectionButton(Button c, Section section)
        {
            
            
            
        }
    }
}
