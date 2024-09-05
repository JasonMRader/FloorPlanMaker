using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
//using PdfSharp.Charting;
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
        public event Action<Section> AssignPickUp;
        public event Action<Section> SectionSelected;
        public event EventHandler ServerRemoved;
        public Action<Section, Floorplan> ShowServerList;
        public event EventHandler ServerAdded;

        private int defaultWidth = 0;
        private int defaultHeight = 0;
        private int defaultAccentWidth = 0;
        private int defaultAccentHeight = 0;
        private int parentDefaultPadding = 0;
        private int accentDefaultPadding = 3;
        private int parentSelectedPadding = 4;
        private int accentSelectedPadding = 5;
        private int selectedSizeIncrease
        {
            get { return parentSelectedPadding + accentSelectedPadding; }
        }
        private Point nonSelectedLocation = new Point(0, 0);
        private bool isSetToSelected = false;
        private List<Button> serverButtons = new List<Button>();
        private int widthOfControls
        {
            get
            {
                return lblSectionNumber.Width + flowServers.Width + picCutOrder.Width;
            }
        }
        private Point selectedLocation
        {
            get { return new Point(nonSelectedLocation.X - (selectedSizeIncrease / 2), nonSelectedLocation.Y - (selectedSizeIncrease / 2)); }
        }
        private ServerSelectionPanel serverSelectionPanel { get; set; }
        public SectionLabel(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            _section = section;
            _floorplan = floorplan;
            _section.SubscribeObserver(this);
            _floorplan.SubscribeObserver(this);
            AssignClickEvents();
            UpdateControlsForSection();

            //this.defaultHeight = this.Height;
            //this.defaultWidth = this.Width;
            //pnlAccent.Width = widthOfControls + 16;
            //this.defaultAccentHeight = pnlAccent.Height;
            //this.defaultAccentWidth = pnlAccent.Width;
            this.Location = new Point(section.MidPoint.X - (this.Width / 2),
               section.MidPoint.Y - (this.Height / 2));
            nonSelectedLocation = this.Location;
            SetSelectedStatus();



        }
        public void ShowServerSelectionPanel(Button button)
        {
            if (serverSelectionPanel == null)
            {
                serverSelectionPanel = new ServerSelectionPanel(_section, _floorplan);
                serverSelectionPanel.Visible = false;
            }
            serverSelectionPanel.Dock = DockStyle.Bottom;
            serverSelectionPanel.SetButton(button);
            flowParent.Controls.Add(serverSelectionPanel);
            this.BringToFront();
            serverSelectionPanel.BringToFront();

        }
        public void CloseServerSelectionPanel()
        {
            if (serverSelectionPanel != null)
            {
                if (serverSelectionPanel.Visible)
                {
                    serverSelectionPanel.Visible = false;
                }
            }
        }

        private void SetSelectedStatus()
        {
            if (_section.IsSelected)
            {
                
                SetToSelected();
               
                
                this.Invalidate();
            }
            else if (!_section.IsSelected)
            {
                
                SetToNotSelected();
                this.Invalidate();
            }
            

        }

        public void UpdateControlsForSection()
        {

            SetSectionLabel();
            SetCutOrderImage();

            SetServerButtons();

            lblSectionNumber.ForeColor = _section.FontColor;
            flowAccent.BackColor = Color.Black;
            flowServers.BackColor = Section.Color;
            flowMainContainer.BackColor = Section.Color;
            this.flowParent.BackColor = UITheme.SelectedColor;
            if (serverButtons.Count > 0)
            {
                picCutOrder.Height = serverButtons[0].Height;
                picCutOrder.Width = serverButtons[0].Height;
            }

        }
        private bool ServersHaveChanged()
        {
            //Make serve3r button
            //UD
            List<Server> servers = new List<Server>();
            foreach (Button button in serverButtons)
            {
                if (button.Tag is Server server)
                {
                    servers.Add(server);
                }
                if (button.Tag == null)
                {

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
            if (!Section.IsPickUp && !Section.IsBarSection)
            {
                for (int i = 0; i < _section.ServerCount; i++)
                {
                    Server server = null;
                    if (Section.ServerTeam.Count > i)
                    {
                        server = this._section.ServerTeam[i];
                    }
                    Button button = CreateServerButton(server);
                    button.Click += SelectSection_Click;
                    button.Click += ServerButton_Click;
                    serverButtons.Add(button);
                    flowServers.Controls.Add(button);
                }
            }
            if (Section.IsPickUp)
            {
                Button button = CreatePickUpButton();

                serverButtons.Add(button);
                flowServers.Controls.Add(button);
                picCutOrder.Visible = false;
            }
            if (Section.IsBarSection)
            {
                Button button = CreateBarButton();
                serverButtons.Add(button);
                flowServers.Controls.Add(button);
                picCutOrder.Visible = false;
            }


        }



        private Button CreateBarButton()
        {
            Button button = new Button();
            button.MinimumSize = new Size(80, 27);
            button.AutoSize = true;
            UITheme.FormatCTAButton(button);

            button.BackColor = Section.Color;
            button.ForeColor = Section.FontColor;
            button.Font = UITheme.CustomFont(11f, FontStyle.Bold);
            button.Text = "Bar";
            button.Click += SelectSection_Click;
            //ADD BAR BUTTON CLICK?
            return button;
        }

        private Button CreatePickUpButton()
        {
            Button button = new Button();
            button.MinimumSize = new Size(80, 27);
            button.AutoSize = true;
            UITheme.FormatCTAButton(button);

            button.BackColor = Section.Color;
            button.ForeColor = Section.FontColor;
            button.Font = UITheme.CustomFont(11f, FontStyle.Bold);

            button.Click += SelectSection_Click;
            button.Click += AssignPickup_Click;
            button.Text = Section.GetDisplayString();
            return button;
        }

        private void AssignPickup_Click(object? sender, EventArgs e)
        {
            AssignPickUp?.Invoke(_section);
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


            button.ForeColor = Color.Black;
            button.Font = UITheme.CustomFont(11f, FontStyle.Bold);
        }
        private void SetButtonToServer(Button button, Server server)
        {

            UITheme.FormatCTAButton(button);
            button.Text = _section.GetDisplayForServer(server);
            button.BackColor = Section.Color;
            button.ForeColor = Section.FontColor;
            button.Font = button.Font = UITheme.CustomFont(11f, FontStyle.Bold);
            button.Tag = server;


        }
        
        private void ServerButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Tag != null)
            {
                Server server = (Server)button.Tag;

                this.Section.RemoveServer(server);

            }
            ShowServerSelectionPanel(button);
            serverSelectionPanel.Visible = !serverSelectionPanel.Visible;


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
            flowParent.MouseDown += SectionLabel_MouseDown;
            flowParent.MouseMove += SectionLabel_MouseMove;
            flowParent.MouseUp += SectionLabel_MouseUp;
            flowAccent.MouseDown += SectionLabel_MouseDown;
            flowAccent.MouseUp += SectionLabel_MouseUp;
            flowAccent.MouseMove += SectionLabel_MouseMove;
            flowMainContainer.MouseDown += SectionLabel_MouseDown;
            flowMainContainer.MouseMove += SectionLabel_MouseMove;
            flowMainContainer.MouseUp += SectionLabel_MouseUp;
            lblSectionNumber.MouseUp += SectionLabel_MouseUp;
            lblSectionNumber.MouseMove += SectionLabel_MouseMove;
            lblSectionNumber.MouseDown += SectionLabel_MouseDown;
            this.Click += SelectSection_Click;
            flowAccent.Click += SelectSection_Click;
            flowMainContainer.Click += SelectSection_Click;
            lblSectionNumber.Click += SelectSection_Click;
            flowParent.Click += SelectSection_Click;
            picCutOrder.Click += SelectSection_Click;
            picCutOrder.Click += CycleCutOrder_Click;

        }

        private void SelectSection_Click(object? sender, EventArgs e)
        {
            SectionSelected?.Invoke(_section);
        }

        private void CycleCutOrder_Click(object? sender, EventArgs e)
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
                this._section.SetToClose();
            }
            SetCutOrderImage();
        }

        public void SetCutOrderImage()
        {
            if (this._section.IsCloser)
            {
                this.picCutOrder.Image = Resources.CloseBlack;
                picCutOrder.BackColor = UITheme.NoColor;
            }
            else if (this._section.IsPre)
            {
                this.picCutOrder.Image = Resources.PrecloseBlack;
                picCutOrder.BackColor = UITheme.WarningColor;
            }
            else
            {
                this.picCutOrder.Image = Resources.ScissorsCircle;
                this.picCutOrder.BackColor = UITheme.YesColor;
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
                if (_section.IsSelected)
                {
                    this.nonSelectedLocation = new Point(this.Left + (selectedSizeIncrease / 2), this.Top + (selectedSizeIncrease / 2));
                }
                else
                {
                    this.nonSelectedLocation = this.Location;
                }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _section.RemoveObserver(this);
                _floorplan.RemoveObserver(this);
            }
            base.Dispose(disposing);
        }

        public void UpdateSection(Section section)
        {
            if (this.InvokeRequired) {
                this.Invoke(new Action(() => UpdateSection(section)));
            }
            else {
                if (ServersHaveChanged()) {
                    UpdateControlsForSection();
                }
                SetSelectedStatus();
                SetCutOrderImage();
            }
        }

        private void SetToNotSelected()
        {

            isSetToSelected = false;
            this.Location = nonSelectedLocation;
            flowAccent.Padding = new Padding(accentDefaultPadding);
            flowParent.Padding = new Padding(parentDefaultPadding);
            flowAccent.BackColor = Color.DarkGray;
            lblSectionNumber.BackColor = MuteColor(Section.Color);
            flowServers.BackColor = MuteColor(Section.Color);
            foreach(Button serverButton in this.serverButtons)
            {
                if (serverButton != null)
                {
                    if(serverButton.Tag != null)
                    {
                        serverButton.BackColor = MuteColor(_section.Color);
                    }
                }
            }
            CloseServerSelectionPanel();

        }
        private Color MuteColor(Color originalColor, float blendFactor = 0.3f)
        {
            // Blend the original color with gray
            int r = (int)(originalColor.R * (1 - blendFactor) + 128 * blendFactor);
            int g = (int)(originalColor.G * (1 - blendFactor) + 128 * blendFactor);
            int b = (int)(originalColor.B * (1 - blendFactor) + 128 * blendFactor);

            return Color.FromArgb(r, g, b);
        }

       
        private void SetToSelected()
        {
            this.Location = selectedLocation;
            isSetToSelected = true;
            flowParent.Padding = new Padding(parentSelectedPadding);
            flowAccent.Padding = new Padding(accentSelectedPadding);
            lblSectionNumber.BackColor = Section.Color;
            flowAccent.BackColor = Color.White;
            flowParent.BackColor = UITheme.SelectedColor;
            foreach (Button serverButton in this.serverButtons)
            {
                if (serverButton != null)
                {
                    if (serverButton.Tag != null)
                    {
                        serverButton.BackColor = _section.Color;
                    }
                }
            }
        }

        public void UpdateFloorplan(Floorplan floorplan)
        {

        }

       

        private void btnServerButton_Click(object sender, EventArgs e)
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            int alpha = _section.IsSelected ? 255 : 28; // Fully opaque when selected, semi-transparent otherwise

            // Draw the background with transparency based on selection
            using (Brush brush = new SolidBrush(Color.FromArgb(alpha, Section.Color)))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Call the base method to draw other elements like text and borders
            base.OnPaint(e);
        }

    }
}
