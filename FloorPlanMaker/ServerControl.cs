using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class ServerControl : FlowLayoutPanel
    {
        public ServerControl(Server server, int width, int height)
        {
            this.Server = server;

            this.Height = height *2 ;
            this.Width = width;
            this.BackColor = Color.White;
            this.AutoSize = true;
            this.MaximumSize = new Size(width, height*10);
            this.Padding = new Padding(0,0,0,0);
            this.Margin = new Padding(0,4,0,0);
            NamePanel = new Panel()
            {
                Height = height,
                Width = width,
            };
            Label = new Label
            {
                Text = Server.Name,
                AutoSize = false,
                Height = height,
                Width = width,
                BackColor = Color.LightBlue,
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
        public Server Server { get; set; }
        public FlowLayoutPanel ShiftsDisplay { get; set; }
        public Label Label { get; set; }
        public Button RemoveButton { get; set; }
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
            
           

            // Add click event to the button
            //RemoveButton.Click += (sender, e) =>
            //{
            //    // Remove from flowStart and startList
            //    flowStart.Controls.Remove(this);
            //    startList.Remove(this.Server);                
                
            //    ServerControl serverControl = new ServerControl(this.Server, width, height);
            //    serverControl.HideShifts();
            //    flowEnd.Controls.Add(this);
            //    endList.Add(this.Server);
            //};

            // Add button to the ServerControl
            this.NamePanel.Controls.Add(RemoveButton);
        }

        public List<ShiftControl> ShiftControls = new List<ShiftControl>(); 
        
        public void DisplayShifts(int maxShiftsToShow = 5)
        {
            ShiftsDisplay = new FlowLayoutPanel
            {
                Height = this.Height,
                Width = this.Width,
                AutoSize = true
            };
            this.Controls.Add(ShiftsDisplay);
            // Assuming you have loaded shifts for this server

            if (this.Server.Shifts != null)
            {
                var lastShifts = this.Server.Shifts.TakeLast(maxShiftsToShow);

                foreach (var shift in lastShifts)
                {
                    ShiftControl shiftControl = new ShiftControl(shift, this.Width / 6, 80);  // Adjust width and height as needed
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
