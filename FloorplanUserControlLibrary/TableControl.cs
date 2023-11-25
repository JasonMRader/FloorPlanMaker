using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FloorPlanMakerUI;

namespace FloorPlanMaker
{
    public class TableControl : Control
    {
        
        public Color BorderColor { get; set; } = Color.Black; // default to DarkBlue
        public TableDataEditorControl dataEditor {get;set;}
        public Color TextColor { get; set; } = Color.Black;
        public DisplayMode CurrentDisplayMode { get; set; } = DisplayMode.TableNumber;
        public int BorderThickness { get; set; } = 2; // default to 1
        public float TableNumberFontSize { get; set; } = 14f; // default to 16
        //public bool IsInSection { get; set; } = false;
        private Section? _section { get; set; }
        public Section? Section
        {
            get => _section;
            set
            {
                if (_section != value)
                {
                    _section = value;
                }
            }
        }

        public Table.TableShape Shape { get; set; }
        public Table Table { get; set; }
        public bool IsSelected { get; set; } = false;
        public string _tableNumber { get { return this.Table.TableNumber; } }
        public Point TopLeft { get { return new Point (this.Left, this.Top); } }
        public Point TopRight { get { return new Point(this.Right, this.Left); } }
        public Point BottomRight { get { return new Point(this.Right, this.Bottom); } }
        public Point BottomLeft { get { return new Point(this.Left, this.Bottom); } }
        public Point TopLeftLinePoint { get { return this.LeftLine.StartPoint; } }
        public Point TopRightLinePoint { get { return this.RightLine.StartPoint; } }
        public Point BottomRightLinePoint { get { return this.BottomLine.EndPoint; } }
        public Point BottomLeftLinePoint { get { return this.BottomLine.StartPoint; } }
        public TextBox txtTableNumber;
        public delegate void TableControlEventHandler(TableControl sender, EventArgs e);
        public bool Moveable { get; set; }
        private bool wasMoved = false;

        private Point MouseDownLocation;
        public void AddCoverEditor()
        {
            this.dataEditor = new TableDataEditorControl(this);
            if(this.txtTableNumber != null )
            {
                this.txtTableNumber.Dispose();
            }  
            this.Controls.Add(this.dataEditor);
            this.dataEditor.SetToCoversOnly();
            this.dataEditor.BringToFront();
            this.dataEditor.Show();
            this.Invalidate();
        }
        public void  AddTxtTableNumber()
        {
             this.txtTableNumber = new TextBox
            {
                Font = UITheme.MainFont,
                MaximumSize = new Size(30, 36),
                Location = new Point(this.Width / 2 - 18, this.Height/2-15),
                Text = this._tableNumber
            };
            txtTableNumber.TextChanged += txtTableNumber_TextChanged;
            this.Controls.Add(txtTableNumber);
        }
        private void txtTableNumber_TextChanged(object sender, EventArgs e)
        {
            this.Table.TableNumber = this.txtTableNumber.Text;
            SqliteDataAccess.UpdateTable(this.Table);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawTableOnGraphics(pe.Graphics, this);
        }

        public static void DrawTableOnGraphics(Graphics g, TableControl control, bool isForPrint = false)
        {
            int xOffset = isForPrint ? control.Left : 0;
            int yOffset = isForPrint ? control.Top : 0;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(control.BorderColor, control.BorderThickness))
            {
                switch (control.Shape)
                {
                    case Table.TableShape.Circle:
                        g.DrawEllipse(pen, xOffset, yOffset, control.Width - control.BorderThickness, control.Height - control.BorderThickness);
                        break;
                    case Table.TableShape.Square:
                        g.DrawRectangle(pen, xOffset, yOffset, control.Width - control.BorderThickness, control.Height - control.BorderThickness);
                        break;
                    case Table.TableShape.Diamond:
                        Point[] diamondPoints = {
                    new Point(xOffset + control.Width / 2, yOffset),
                    new Point(xOffset + control.Width, yOffset + control.Height / 2),
                    new Point(xOffset + control.Width / 2, yOffset + control.Height),
                    new Point(xOffset, yOffset + control.Height / 2)
                };
                        g.DrawPolygon(pen, diamondPoints);
                        break;
                }
            }


