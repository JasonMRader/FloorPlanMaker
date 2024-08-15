using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using NetTopologySuite.Triangulate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace FloorplanUserControlLibrary
{
    public partial class SectionInfoPanel : UserControl, ISectionObserver
    {
        private ToolTip toolTip = new ToolTip();
        private Section _section { get; set; }
        public Section Section { get { return _section; } }
        public event Action<Section> SectionSelected;
        public event EventHandler ServerRemoved;
        public event EventHandler ServerAdded;
        private Floorplan floorplan { get; set; }
        public SectionInfoPanel(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            this._section = section;
            this.floorplan = floorplan;
            this._section.SubscribeObserver(this);
            this.BackColor = Section.Color;
            this.ForeColor = Section.FontColor;
            pnlMainContainer.BackColor = this.BackColor;

            UpdateSection(section);
            AttachClickEventToControls(this);

            toolTip.SetToolTip(picSetTeamWait, "Set To TeamWait [T]");

            toolTip.SetToolTip(lblCovers, "Difference from Average Covers");
            toolTip.SetToolTip(lblSales, "Estimated Sales");
            toolTip.SetToolTip(lblSalesDif, "Difference from Average Sales");


        }
        private void AttachClickEventToControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.Click += Control_Click;

                if (control.HasChildren)
                {
                    AttachClickEventToControls(control);
                }
            }
        }

        private void Control_Click(object? sender, EventArgs e)
        {
            SectionSelected?.Invoke(_section);
        }

        public void UpdateSection(Section section)
        {
            UpdateLabels();
            if (section.IsSelected)
            {
                SetToSelected();
            }
            else
            {
                SetToNotSelected();
            }
        }
        public void SetToSelected()
        {
            this.Margin = new Padding(25, 0,0,0);
            this.BackColor = Color.FromArgb(255, 103, 0);
            this.Size = new System.Drawing.Size(291, 65);
            //this.pnlHighlightBuffer.Location = new System.Drawing.Point(10,10);
        }
        public void SetToNotSelected()
        {
            this.Margin = new Padding(30,0,0,0);
            this.BackColor = Color.WhiteSmoke;
            this.Size = new System.Drawing.Size(281, 65);
            //this.pnlHighlightBuffer.Location = new System.Drawing.Point(5, 5);
        }
        private void SetForPickup()
        {
            lblSectionNumber.Text = "PU";
            picSetTeamWait.Visible = false;

            lblCovers.Text = Section.MaxCovers.ToString("F0");
            lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(Section.ExpectedTotalSales);
            lblSalesDif.Visible = false;
        }
        public void UpdateLabels()
        {
            if (Section.IsPickUp)
            {
                SetForPickup();
                return;
            }
            if (Section.IsBarSection)
            {
                SetForBarSection();
            }
            else
            {
                SetForNormalSection();
            }
            SetTeamWaitPictureBoxes();
            UpdateSalesAndCovers();
            SetServerButtons();
        }

        private void SetForNormalSection()
        {
            lblSectionNumber.Text = $"#{Section.Number}";
        }

        private void SetForBarSection()
        {
            lblSectionNumber.Text = "BAR";
            Label label = new Label()
            {
                Text = $"{Section.ServerCount} Bartenders",
                Size = flowServers.Size,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            if (Section.ServerCount == 1)
            {
                label.Text = "1 Bartender";
            }
            flowServers.Controls.Add(label);
        }

        private void SetServerButtons()
        {
            int buttonHeight = flowServers.Height;
            int buttonWidth = flowServers.Width / this.Section.ServerCount;
            for (int i = 0; i < this.Section.ServerCount; i++)
            {
                Server server = null;
                if(Section.ServerTeam.Count > i)
                {
                    server = this._section.ServerTeam[i];
                }
                Button button = CreateServerButton(server, buttonHeight, buttonWidth);
                flowServers.Controls.Add(button);
            }
        }

        private Button CreateServerButton(Server server, int buttonHeight, int buttonWidth)
        {
            Button button = new Button();
            button.Size = new Size(buttonWidth, buttonHeight);
            button.Font = UITheme.SmallerFont;
            button.Margin = new Padding(0);
            if (server == null)
            {
                SetButtonToUnassigned(button);
            }
            else
            {
                SetButtonToServer(button, server);
            }
            return button;
        }
        private void SetButtonToUnassigned(Button button)
        {
            UITheme.FormatMainButton(button);
            button.Text = "Empty";
            button.Tag = null;
            button.Click += AssignServer;
        }
        private void SetButtonToServer(Button button, Server server)
        {
            UITheme.FormatCTAButton(button);
            if (!server.isDouble)
            {
                button.Text = server.ToString();
            }
            else
            {
                button.Text = server.ToString() + "*";
            }

            button.Tag = server;
            button.Click += RemoveServer;
        }
        private void RemoveServer(object? sender, EventArgs e)
        {
            ServerRemoved?.Invoke(sender, e);
        }

        private void AssignServer(object? sender, EventArgs e)
        {
            ServerAdded?.Invoke(sender, e);
        }

        public void SetTeamWaitPictureBoxes()
        {
            if (this.Section.IsTeamWait)
            {
                picSetTeamWait.BackColor = UITheme.WarningColor;
                picSetTeamWait.Image = Resources.waiters;
                toolTip.SetToolTip(lblSales, $"Estimated Sales Per Server: {Section.AverageSalesDisplay()}\n" +
                    $"Total Section Sales: {Section.ExpectedSalesDisplay()}");
            }
            else
            {
                picSetTeamWait.BackColor = UITheme.MainColor;
                picSetTeamWait.Image = Resources.waiter;
                toolTip.SetToolTip(lblSales, "Estimated Sales");
            }
        }
        private void UpdateSalesAndCovers()
        {
            if (this.Section.IsPickUp) { return; }
            lblCovers.Text = floorplan.GetCoverDifferenceForSection(Section).ToString("F0");
            lblSales.Text = Section.AverageSalesDisplay();
            if (floorplan.GetSalesDifferenceForSection(Section) >= 0)
            {
                lblSalesDif.Text =
                     Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
                lblSalesDif.BackColor = Color.LightGreen;
                lblSalesDif.ForeColor = Color.Black;
            }
            else
            {
                lblSalesDif.Text =
                     Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
                lblSalesDif.BackColor = Color.Pink;
                lblSalesDif.ForeColor = Color.Black;
            }
        }
    }
}
