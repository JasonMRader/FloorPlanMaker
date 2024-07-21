using FloorPlanMaker;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.CompilerServices;

namespace FloorplanClassLibrary
{
    public class SectionLabelControl : UserControl, ISectionObserver
    {
        private Label sectionLabel;
        private PictureBox assignServerButton;
        private PictureBox setCloserButton;
        private FlowLayoutPanel serversPanel;
        private Panel headerPanel;
        private Point MouseDownLocation;
        private FlowLayoutPanel closerPanel;
        private bool closerPanelOpen = false;
        public bool isSelected = false;
        private bool serverPanelOpen = false;
        private bool isDragging = false; 
        private Color BorderColor = Color.Black;
        private int borderWidth = 5;
        private ToolTip toolTip;
        public event EventHandler AssignPickup;
        // TODO: rework what apears on SectionLabels
        public event EventHandler SectionLabelClick;

        public Section Section { get; set; }

        private List<Server> unassignedServers = new List<Server>();
        private List<Server> allServers = new List<Server>();
        public SectionLabelControl(Section section, List<Server> unassignedServers, List<Server> allServers)
        {
            this.Section = section;
            section.ServerAssigned += RemoveServerFromAvailable;
            section.ServerRemoved += AddServerToAvailable;
           
            this.Section.SubscribeObserver(this);
            this.unassignedServers = unassignedServers;
            this.allServers = allServers;
            closerPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 0 };
            closerPanel.AutoSize = true;

