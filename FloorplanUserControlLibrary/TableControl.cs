using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FloorPlanMakerUI;
using System.Xml.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using FloorplanUserControlLibrary;

//using static System.Collections.Specialized.BitVector32;

namespace FloorPlanMaker
{
    public class TableControl : Control, ISectionObserver
    {
        // TODO fix tableControl text colors not always adjusting properly
        public ToolTip toolTip = new ToolTip();
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
            
        }
        public void UpdateSection(Section section)
        {
            if (section.Tables.Contains(Table))
            {
                if (section.IsSelected)
                {
                    this.TextColor = section.FontColor;
                }
                else
                {
                    this.TextColor = Color.FromArgb(150, section.FontColor); // Semi-transparent text color
                }

                _section = section;
                this.Invalidate(); // Trigger a repaint
            }
        }


        public void SetSection(Section section)
        {
            section.SubscribeObserver(this);
            this._section = section;
            UpdateSection(section);
           
        }
        public void RemoveSection()
        {
            if(this._section != null)
            {
                this._section.RemoveObserver(this);
                this._section = null;
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
            // Draw semi-transparent background
            if (_section != null && !_section.IsSelected)
            {
                using (Brush semiTransparentBrush = new SolidBrush(Color.FromArgb(25, this.BackColor))) // 100 is the alpha value
                {
                    pe.Graphics.FillRectangle(semiTransparentBrush, this.ClientRectangle);
                }
            }
            else
            {
                // Draw solid background
                using (Brush solidBrush = new SolidBrush(this.BackColor))
                {
                    pe.Graphics.FillRectangle(solidBrush, this.ClientRectangle);
                }
            }

            // Use existing logic to draw the table control content
            DrawTableOnGraphics(pe.Graphics, this);
        }


        public static void DrawTableOnGraphics(Graphics g, TableControl control, bool isForPrint = false)
        {
            //control.toolTip.SetToolTip(control, control.Table.AverageSales.ToString("C0"));
            int xOffset = isForPrint ? control.Left : 0;
            int yOffset = isForPrint ? control.Top : 0;
            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if(control.Section != null)
            {
                if (control.Section.IsSelected && !isForPrint)
                {
                    int innerOffset = 2; // 3 pixels inside the black line
                    int orangeLineThickness = control.BorderThickness + 1; // Make the orange line thicker

                    using (Pen highlightPen = new Pen(Color.White, orangeLineThickness))
                    {
                        switch (control.Shape)
                        {
                            case Table.TableShape.Circle:
                                g.DrawEllipse(highlightPen,
                                    xOffset + innerOffset,
                                    yOffset + innerOffset,
                                    control.Width - control.BorderThickness - innerOffset * 2,
                                    control.Height - control.BorderThickness - innerOffset * 2);
                                break;
                            case Table.TableShape.Square:
                                g.DrawRectangle(highlightPen,
                                    xOffset + innerOffset,
                                    yOffset + innerOffset,
                                    control.Width - control.BorderThickness - innerOffset * 2,
                                    control.Height - control.BorderThickness - innerOffset * 2);
                                break;
                            case Table.TableShape.Diamond:
                                Point[] highlightDiamondPoints = {
                        new Point(xOffset + control.Width / 2, yOffset + innerOffset),
                        new Point(xOffset + control.Width - innerOffset, yOffset + control.Height / 2),
                        new Point(xOffset + control.Width / 2, yOffset + control.Height - innerOffset),
                        new Point(xOffset + innerOffset, yOffset + control.Height / 2)
                    };
                                g.DrawPolygon(highlightPen, highlightDiamondPoints);
                                break;
                        }
                    }
                }
            }
            

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
                    textToDisplay = control.Table.AverageSales.ToString("C0"); // One decimal place
                    break;
            }

            if (!string.IsNullOrEmpty(textToDisplay))
            {
                // Determine the font style
                FontStyle fontStyle = FontStyle.Regular;

                if (control.Section != null )
                {
                    if (control.Section.IsSelected)
                    {
                        fontStyle = FontStyle.Bold;
                    }
                    
                }

                using (Font font = new Font(control.Font.FontFamily, control.TableNumberFontSize, fontStyle))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    Rectangle tableBounds = new Rectangle(xOffset, yOffset, control.Width, control.Height);
                    using (Brush textBrush = new SolidBrush(isForPrint ? Color.Black : control.TextColor)) // Use the TextColor property
                    {
                        g.DrawString(textToDisplay, font, textBrush, tableBounds, sf);
                    }
                }
            }
        }
        public static void DrawTableOnGraphics(XGraphics gfx, TableControl control, bool isBlackAndWhite)
        {
            int xOffset = control.Left;
            int yOffset = control.Top;
            
            XPen pen = new XPen(XColors.Black, control.BorderThickness);
            if (!isBlackAndWhite)
            {
                pen.Color = control.BackColor.ToXColor();
            }
            switch (control.Shape)
            {
                case Table.TableShape.Circle:
                    gfx.DrawEllipse(pen, xOffset, yOffset, control.Width - control.BorderThickness, control.Height - control.BorderThickness);
                    break;
                case Table.TableShape.Square:
                    gfx.DrawRectangle(pen, xOffset, yOffset, control.Width - control.BorderThickness, control.Height - control.BorderThickness);
                    break;
                case Table.TableShape.Diamond:
                    XPoint[] diamondPoints = {
                    new XPoint(xOffset + control.Width / 2, yOffset),
                    new XPoint(xOffset + control.Width, yOffset + control.Height / 2),
                    new XPoint(xOffset + control.Width / 2, yOffset + control.Height),
                    new XPoint(xOffset, yOffset + control.Height / 2)
                };
                    gfx.DrawPolygon(pen, diamondPoints);
                    break;
            }

            string textToDisplay = control.Table.TableNumber.ToString();
            

            if (!string.IsNullOrEmpty(textToDisplay))
            {
                //XFont font = new XFont(control.Font.FontFamily.Name, control.TableNumberFontSize);
                var font = new XFont("Arial", 16, XFontStyleEx.Bold);

                XRect tableBounds = new XRect(xOffset, yOffset, control.Width, control.Height);
                //XBrush textBrush = isBlackAndWhite ? XBrushes.Black : new XSolidBrush(control.TextColor.ToXColor());
                //Color color = Color.FromArgb(64, 64, 64);
                XBrush textBrush = new XSolidBrush(XColors.Black);

                gfx.DrawString(textToDisplay, font, textBrush, tableBounds, XStringFormats.Center);
            }
        }

        

        public void MuteColors()
        {
            if(this.Section == null) { return; }
            this.BackColor = MuteColor(this.Section.Color);
            this.TextColor = MuteColor(this.Section.FontColor);
        }
        private Color MuteColor(Color originalColor, float blendFactor = 0.3f)
        {
            // Blend the original color with gray
            int r = (int)(originalColor.R * (1 - blendFactor) + 128 * blendFactor);
            int g = (int)(originalColor.G * (1 - blendFactor) + 128 * blendFactor);
            int b = (int)(originalColor.B * (1 - blendFactor) + 128 * blendFactor);

            return Color.FromArgb(r, g, b);
        }
        public TableControl() : this(new Table()) {  }
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
        public void SimulateTableClick(MouseButtons button)
        {
            OnTableClicked(button);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(this.Section != null)
                {
                    Section.RemoveObserver(this);
                }
               
            }
            base.Dispose(disposing);
        }

    }
    public enum DisplayMode
    {
        TableNumber,
        MaxCovers,
        AverageCovers
    }
}
