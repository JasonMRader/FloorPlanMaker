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
        private Button assignServerButton;
        private FlowLayoutPanel serversPanel;
        private Panel headerPanel;
        private Point MouseDownLocation;
        private bool isDragging = false; // Indicates whether dragging is ongoing
        public Section Section { get; set; }

        public List<Server> Servers { get; set; } = new List<Server>();

        public SectionControl(Section section)
        {
            this.Section = section;

            sectionLabel = new Label { Dock = DockStyle.Fill, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, 
            Font = new Font("Segoe UI", 12f, FontStyle.Bold)};
            assignServerButton = new Button { Text = "+", Dock = DockStyle.Right, Size = new Size(23,23), FlatStyle = FlatStyle.Flat };
            serversPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom , Height = 0};
            headerPanel = new Panel { Dock = DockStyle.Top, Height = 30 }; // Assuming height of 30, adjust as needed
            this.Height = 30;
            this.AutoSize = true;
            this.Padding = new Padding(5); // Adjust this value based on your desired border thickness.

            assignServerButton.Click += AssignServerButton_Click;
            //this.BorderStyle = BorderStyle.FixedSingle;
            this.Paint += SectionControl_Paint;
            headerPanel.Controls.Add(assignServerButton);
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

        private void AssignServerButton_Click(object sender, EventArgs e)
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
            SizeF textSize = g.MeasureString(control.Section.Server.AbbreviatedName, boldLargeFont);
            if (control.Section.IsCloser)
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