            sectionLabel = new Label
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold)
            };
            assignServerButton = new PictureBox
            {
                Image = Resources.person,
                Dock = DockStyle.Right,
                Size = new Size(23, 23),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            setCloserButton = new PictureBox
            {
                //Image = Resource1.Cut,
                Dock = DockStyle.Right,
                Size = new Size(23, 23),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            serversPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 0 };
            headerPanel = new Panel { Dock = DockStyle.Top, Height = 30 }; // Assuming height of 30, adjust as needed
            //headerPanel.AutoSize = true;
            this.Height = 30;
           
            this.AutoSize = true;
            this.Padding = new Padding(5); // Adjust this value based on your desired border thickness.

            assignServerButton.Click += AssignServerButton_Click;
            setCloserButton.Click += SetToCloserButton_Click;
            //this.BorderStyle = BorderStyle.FixedSingle;
            this.Paint += SectionControl_Paint;
            this.Controls.Add(closerPanel);
            headerPanel.Controls.Add(assignServerButton);
            headerPanel.Controls.Add(setCloserButton);
            headerPanel.Controls.Add(sectionLabel);

            Controls.Add(serversPanel);
            Controls.Add(headerPanel);
            this.MouseDown += SectionControl_MouseDown;
            this.MouseMove += SectionControl_MouseMove;
            this.MouseUp += SectionControl_MouseUp;

            sectionLabel.MouseDown += SectionControl_MouseDown;
            sectionLabel.MouseMove += SectionControl_MouseMove;
            sectionLabel.MouseUp += SectionControl_MouseUp;
            sectionLabel.Click += SectionLabel_Click1;
            this.ResizeRedraw = true;

            UpdateLabel();
            this.Location = new Point(section.MidPoint.X - (this.Width / 2),
                section.MidPoint.Y - (this.Height / 2));
            this.BringToFront();
            this.toolTip = new ToolTip();
            toolTip.SetToolTip(setCloserButton, "Assign Closer");
            toolTip.SetToolTip(assignServerButton, "Assign Server");
            toolTip.SetToolTip(sectionLabel, "[Hold LEFT-MOUSE] Drag\n" +
                "[LEFT-MOUSE Select]\n" +
                "[RIGHT-MOUSE] Assign Server\n" +
                "[SHIFT + LEFT-MOUSE] Swap Server With SELECTED Section\n" +
                "[SHIFT + RIGHT-MOUSE] Unassign Server");
            
        }
        public void SectionLabel_ShiftClick(Section otherSection)
        {
            if (otherSection == null) { return; }

        }
        private void SectionLabel_Click1(object? sender, EventArgs e)
        {
            SectionLabelClick?.Invoke(this, e);
            if (e is MouseEventArgs mouseEventArgs)
            {
                bool isShiftPressed = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                if (mouseEventArgs.Button == MouseButtons.Right && !isShiftPressed)
                {
                    ToggleAssignServerPanel();
                }
            }
        }

        private void AddServerToAvailable(Server server, Section section)
        {
            if (!unassignedServers.Contains(server))
            {
                unassignedServers.Add(server);
            }
            
        }

        private void RemoveServerFromAvailable(Server server, Section section)
        {
            unassignedServers.Remove(server);
        }

        public void UpdateSection(Section section)
        {
            this.isSelected = this.Section.IsSelected;
            // Update the control based on the new state of the section
            UpdateLabel();
           
            //this.Invalidate();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Section.RemoveObserver(this);
            }
            base.Dispose(disposing);
        }
        private void SetToCloserButton_Click(object? sender, EventArgs e)
        {

            if (e is MouseEventArgs mouseEventArgs)
            {
                if (mouseEventArgs.Button == MouseButtons.Right)
                {
                    if (this.closerPanelOpen == false)
                    {
                        PictureBox pbCut = new PictureBox { Size = new Size((this.Width / 4), (this.Width / 4)), Image = Resources.Scissors__Copy, SizeMode = PictureBoxSizeMode.StretchImage };
                        pbCut.Click += Cut_Click;
                        PictureBox pbPre = new PictureBox { Size = new Size((this.Width / 4), (this.Width / 4)), Image = Resources.Pre2, SizeMode = PictureBoxSizeMode.StretchImage };
                        pbPre.Click += Pre_Click;
                        PictureBox pbClose = new PictureBox { Size = new Size((this.Width / 4), (this.Width / 4)), Image = Resources.Close, SizeMode = PictureBoxSizeMode.StretchImage };
                        pbClose.Click += Close_click;
                        if (!this.Section.IsPre) { closerPanel.Controls.Add(pbPre); }
                        if (!this.Section.IsCloser) { closerPanel.Controls.Add(pbClose); }
                        if (this.Section.IsPre || this.Section.IsCloser) { closerPanel.Controls.Add(pbCut); }

                        closerPanel.Height = pbClose.Height;

                    }
                    if (this.closerPanelOpen == true)
                    {
                        closerPanel.Controls.Clear();
                        closerPanel.Height = 0;

                    }
                    closerPanelOpen = !closerPanelOpen;
                }
                else if (mouseEventArgs.Button == MouseButtons.Left)
                {
                    if (!this.Section.IsPre && !this.Section.IsCloser)
                    {
                        SetSectionToPre();
                        return;
                    }
                    if (this.Section.IsPre)
                    {
                        SetSectionToClose();
                        return;
                    }
                    if (this.Section.IsCloser)
                    {
                        SetSectionToCut();
                        return;
                    }
                } 
            }

        }
        

        private void SectionControl_Paint(object sender, PaintEventArgs e)
        {           
            
            if (this.isSelected == true)
            {
                BorderColor = UITheme.MuteColor(3f, this.Section.Color);
                borderWidth = 30;
            }
            using (Pen pen = new Pen(BorderColor, borderWidth))
            {
                // Adjust the rectangle's coordinates and size to ensure the entire border is visible
                int penHalfWidth = (int)(pen.Width / 2);
                e.Graphics.DrawRectangle(pen, penHalfWidth, penHalfWidth,
                                         this.Width - pen.Width, this.Height - pen.Width);
            }
           
           
        }
       
        private void SectionControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true; // Start the drag action
                MouseDownLocation = e.Location;
            }
        }

        private void SectionControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                // Determine the new position of the control
                this.Left += e.X - MouseDownLocation.X;
                this.Top += e.Y - MouseDownLocation.Y;

                // Optional: Update the parent form or control to reflect the new position
                this.Update();
            }
        }

        private void SectionControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false; // End the drag action
            }
        }
        private void Cut_Click(object sender, EventArgs e)
        {
            SetSectionToCut();
            if (this.closerPanelOpen == true)
            {
                closerPanel.Controls.Clear();
                closerPanel.Height = 0;

            }
            closerPanelOpen = !closerPanelOpen;
        }
        private void SetSectionToCut()
        {

            this.Section.SetToCut();
            this.setCloserButton.Image = Resources.Scissors__Copy;
        }
        private void SetSectionToClose()
        {
            if (this.Section.Server.isDouble)
            {
                DialogResult result = MessageBox.Show(Section.Server.Name + " is a Double. Assign as closer anyway?",
                                                 "Continue?",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }
            }
            this.Section.SetToClose();
            this.setCloserButton.Image = Resources.Close;
        }
        private void Close_click(object sender, EventArgs e)
        {
            if (this.Section.IsTeamWait)
            {
                //closerPanelOpen = !closerPanelOpen;
                //MessageBox.Show("Assigning a teamwait section as close is not supported at this time");
               
                //return;

            }
            SetSectionToClose();
            if (this.closerPanelOpen == true)
            {
                closerPanel.Controls.Clear();
                closerPanel.Height = 0;

            }
            closerPanelOpen = !closerPanelOpen;
        }
        private void SetSectionToPre()
        {
            this.Section.SetToPre();
            
            this.setCloserButton.Image = Resources.Pre2;
        }
        private void Pre_Click(object sender, EventArgs e)
        {
            if (this.Section.IsTeamWait)
            {
                //closerPanelOpen = !closerPanelOpen;
                //MessageBox.Show("Assigning a teamwait section as close is not supported at this time");
                
                //return;

            }
            SetSectionToPre();
            if (this.closerPanelOpen == true)
            {
                closerPanel.Controls.Clear();
                closerPanel.Height = 0;

            }
            closerPanelOpen = !closerPanelOpen;
        }
        private void AssignServerButton_Click(object sender, EventArgs e)
        {
            if(this.Section.IsPickUp)
            {
                AssignPickup?.Invoke(this, e);
               
                return;
            }
            ToggleAssignServerPanel();

        }
        private void ToggleAssignServerPanel()
        {
            if (serverPanelOpen == false)
            {

                if (!this.Section.IsPickUp && this.Section.ServerTeam.Count() < this.Section.ServerCount)
                {
                    RefreshUnassignedServerPanel();
                }
                //if (this.Section.IsPickUp)
                //{
                //    RefreshAllServerPanelForPickUpSection();
                //}

                if (this.Section.Server != null)
                {
                    Button unassign = new Button { Text = "Unassign", Dock = DockStyle.Top, Width = this.Width - 20 };
                    unassign.Click += UnassignButton_Click;
                    serversPanel.Controls.Add(unassign);
                    serversPanel.Height += 30;

                }
            }
            if (serverPanelOpen == true)
            {
                serversPanel.Controls.Clear();
                serversPanel.Height = 0;

            }
            serverPanelOpen = !serverPanelOpen;
        }
        private void teamWaitButton_Click(object? sender, EventArgs e)
        {
            this.Section.MakeTeamWait();
            RefreshUnassignedServerPanel();
        }

        private void RefreshUnassignedServerPanel()
        {
            serversPanel.Controls.Clear();
            if (this.Section.Server != null)
            {
                Button unassign = new Button { Text = "Unassign", Dock = DockStyle.Top, Width = this.Width - 20 };
                unassign.Click += UnassignButton_Click;
                serversPanel.Controls.Add(unassign);
                //serversPanel.Height += 30;
            }
            foreach (var server in unassignedServers)
            {
            var serverButton = new Button { Text = server.ToString(), Tag = server, Dock = DockStyle.Top, Width = this.Width - 20 };
            serverButton.Click += ServerButton_Click;
            serversPanel.Controls.Add(serverButton);
            }
           
            if (this.Section.ServerCount == this.Section.ServerTeam.Count())
            {
                serversPanel.Height = 0;
                serverPanelOpen = false;               
            }
            else
            {
                serversPanel.Height = (serversPanel.Controls.Count * 30);
            }

            this.BringToFront();

        }
        private void RefreshAllServerPanelForPickUpSection()
        {
            serversPanel.Controls.Clear();
            if (this.Section.Server != null)
            {
                Button unassign = new Button { Text = "Unassign", Dock = DockStyle.Top, Width = this.Width - 20 };
                unassign.Click += UnassignButton_Click;
                serversPanel.Controls.Add(unassign);
                serversPanel.Height += 30;
            }
            foreach (var server in allServers)
            {
                var serverButton = new Button { Text = server.ToString(), Tag = server, Dock = DockStyle.Top, Width = this.Width - 20 };
                serverButton.Click += ServerButton_Click;
                serversPanel.Controls.Add(serverButton);
            }
            serversPanel.Height = (allServers.Count * 30);
        }
        private void UnassignButton_Click(Object sender, EventArgs e)
        {
            this.Section.ClearAllServers();
            if (this.Section.IsPickUp)
            {
                this.Section.MakeSoloSection();
            }
            
            RefreshUnassignedServerPanel();
            UpdateLabel();
        }

        private void ServerButton_Click(object sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            var assignedServer = (Server)clickedButton.Tag;

            if (this.Section.IsPickUp)
            {
                if(this.Section.ServerTeam.Count() == this.Section.ServerCount)
                {
                    this.Section.IncreaseServerCount();
                }
            }
            Section.AddServer(assignedServer);
            //assignedServer.SalesFromPickupSection = this.Section.AdditionalPickupSales;
            if(Section.ServerTeam != null)
            {
                for(int i = 0;  i < Section.ServerTeam.Count; i++) 
                {
                    this.sectionLabel.Height += 30;
                    this.headerPanel.Height += 30;
                }
            }
            if(this.Section.ServerCount == this.Section.ServerTeam.Count())
            {
                serversPanel.Height = 0;
                serverPanelOpen = false;
                RefreshUnassignedServerPanel();
            }
            else
            {
                RefreshUnassignedServerPanel();
            }
            
           
            UpdateLabel();
            
        }

        public void UpdateLabel()
        {
            
            sectionLabel.Text = Section.GetDisplayString();
            headerPanel.Height = 30;
            if(Section.ServerTeam.Count > 1 && !Section.IsBarSection)
            {
                for(int i = 0; i < Section.ServerTeam.Count-1; i++)
                {
                    headerPanel.Height += 25;
                }
            }
            headerPanel.BackColor = Section.Color; // Assuming the Section class has a Color property
            headerPanel.ForeColor = Section.FontColor;
            if(this.isSelected)
            {
                headerPanel.BackColor = Section.MuteColor(1.5f);
                this.BorderColor = Section.MuteColor(2);
                this.borderWidth = 20;
               
            }
            else
            {
                headerPanel.BackColor = Section.MuteColor(.5f);
                this.BorderColor = Color.Black;
                this.borderWidth = 5;
                
            }
            if (this.Section.IsCloser)
            {
                setCloserButton.Image = Resources.Close;
            }
            else if (this.Section.IsPre)
            {
                setCloserButton.Image = Resources.Pre2;
            }
            else
            {
                setCloserButton.Image = Resources.Scissors__Copy;
            }
            using (Graphics g = this.CreateGraphics())
            {
               
                SizeF stringSize = g.MeasureString(sectionLabel.Text, sectionLabel.Font);

               
                this.Width = (int)stringSize.Width + 60; // Adding some padding
                this.Height = 27;
               
            }
            this.Invalidate();

        }
        public static void DrawSectionLabelForPrinting(Graphics g, SectionLabelControl control)
        {
            Font boldLargeFont = new Font(control.Font.FontFamily, 20f, FontStyle.Bold);

            // Measure the size of the text
            SizeF textSize = g.MeasureString(control.Section.GetDisplayString(), boldLargeFont);
            if (control.Section.IsCloser || control.Section.IsPre)
            {
                textSize = g.MeasureString(control.Section.GetDisplayString(), boldLargeFont);
            }
            // Define the rectangle based on the text size with a margin of 5 pixels
            Rectangle rect = new Rectangle(
                control.Bounds.X - 5,
                control.Bounds.Y - 5,
                (int)textSize.Width + 10,  // 5 pixels on left + 5 pixels on right
                (int)textSize.Height + 10  // 5 pixels on top + 5 pixels on bottom
            );

            // Draw the border rectangle
            using (Pen pen = new Pen(Color.Black, 3))
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(brush, rect);
                }
                g.DrawRectangle(pen, rect);
            }

            // Draw the section label (abbreviated server name)
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Calculate the center position of the text inside the rectangle
            float textX = rect.X + rect.Width / 2;
            float textY = rect.Y + rect.Height / 2;

            g.DrawString(control.Section.GetDisplayString(), boldLargeFont, Brushes.Black, textX, textY, sf);
        }

        
       

    }


}