            string textToDisplay = string.Empty;
            switch (control.CurrentDisplayMode)
            {
                case DisplayMode.TableNumber:
                    textToDisplay = control.Table.TableNumber.ToString();
                    break;
                case DisplayMode.MaxCovers:
                    textToDisplay = control.Table.MaxCovers.ToString();
                    break;
                case DisplayMode.AverageCovers:
                    textToDisplay = control.Table.AverageCovers.ToString("C0"); // One decimal place
                    break;
            }

            if (!string.IsNullOrEmpty(textToDisplay))
            {
                using (Font font = new Font(control.Font.FontFamily, control.TableNumberFontSize))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;


                    Rectangle tableBounds = new Rectangle(xOffset, yOffset, control.Width, control.Height);
                    using (Brush textBrush = new SolidBrush(control.TextColor)) // Use the TextColor property
                    {
                        g.DrawString(textToDisplay, font, textBrush, tableBounds, sf);
                    }
                }
            }
        }

        public void MuteColors()
        {
            this.BackColor = UITheme.MuteColor(.5f, this.Section.Color);
            this.TextColor = Color.White;
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
            this.MouseUp += new MouseEventHandler(TableControl_MouseUp);
        }
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
                wasMoved = true;
            }
            this.Table.XCoordinate = this.Left;
            this.Table.YCoordinate = this.Top;
        }
        private void TableControl_MouseUp(object? sender, MouseEventArgs e)
        {
            if (wasMoved)
            {
                SqliteDataAccess.UpdateTable(this.Table);
            }
            wasMoved = false;
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
        public int SectionLineBuffer { get; set; } = 5;
        //public float SectionLineThickness { get; set; } = 15f;
        public SectionLine TopLine 
        { 
            get 
            {
                if (this.Section != null)
                {
                    SectionLine topLine = new SectionLine(this.Left - this.SectionLineBuffer, this.Top - this.SectionLineBuffer,
                    this.Right + this.SectionLineBuffer, this.Top - this.SectionLineBuffer, this.Section);
                    topLine.Edge = SectionLine.BorderEdge.Right;
                    return topLine;
                }
                else
                {
                    return new SectionLine(this.Left - this.SectionLineBuffer, this.Top - this.SectionLineBuffer,
                    this.Right + this.SectionLineBuffer, this.Top - this.SectionLineBuffer);
                }
                
            }
            set { }
        }
        public SectionLine RightLine
        {
            get
            {
                if (this.Section != null)
                {
                    return new SectionLine(this.Right + this.SectionLineBuffer, this.Top - this.SectionLineBuffer,
                    this.Right + this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer, this.Section);
                }
                else
                {
                    return new SectionLine(this.Right + this.SectionLineBuffer, this.Top - this.SectionLineBuffer,
                    this.Right + this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer);
                }
                
            }
            set { }
        }
        public SectionLine BottomLine
        {
            get
            {
                if (this.Section != null)
                {
                    return new SectionLine(this.Right + this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer,
                    this.Left - this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer, this.Section);
                }
                else
                {
                    return new SectionLine(this.Right + this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer,
                    this.Left - this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer);
                }
                
            }
            set { }
        }
        public SectionLine LeftLine
        {
            get
            {
                if (this.Section != null)
                {
                    return new SectionLine(this.Left - this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer,
                    this.Left - this.SectionLineBuffer, this.Top - this.SectionLineBuffer, this.Section);
                }
                else
                {
                    return new SectionLine(this.Left - this.SectionLineBuffer, this.Bottom + this.SectionLineBuffer,
                    this.Left - this.SectionLineBuffer, this.Top - this.SectionLineBuffer);
                }
                
            }
            set { }
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
        public static Point FindMiddlePoint(List<TableControl> controls)
        {
            if (controls.Count == 0)
            {
                throw new ArgumentException("The controls list cannot be empty.");
            }

            int totalX = 0;
            int totalY = 0;

            foreach (Control control in controls)
            {
                totalX += control.Location.X + (control.Width / 2);  // X-coordinate of control's center
                totalY += control.Location.Y + (control.Height / 2); // Y-coordinate of control's center
            }

            return new Point(totalX / controls.Count, totalY / controls.Count);
        }

        public override string ToString()
        {
            return _tableNumber;
        }



    }
    public enum DisplayMode
    {
        TableNumber,
        MaxCovers,
        AverageCovers
    }
}
