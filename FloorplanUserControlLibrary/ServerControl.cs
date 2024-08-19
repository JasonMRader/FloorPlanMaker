using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMaker
{
    public class ServerControl : FlowLayoutPanel, ISectionObserver
    {
        public ServerControl(Server server, int height)
        {
            this.Server = server;

            this.Height = height *2 ;
            this.Width = 300;
            this.BackColor = UITheme.AccentColor;
            this.AutoSize = true;
            this.MaximumSize = new Size(300, height * 10);
            this.Padding = new Padding(0,0,0,0);
            this.Margin = new Padding(0,4,0,0);
            NamePanel = new Panel()
            {
                Height = height,
                Width = 300,
                Margin = new Padding(0,0,0,0)
            };
            Label = new Label
            {                
                Text = Server.ToString(),
                AutoSize = false,
                Height = height,
                Width = 300,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black,
                Margin = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.NamePanel.Controls.Add(Label);
            this.Controls.Add(NamePanel);
            this.Label.Click += (sender, e) => this.OnClick(e);
            this.TabStop = false;
            if (this.Server.isDouble)
            {
                Label.Text = Server.ToString() + " (Dbl)";
            }
            
           
            DisplayShifts();
           
        }
        public ServerControl(Server server, int height, List<Section> sections)
        {
            this.Server = server;

            this.Height = height * 2;
            this.Width = 281;
            this.BackColor = UITheme.AccentColor;
            this.AutoSize = true;
            this.MaximumSize = new Size(281, height * 10);
            this.Padding = new Padding(0, 0, 0, 0);
            this.Margin = new Padding(0, 4, 0, 0);
            NamePanel = new Panel()
            {
                Height = height,
                Width = 300,
                Margin = new Padding(0, 0, 0, 0)
            };
            Label = new Label
            {
                Text = Server.ToString(),
                AutoSize = false,
                Height = height,
                Width = 281,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black,
                Margin = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.NamePanel.Controls.Add(Label);
            this.Controls.Add(NamePanel);
            this.Label.Click += (sender, e) => this.OnClick(e);
            this.TabStop = false;
            if (this.Server.isDouble)
            {
                Label.Text = Server.ToString() + " (Dbl)";
            }

            subscribeToSectionEvents(sections);
            DisplayShifts();

        }
        private void subscribeToSectionEvents(List<Section> sections)
        {
            foreach (Section section in sections)
            {
                section.ServerAssigned += OnServerAssignedToSection;
                section.ServerRemoved += OnServerRemovedFromSection;
            }
        }
        public Label lblOutsidePercentage = new Label();
        public Panel NamePanel { get; set; }
        private Server _server;
        public Server Server
        {
            get => _server;
            set
            {
                if (_server != value)
                {
                    if (_server != null && _server.CurrentSection != null)
                    {
                        
                        _server.CurrentSection.ServerAssigned -= OnServerAssignedToSection;
                        _server.CurrentSection.ServerRemoved -= OnServerRemovedFromSection;
                    }

                    _server = value;

                    if (_server != null && _server.CurrentSection != null)
                    {
                       
                        _server.CurrentSection.ServerAssigned += OnServerAssignedToSection;
                        _server.CurrentSection.ServerRemoved += OnServerRemovedFromSection;
                    }

                  
                }
            }
        }

        private void OnServerCurrentSectionChanged(Section section)
        {
            if (this.Server.CurrentSection == null)
            {
                this.Section = null;
                this.Label.BackColor = UITheme.ButtonColor;
            }
            else
            {
                this.Section = section;
                this.Label.BackColor = section.Color;
            }
        }
        private void OnServerAssignedToSection(Server server, Section section)
        {
            
            if (server == this.Server)
            {
                this.Section = section;
                this.UpdateSection(section);
            }
        }

        private void OnServerRemovedFromSection(Server server, Section section)
        {
            
            if (server == this.Server)
            {
                this.Section = null;
                this.Label.BackColor = UITheme.ButtonColor;
                this.Label.ForeColor = Color.Black;
            }
        }
       
        public FlowLayoutPanel ShiftsDisplay { get; set; }
        public Label Label { get; set; }
        public Section? Section { get; set; }
        public Button RemoveButton { get; set; }
        public void UpdateSection(Section section)
        {
            this.Label.BackColor = section.Color;     
            this.Label.ForeColor = section.FontColor;
            
        }
        public void AddRemoveButton(FlowLayoutPanel flowStart, FlowLayoutPanel flowEnd, List<Server> startList, List<Server> endList, int width, int height)
        {            
            this.Label.Width -= this.Label.Height;
            
            RemoveButton = new Button
            {
                Height = this.Label.Height,
                Width = this.Label.Height - 5,
                Location = new Point(this.Label.Width, 0),
                Text = "X",
                BackColor = Color.Red,
                Padding = new Padding(0),
                TabStop = false
            };            
           
            this.NamePanel.Controls.Add(RemoveButton);
        }

        public List<ShiftControl> ShiftControls = new List<ShiftControl>(); 
        
        public void DisplayShifts(int maxShiftsToShow = 5)
        {
            ShiftsDisplay = new FlowLayoutPanel
            {
                Height = this.Height,
                Width = this.Width,
                AutoSize = true,
                Margin = new Padding(0)
            };
            this.Controls.Add(ShiftsDisplay);
           
            float OutsidePercentage = 0f;
            
            if (this.Server.Shifts != null)
            {
                var lastShifts = this.Server.Shifts.Take(maxShiftsToShow);
                lastShifts = lastShifts.OrderBy(s => s.Date).ToList();

                foreach (var shift in lastShifts)
                {
                    ShiftControl shiftControl = new ShiftControl(shift, this.Width / 8, 80); 
                    this.ShiftControls.Add(shiftControl);
                    this.ShiftsDisplay.Controls.Add(shiftControl);
                }
                var lastShiftsForPercentage = this.Server.Shifts.Take(10);
                int OutsideShifts = 0;
                foreach (var shift in lastShiftsForPercentage)
                {
                    if (!shift.IsInside)
                    {
                        OutsideShifts += 1;    
                    }
                }
                //OutsidePercentage = (float)OutsideShifts / (float)lastShiftsForPercentage.Count();
                //string formattedPercentage = $"{(int)(OutsidePercentage * 100)}%";
                //this.lblOutsidePercentage.Text = $"Last {lastShiftsForPercentage.Count()}: {formattedPercentage}";
                string serverRatingDisplay =
                    $"Section:       {this.Server.PreferedSectionWeight}\n" +
                    $"TeamWait:  {this.Server.TeamWaitFrequency}\n" +
                    $"Close:           {this.Server.CloseFrequency}";
                this.lblOutsidePercentage.Text = serverRatingDisplay;
                //this.lblOutsidePercentage.Font = UITheme.SmallerFont;
                this.lblOutsidePercentage.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                this.lblOutsidePercentage.Margin = new Padding(10,4,0,0);
                this.lblOutsidePercentage.AutoSize = false;
                this.lblOutsidePercentage.Size = new Size(90, 50);
                ShiftsDisplay.Controls.Add(this.lblOutsidePercentage);
            }            
        }
        public void HideShifts()
        {
            this.ShiftsDisplay.AutoSize = false;
            this.ShiftsDisplay.MaximumSize = new Size(this.Width, 0);
        }
        public void ShowShifts()
        {
            this.ShiftsDisplay.AutoSize = true;            
        }

        
    }
}
