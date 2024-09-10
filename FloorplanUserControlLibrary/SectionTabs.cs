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
                if (floorplan != null && floorplan.Sections.Count > 3) {
                    return (int)((this.Width * .4)/(floorplan.Sections.Count - 1));
                }
                else if (floorplan != null && floorplan.Sections.Count > 0) {
                    return (int)((this.Width * .2) / (floorplan.Sections.Count - 1));
                }
                
                return 0;
            }
        }
        private int selectedWidth {
            get {
                if (floorplan != null && floorplan.Sections.Count > 3) {
                    return (int)(this.Width  * .6);
                }
                else if(floorplan != null && floorplan.Sections.Count > 0) {
                    return (int)(this.Width * .8);
                }
                return 0;
            }
        }
        private Floorplan floorplan { get; set; }
        public event Action<Section> SectionSelected;
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
                    Height = this.Height-7,
                    Width = defaultWidth,                    
                    Margin = new Padding(0, 7, 0, 0),                
                    Tag = section
                };
                section.SubscribeObserver(this);
                button.Click += Button_Click;
                
                button.FlatAppearance.BorderSize = 0;
                if(section.IsSelected) {
                    button.Width = selectedWidth;
                    button.Height = this.Height;
                    button.Margin = new Padding(0);
                }
                flowLayoutPanel.Controls.Add(button);
            }
            
        }

        private void Button_Click(object? sender, EventArgs e)
        {
            Button button = (Button)sender;
            Section section = button.Tag as Section;
            if(section != null) {
                floorplan.SetSelectedSection(section);
                //SectionSelected?.Invoke(section);
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
                    button.Height = this.Height;
                    button.Margin = new Padding(0);
                }
                else {
                    button.Width = defaultWidth;
                    button.Height = this.Height - 7;
                    button.Margin = new Padding(0,7,0,0);
                }
            }
            
        }
    }
}
