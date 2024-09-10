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
    public partial class SectionTabs : UserControl, ISectionObserver
    {
        public SectionTabs()
        {
            InitializeComponent();
        }
        private int defaultWidth {
            get {
                if(floorplan != null && floorplan.Sections.Count > 0) {
                    return this.Width / floorplan.Sections.Count + 3;
                }
                return 0;
            }
        }
        private int selectedWidth {
            get {
                if (floorplan != null && floorplan.Sections.Count > 0) {
                    return (this.Width / floorplan.Sections.Count + 2) * 3;
                }
                return 0;
            }
        }
        private Floorplan floorplan { get; set; }
        public void SetFloorplanToNone()
        {
            foreach(var section in floorplan.Sections) {
                section.RemoveObserver(this);
            }
            this.floorplan = null;
            flowLayoutPanel.Controls.Clear();
        }
        public void SetFloorplan(Floorplan floorplan)
        {
            if(this.floorplan != null) {
                foreach (var section in this.floorplan.Sections) {
                    section.RemoveObserver(this);
                }
            }
            this.floorplan = floorplan;
            int xLocation = 0;
            flowLayoutPanel.Controls.Clear();
            foreach(Section section in floorplan.Sections) {
                Button button = new Button() {
                    BackColor = section.Color,
                    ForeColor = section.FontColor,
                    FlatStyle = FlatStyle.Flat,
                    Height = this.Height,
                    Width = defaultWidth,
                    Location = new Point(xLocation, 0),  
                    Margin = new Padding(0),
                    Tag = section
                };
                section.SubscribeObserver(this);
                xLocation += button.Width;
                button.FlatAppearance.BorderSize = 0;
                if(section.IsSelected) {
                    button.Width = selectedWidth;
                }
                flowLayoutPanel.Controls.Add(button);
            }
            
        }
        public static void FormatSectionButton(Button c, Section section)
        {
            
            
            
        }

        public void UpdateSection(Section section)
        {
            foreach(Button button in flowLayoutPanel.Controls) {
                Section sectionTag = button.Tag as Section;
                if (sectionTag.IsSelected) {
                    button.Width = selectedWidth;
                }
                else {
                    button.Width = defaultWidth;
                }
            }
            
        }
    }
}
