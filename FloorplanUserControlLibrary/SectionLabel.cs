using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FloorplanUserControlLibrary
{
    public partial class SectionLabel : UserControl, ISectionObserver, IFloorplanObserver
    {
        private Section _section { get; set; }
        private Floorplan _floorplan { get; set; }
        public Section Section { get { return _section; } }
        public Floorplan Floorplan { get { return _floorplan; } }
        private Point MouseDownLocation;
        private bool isDragging = false;
        public event Action<Section> SectionSelected;
        public event EventHandler ServerRemoved;
        public event EventHandler ServerAdded;
        private int defaultWidth = 0;
        private int defaultHeight = 0;
        private int defaultAccentWidth = 0;
        private int defaultAccentHeight = 0;
        private int selectedSizeIncrease = 8;
        private Point nonSelectedLocation = new Point(0, 0);
        private bool isSetToSelected = false;
        private List<Button> serverButtons = new List<Button>();
        private int widthOfControls
        {
            get
            {
                return lblSectionNumber.Width + flowServers.Width + picCutOrder.Width + 5;
            }
        }
        private Point selectedLocation
        {
            get { return new Point(nonSelectedLocation.X - 5, nonSelectedLocation.Y - 5); }
        }
        public SectionLabel(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            _section = section;
            _floorplan = floorplan;
            _section.SubscribeObserver(this);
            _floorplan.SubscribeObserver(this);
            AssignClickEvents();
            UpdateControlsForSection();
            this.Width = widthOfControls + 16;
            this.defaultHeight = this.Height;
            this.defaultWidth = this.Width;
            pnlAccent.Width = widthOfControls + 16;
            this.defaultAccentHeight = pnlAccent.Height;
            this.defaultAccentWidth = pnlAccent.Width;
            this.Location = new Point(section.MidPoint.X - (this.Width / 2),
               section.MidPoint.Y - (this.Height / 2));
            nonSelectedLocation = this.Location;
            SetSelectedStatus();


        }

        private void SetSelectedStatus()
        {
            if (_section.IsSelected)
            {
                SetToSelected();
            }
            else
            {
                SetToNotSelected();
            }
        }

        public void UpdateControlsForSection()
        {

            SetSectionLabel();
            SetCutOrderImage();

            SetServerButtons();


            pnlAccent.BackColor = Section.FontColor;
            flowServers.BackColor = Section.Color;
            flowMainContainer.BackColor = Section.Color;
            this.BackColor = Section.Color;
        }
        private bool ServersHaveChanged()
        {
            List<Server> servers = new List<Server>();
            foreach (Button button in serverButtons)
            {
                if (button.Tag is Server server)
                {
                    servers.Add(server);
                }
            }
            if (servers != _section.ServerTeam)
            {
                return true;
            }
            return false;
        }

        private void SetServerButtons()
        {
            serverButtons.Clear();
            flowServers.Controls.Clear();
            for (int i = 0; i < _section.ServerCount; i++)
            {
                Server server = null;
                if (Section.ServerTeam.Count > i)
                {
                    server = this._section.ServerTeam[i];
                }
                Button button = CreateServerButton(server);
                serverButtons.Add(button);
                flowServers.Controls.Add(button);
            }

        }

        private Button CreateServerButton(Server? server)
        {
            Button button = new Button();
            button.MinimumSize = new Size(80, 27);
            button.AutoSize = true;

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
            button.ForeColor = Color.Black;
            button.Font = UITheme.MainFont;
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
            button.BackColor = Section.Color;
            button.ForeColor = Section.FontColor;
            button.Font = UITheme.MainFont;
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
        private void SetSectionLabel()
        {
            if (_section.IsPickUp)
            {
                lblSectionNumber.Text = "PU";
            }
            else if (_section.IsBarSection)
            {
                lblSectionNumber.Text = "B";
            }
            else
            {
                lblSectionNumber.Text = "#" + _section.Number.ToString();
            }
        }
        private void AssignClickEvents()
        {
            this.MouseDown += SectionLabel_MouseDown;
            this.MouseMove += SectionLabel_MouseMove;
            this.MouseUp += SectionLabel_MouseUp;
            pnlAccent.MouseDown += SectionLabel_MouseDown;
            pnlAccent.MouseUp += SectionLabel_MouseUp;
            pnlAccent.MouseMove += SectionLabel_MouseMove;
            flowMainContainer.MouseDown += SectionLabel_MouseDown;
            flowMainContainer.MouseMove += SectionLabel_MouseMove;
            flowMainContainer.MouseUp += SectionLabel_MouseUp;
            lblSectionNumber.MouseUp += SectionLabel_MouseUp;
            lblSectionNumber.MouseMove += SectionLabel_MouseMove;
            lblSectionNumber.MouseDown += SectionLabel_MouseDown;
            this.Click += SelectSection;
            pnlAccent.Click += SelectSection;
            flowMainContainer.Click += SelectSection;
            lblSectionNumber.Click += SelectSection;

            picCutOrder.Click += CycleCutOrder;

        }

        private void SelectSection(object? sender, EventArgs e)
        {
            SectionSelected?.Invoke(_section);
        }

        private void CycleCutOrder(object? sender, EventArgs e)
        {
            if (this._section.IsCloser)
            {
                this._section.SetToPre();
            }
            else if (this._section.IsPre)
            {
                this._section.SetToCut();
            }
            else
            {
                this._section.SetToCut();
            }
            SetCutOrderImage();
        }

        private void SetCutOrderImage()
        {
            if (this._section.IsCloser)
            {
                this.picCutOrder.Image = Resources.Close;
            }
            else if (this._section.IsPre)
            {
                this.picCutOrder.Image = Resources.Pre;
            }
            else
            {
                this.picCutOrder.Image = Resources.Scissors__Copy;
            }
        }

        private void SectionLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true; // Start the drag action
                MouseDownLocation = e.Location;
            }
        }

        private void SectionLabel_MouseMove(object sender, MouseEventArgs e)
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

        private void SectionLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false; // End the drag action
                if (_section.IsSelected)
                {
                    this.nonSelectedLocation = new Point(this.Left + (selectedSizeIncrease / 2), this.Top + (selectedSizeIncrease / 2));
                }
                else
                {
                    this.nonSelectedLocation = this.Location;
                }

            }
        }
        private void btnServerButton_Click(object sender, EventArgs e)
        {
            SectionSelected?.Invoke(_section);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Section.RemoveObserver(this);
            }
            base.Dispose(disposing);
        }

        public void UpdateSection(Section section)
        {
            if (ServersHaveChanged())
            {
                UpdateControlsForSection();
            }


            if (section.IsSelected && !isSetToSelected)
            {
                SetToSelected();
            }
            else if (!section.IsSelected && isSetToSelected)
            {
                SetToNotSelected();
            }
        }

        private void SetToNotSelected()
        {
            flowMainContainer.Width = widthOfControls;
            isSetToSelected = false;
            this.Width = defaultWidth;
            this.Height = defaultHeight;
            this.Location = nonSelectedLocation;
            pnlAccent.Width = defaultAccentWidth;
            pnlAccent.Height = defaultAccentHeight;
            flowMainContainer.Location = new Point(2, 2);
            pnlAccent.BackColor = Color.Gray;
        }
        //152, 39
        //Accent 144, 31 | 4,4
        //Main Container 140, 27 | 2,2

        private void SetToSelected()
        {
            flowMainContainer.Width = widthOfControls;
            isSetToSelected = true;
            this.AutoSize = false;
            pnlAccent.AutoSize = false;
            this.Width = defaultWidth + selectedSizeIncrease;
            this.Height = defaultHeight + selectedSizeIncrease;
            this.Location = selectedLocation;

            pnlAccent.Width = defaultAccentWidth + selectedSizeIncrease;
            pnlAccent.Height = defaultAccentHeight + selectedSizeIncrease;
            int accentWidth = pnlAccent.Width;
            int accentHeight = pnlAccent.Height;
            int mainWidth = flowMainContainer.Width;
            int mainHeight = flowMainContainer.Height;
            pnlAccent.BackColor = _section.FontColor;
            int mainContainerX = (pnlAccent.Width - flowMainContainer.Width) / 2;
            int mainContainerY = (pnlAccent.Height - flowMainContainer.Height) / 2;
            flowMainContainer.Location = new Point(6, 6);
        }

        public void UpdateFloorplan(Floorplan floorplan)
        {

        }

        private void pnlAccent_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
