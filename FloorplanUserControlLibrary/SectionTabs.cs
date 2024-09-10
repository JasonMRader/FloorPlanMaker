using FloorplanClassLibrary;
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

namespace FloorplanUserControlLibrary
{
    public partial class SectionTabs : UserControl, ISectionObserver
    {
        public SectionTabs()
        {
            InitializeComponent();
        }
        List<Button> buttons = new List<Button>();
        private int defaultWidth {
            get {
                if (floorplan != null && floorplan.Sections.Count > 7) {
                    return (int)((this.Width * .75) / (floorplan.Sections.Count - 1));
                }
                else if (floorplan != null && floorplan.Sections.Count > 5) {
                    return (int)((this.Width * .55) / (floorplan.Sections.Count - 1));
                }
                else if (floorplan != null && floorplan.Sections.Count > 3) {
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
                if (floorplan != null && floorplan.Sections.Count > 7) {
                    return (int)(this.Width * .20);
                }
                else if (floorplan != null && floorplan.Sections.Count > 5) {
                    return (int)(this.Width * .25);
                }
                else if (floorplan != null && floorplan.Sections.Count > 3) {
                    return (int)(this.Width  * .3);
                }
                else if(floorplan != null && floorplan.Sections.Count > 0) {
                    return (int)(this.Width * .4);
                }
                return 0;
            }
        }
        private Floorplan floorplan { get; set; }
        public event Action<Section> SectionSelected;
        public void SetFloorplanToNone()
        {
            if(floorplan != null) {
                foreach (var section in floorplan.Sections) {
                    section.RemoveObserver(this);
                    this.floorplan.SectionRemoved -= RemoveSection;
                    this.floorplan.SectionAdded -= AddSection;
                }
                this.floorplan = null;
            }
            buttons.Clear();
            flowLayoutPanel.Controls.Clear();
        }
        public void SetFloorplan(Floorplan floorplan)
        {
            SetFloorplanToNone();
           
            this.floorplan = floorplan;
            floorplan.SectionRemoved += RemoveSection;
            floorplan.SectionAdded += AddSection;           
           
            foreach(Section section in floorplan.Sections) {
               CreateButtonForSection(section);
            }
            
        }
        private void CreateButtonForSection(Section section)
        {
            Button button = new Button() {
                BackColor = section.Color,
                ForeColor = section.FontColor,
                FlatStyle = FlatStyle.Flat,
                Height = this.Height - 15,
                Width = defaultWidth,
                Margin = new Padding(0, 15, 0, 0),
                Tag = section
            };
            section.SubscribeObserver(this);
            button.Click += Button_Click;

            button.FlatAppearance.BorderSize = 0;
            if (section.IsSelected) {
                button.Width = selectedWidth;
                button.Height = this.Height;
                button.Margin = new Padding(0);
            }
            flowLayoutPanel.Controls.Add(button);
            buttons.Add(button);
        }
        private void AddSection(Section section, Floorplan arg2)
        {
            
            CreateButtonForSection(section);
        }

        private void RemoveSection(Section section, Floorplan arg2)
        {
            Button button = buttons.FirstOrDefault(b => b.Tag == section);
            if (button != null) {
                section.RemoveObserver(this);
                flowLayoutPanel.Controls.Remove(button);
                buttons.Remove(button);
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
                    button.Height = this.Height - 15;
                    button.Margin = new Padding(0,15,0,0);
                }
            }
            
        }
    }
}
