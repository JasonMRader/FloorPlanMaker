using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class TableControl : Control
    {

        public enum TableShape
        {
            Circle,
            Square,
            Diamond
        }

        // Property to store the table shape
        public TableShape Shape { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            switch (Shape)
            {
                case TableShape.Circle:
                    g.FillEllipse(Brushes.DarkBlue, 0, 0, this.Width, this.Height);
                    break;
                case TableShape.Square:
                    g.FillRectangle(Brushes.DarkBlue, 0, 0, this.Width, this.Height);
                    break;
                case TableShape.Diamond:
                    Point[] diamondPoints = {
                    new Point(this.Width / 2, 0),
                    new Point(this.Width, this.Height / 2),
                    new Point(this.Width / 2, this.Height),
                    new Point(0, this.Height / 2)
                };
                    g.FillPolygon(Brushes.DarkBlue, diamondPoints);
                    break;
            }
        }
        

        public TableControl()
        {
            this.MouseDown += new MouseEventHandler(TableControl_MouseDown);
            this.MouseMove += new MouseEventHandler(TableControl_MouseMove);
            this.MouseClick += new MouseEventHandler(TableControl_MouseClick);
        }
        public bool Moveable { get; set; }


        private Point MouseDownLocation;

        private void TableControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void TableControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Moveable)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }
        public event EventHandler TableClicked;

        protected void OnTableClicked()
        {
            TableClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TableControl_MouseClick(object sender, MouseEventArgs e)
        {
            // existing code

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                OnTableClicked();
            }
        }

        // resize methods
        private bool IsOnBorder(MouseEventArgs e)
        {
            int borderThickness = 15;
            return e.X < borderThickness || // Left border
                   e.Y < borderThickness || // Top border
                   e.X > this.Width - borderThickness || // Right border
                   e.Y > this.Height - borderThickness; // Bottom border
        }
        private bool isDragging = false; // To track whether the user is dragging
        private bool isResizing = false; // To track whether the user is resizing
        private Point dragStartPoint; // The point where the dragging started
        private Size dragStartSize; // The size of the control when the dragging started
       





    }
}
