﻿using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class ServerControl : FlowLayoutPanel, ISectionObserver
    {
        public ServerControl(Server server, int height)
        {
            this.Server = server;

            this.Height = height *2 ;
            this.Width = 300;//this.Parent.Width - 20;
            this.BackColor = UITheme.AccentColor;
            this.AutoSize = true;
            this.MaximumSize = new Size(300, height * 10);// new Size(this.Parent.Width - 20, height*10);
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
                Text = Server.AbbreviatedName,
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
           
            
           
            DisplayShifts();
           
        }
        public Panel NamePanel { get; set; }
        private Server _server;
        public Server Server
        {
            get => _server;
            set
            {
                if (_server != value)
                {
                    

                    _server = value;
                   
                }
            }
        }

        private void OnServerRemovedFromSection(Section obj)
        {
            this.Section = null;
            this.Label.BackColor = UITheme.ButtonColor;

        }

        private void OnServerAssignedToSection(Section section)
        {
            this.Section = section;
            this.Update(section);
        }
        public FlowLayoutPanel ShiftsDisplay { get; set; }
        public Label Label { get; set; }
        public Section? Section { get; set; }
        public Button RemoveButton { get; set; }
        public void Update(Section section)
        {
            this.Section = section;
            this.Label.BackColor = section.Color;

        }
        public void AddRemoveButton(FlowLayoutPanel flowStart, FlowLayoutPanel flowEnd, List<Server> startList, List<Server> endList, int width, int height)
        {
            // Adjust the label width
            this.Label.Width -= this.Label.Height;

            // Create a new button
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
            // Assuming you have loaded shifts for this server

            if (this.Server.Shifts != null)
            {
                var lastShifts = this.Server.Shifts.Take(maxShiftsToShow);
                lastShifts = lastShifts.OrderBy(s => s.Date).ToList();

                foreach (var shift in lastShifts)
                {
                    ShiftControl shiftControl = new ShiftControl(shift, this.Width / 8, 80);  // Adjust width and height as needed
                    this.ShiftControls.Add(shiftControl);
                    this.ShiftsDisplay.Controls.Add(shiftControl);
                } 
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
