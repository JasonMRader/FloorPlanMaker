using FloorPlanMaker;
using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SectionControl : UserControl
    {
        private Label sectionLabel;
        private PictureBox assignServerButton;
        private PictureBox setCloserButton;
        private FlowLayoutPanel serversPanel;
        private Panel headerPanel;
        private Point MouseDownLocation;
        private FlowLayoutPanel closerPanel;
        private bool closerPanelOpen = false;
        private bool serverPanelOpen = false;
        private bool isDragging = false; // Indicates whether dragging is ongoing
        public Section Section { get; set; }

        public List<Server> Servers { get; set; } = new List<Server>();

        public SectionControl(Section section, Panel panel, List<Server> servers)
        {
            this.Section = section;
            closerPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 0};
            closerPanel.AutoSize = true;
           
            sectionLabel = new Label
            {
                Dock = DockStyle.Left,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold)
            };
            assignServerButton = new PictureBox { Image = Resource1.Add_Person1, Dock = DockStyle.Right, Size = new Size(23, 23), SizeMode = PictureBoxSizeMode.StretchImage };
            setCloserButton = new PictureBox { Image = Resource1.Cut, Dock = DockStyle.Right, Size = new Size(23, 23), SizeMode = PictureBoxSizeMode.StretchImage };
            serversPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 0 };
            headerPanel = new Panel { Dock = DockStyle.Top, Height = 30 }; // Assuming height of 30, adjust as needed
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
            this.ResizeRedraw = true;

            UpdateLabel();
            this.Location = FindMidpointForSectionControls(section, panel);
            this.Servers = servers;
            this.BringToFront();
        }

        private void SetToCloserButton_Click(object? sender, EventArgs e)
        {
            if (this.closerPanelOpen == false)
            {
                PictureBox pbCut = new PictureBox { Size = new Size((this.Width / 4), (this.Width / 4)), Image = Resource1.Scissors__Copy, SizeMode = PictureBoxSizeMode.StretchImage };
                pbCut.Click += Cut_Click;
                PictureBox pbPre = new PictureBox { Size = new Size((this.Width / 4), (this.Width / 4)), Image = Resource1.PreLetters, SizeMode = PictureBoxSizeMode.StretchImage };
                pbPre.Click += Pre_Click;
                PictureBox pbClose = new PictureBox { Size = new Size((this.Width / 4), (this.Width / 4)), Image = Resource1.ClsLetters, SizeMode = PictureBoxSizeMode.StretchImage };
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

        private void SectionControl_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 10))
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
            this.Section.IsCloser = false;
            this.Section.IsPre = false;
            this.setCloserButton.Image = Resource1.Scissors__Copy;
            if (this.closerPanelOpen == true)
            {
                closerPanel.Controls.Clear();
                closerPanel.Height = 0;

            }
            closerPanelOpen = !closerPanelOpen;
        }
        private void Close_click(object sender, EventArgs e)
        {
            this.Section.IsCloser = true;
            this.Section.IsPre = false;
            this.setCloserButton.Image = Resource1.ClsLetters;
            if (this.closerPanelOpen == true)
            {
                closerPanel.Controls.Clear();
                closerPanel.Height = 0;

            }
            closerPanelOpen = !closerPanelOpen;
        }
        private void Pre_Click(object sender, EventArgs e)
        {
            this.Section.IsCloser = false;
            this.Section.IsPre = true;
            this.setCloserButton.Image = Resource1.PreLetters;
            if (this.closerPanelOpen == true)
            {
                closerPanel.Controls.Clear();
                closerPanel.Height = 0;

            }
            closerPanelOpen = !closerPanelOpen;
        }
        private void AssignServerButton_Click(object sender, EventArgs e)
        {
            if (serverPanelOpen == false)
            {
                serversPanel.Controls.Clear();
                foreach (var server in Servers)
                {
                    var serverButton = new Button { Text = server.Name, Tag = server, Dock = DockStyle.Top };
                    serverButton.Click += ServerButton_Click;
                    serversPanel.Controls.Add(serverButton);
                }
                serversPanel.Height = Servers.Count * 30;
            }
            if (serverPanelOpen == true)
            {
                serversPanel.Controls.Clear();
                serversPanel.Height = 0;
               
            }
            serverPanelOpen = !serverPanelOpen;

        }

        private void ServerButton_Click(object sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            var assignedServer = (Server)clickedButton.Tag;

            Section.Server = assignedServer;
            UpdateLabel();
            serversPanel.Height = 0;
        }

        public void UpdateLabel()
        {
            //sectionLabel.Text = Section.Server == null ? Section.Name : Section.Server.AbbreviatedName;
            //if (Section.IsCloser)
            //{
            //    sectionLabel.Text = sectionLabel.Text + " " + "(CLS)";
            //}
            sectionLabel.Text = Section.GetDisplayString();
            headerPanel.BackColor = Section.Color; // Assuming the Section class has a Color property
            headerPanel.ForeColor = Section.FontColor;
        }
        public static void DrawSectionLabelForPrinting(Graphics g, SectionControl control)
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

        private void InitializeComponent()
        {

        }

        private Point FindMidpointForSectionControls(Section targetSection, Panel panel)
        {
            // 1. Collect all the TableControl controls with a Section
            List<TableControl> tableControlsWithSection = new List<TableControl>();
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is TableControl tableControl && tableControl.Section != null)
                {
                    tableControlsWithSection.Add(tableControl);
                }
            }

            // 2. Group by Section and find the controls with the targetSection
            var targetControls = tableControlsWithSection
                .Where(tc => tc.Section.Equals(targetSection))
                .ToList();

            // 3. Compute the midpoint for the target controls
            return TableControl.FindMiddlePoint(targetControls);
        }

    }


}
