using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class ServerControl : Panel
    {
        public ServerControl(Server server, int width, int height)
        {
            this.Server = server;

            this.Height = height;
            this.Width = width;
            this.BackColor = Color.White;
            
            Label = new Label
            {
                Text = Server.Name,
                AutoSize = false,
                Height = height,
                Width = width,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(Label);
            this.MouseDown += new MouseEventHandler(this.ServerControl_MouseDown);
            this.MouseMove += new MouseEventHandler(this.ServerControl_MouseMove);

            Label.MouseDown += new MouseEventHandler(this.ServerControl_MouseDown);
            Label.MouseMove += new MouseEventHandler(this.ServerControl_MouseMove);
        }
        //public Panel Panel { get; set; }
        public Server Server { get; set; }
        public Section? Section { get; set; }
        public Label Label { get; set; }
        private Point MouseDownLocation;

        private void ServerControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }

        private void ServerControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
           
        }
    }
}
