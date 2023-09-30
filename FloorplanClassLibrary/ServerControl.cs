using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
            this.MaximumSize = new Size(width, height*2);
            
            Label = new Label
            {
                Text = Server.Name,
                AutoSize = false,
                Height = height,
                Width = width,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(Label);
            ShiftsDisplay = new Panel
            {
                Height = height,
                Width = width
            };
            this.Controls.Add(ShiftsDisplay);
            DisplayShifts(server);
           
        }
        //public Panel Panel { get; set; }
        public Server Server { get; set; }
        public Panel ShiftsDisplay { get; set; }
        public Label Label { get; set; }
        public Image CloserImage { get; set; }
        public Image TeamWaitImage { get; set; }
        public Image OutsideImage { get; set; }
        public void DisplayShifts(Server server, int maxShiftsToShow = 5)
        {
            // Assuming you have loaded shifts for this server
            var lastShifts = server.Shifts.TakeLast(maxShiftsToShow);

            foreach (var shift in lastShifts)
            {
                ShiftControl shiftControl = new ShiftControl(shift, 50, 125);  // Adjust width and height as needed
                this.ShiftsDisplay.Controls.Add(shiftControl);
            }
        }

        

       
    }
}
