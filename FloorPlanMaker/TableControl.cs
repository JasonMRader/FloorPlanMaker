using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class TableControl : Control
    {
        public Color BorderColor { get; set; } = Color.DarkBlue; // default to DarkBlue
        public int BorderThickness { get; set; } = 1; // default to 1
        public float TableNumberFontSize { get; set; } = 14f; // default to 16
        //public bool IsInSection { get; set; } = false;
        public Section? Section { get; set; }
        public Table.TableShape Shape { get; set; }
        public Table Table { get; set; }
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(BorderColor, BorderThickness))
            {
                switch (Shape)
                {
                    case Table.TableShape.Circle:
                        g.DrawEllipse(pen, 0, 0, this.Width - BorderThickness, this.Height - BorderThickness);
                        break;
                    case Table.TableShape.Square:
                        g.DrawRectangle(pen, 0, 0, this.Width - BorderThickness, this.Height - BorderThickness);
                        break;
                    case Table.TableShape.Diamond:
                        Point[] diamondPoints = {
                        new Point(this.Width / 2, 0),
                        new Point(this.Width, this.Height / 2),
                        new Point(this.Width / 2, this.Height),
                        new Point(0, this.Height / 2)
                    };
                        g.DrawPolygon(pen, diamondPoints);
                        break;
                }
            }

            if (this.Table != null && this.Table.TableNumber != null)
            {
                using (Font font = new Font(this.Font.FontFamily, TableNumberFontSize)) // Use custom font size
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    Rectangle tableBounds = new Rectangle(0, 0, this.Width, this.Height);
                    g.DrawString(Table.TableNumber.ToString(), font, Brushes.Black, tableBounds, sf); // Number color set to black
                }
            }
        }

        public TableControl() : this(new Table()) { }
        public TableControl(Table table)
        {
            this.Table = table;
            this.Width = table.Width;
            this.Height = table.Height;
            this.Shape = table.Shape;

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
            this.Table.XCoordinate = this.Left;
            this.Table.YCoordinate = this.Top;
        }
        public event EventHandler<TableClickedEventArgs> TableClicked;

        protected void OnTableClicked(MouseButtons button)
        {
            TableClickedEventArgs args = new TableClickedEventArgs(this.Table, this.Moveable)
            {
                MouseButton = button
            };
            TableClicked?.Invoke(this, args);
        }


        private void TableControl_MouseClick(object sender, MouseEventArgs e)
        {
            // existing code

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                OnTableClicked(e.Button);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                OnTableClicked(e.Button);  // Same method, just a different button
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
