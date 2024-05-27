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
//using static System.Collections.Specialized.BitVector32;


namespace FloorplanUserControlLibrary
{
    public partial class SectionPanelControl : UserControl, ISectionObserver
    {
        public event EventHandler CheckBoxChanged;
        public event EventHandler picEraseSectionClicked;
        public event EventHandler picTeamWaitClicked;
        public event EventHandler picAddServerClicked;
        public event EventHandler picSubtractServerClicked;
        public event EventHandler unassignedSpotClicked;
        public event EventHandler ServerRemoved;
        public event Action<Section> SectionRemoved;
        public event Action<Section> SectionAdded;
        private ToolTip toolTip = new ToolTip();
        private List<Label> serverLabels = new List<Label>();
        private List<PictureBox> removeServerPBs = new List<PictureBox>();
        
        private Floorplan floorplan { get; set; }
        public bool CheckBoxChecked
        {
            get { return cbSectionSelect.Checked; }
            set { cbSectionSelect.Checked = value; }
        }

        public SectionPanelControl(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            this.Section = section;
            this.Section.SubscribeObserver(this);
            this.BackColor = Section.Color;
            this.ForeColor = Section.FontColor;
            this.floorplan = floorplan;

            UpdateLabels();
           
            toolTip.SetToolTip(picSetTeamWait, "Set To TeamWait");
            toolTip.SetToolTip(picClearSection, "Clear Section");
            toolTip.SetToolTip(picPlusOneServer, "Add Server Spot");
            toolTip.SetToolTip(picMinusOneServer, "Remove Server Spot");
            toolTip.SetToolTip(lblCovers, "Difference from Average Covers");
            toolTip.SetToolTip(lblSales, "Difference from Average Sales");
            toolTip.SetToolTip(cbSectionSelect, "Press TAB to cycle through Sections");

        }
        public SectionPanelControl(Section section, FloorplanTemplate floorplan)
        {
            InitializeComponent();
            this.Section = section;
            this.Section.SubscribeObserver(this);
            this.BackColor = Section.Color;
            this.ForeColor = Section.FontColor;
            //this.floorplan = floorplan;

            UpdateLabels();
           
            toolTip.SetToolTip(picSetTeamWait, "Set To TeamWait");
            toolTip.SetToolTip(picClearSection, "Clear Section");
            toolTip.SetToolTip(picPlusOneServer, "Add Server Spot");
            toolTip.SetToolTip(picMinusOneServer, "Remove Server Spot");
            toolTip.SetToolTip(lblCovers, "Difference from Average Covers");
            toolTip.SetToolTip(lblSales, "Difference from Average Sales");
            toolTip.SetToolTip(cbSectionSelect, "Press TAB to cycle through Sections");


        }
        public Section Section { get; set; }
        //public Floorplan Floorplan { get; set; }
        public void UpdateSection(Section section)
        {
            UpdateLabels();
            if (section.IsSelected)
            {
                this.CheckBoxChecked = true;
            }

            //SetTeamWaitPictureBoxes();


        }
        private void picClearSection_Click(object sender, EventArgs e)
        {
            picEraseSectionClicked?.Invoke(this, e);
        }

        private void picTeamWait_Click(object sender, EventArgs e)
        {
            picTeamWaitClicked?.Invoke(this, e);
        }

        public void UpdateLabels()
        {
           
            if (Section.IsPickUp)
            {
                this.Height = 25;
                cbSectionSelect.Text = "Pick-up";
                picSetTeamWait.Visible = false;
                picClearSection.Image = Resources.Trash;
                toolTip.SetToolTip(picClearSection, "Delete Section");
                lblCovers.Text = Section.MaxCovers.ToString("F0");
                lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(Section.AverageSales);
                return;
            }
           
            cbSectionSelect.Text = "Section " + this.Section.Number.ToString();
            UpdateSalesAndCovers();

            if (this.Section.Server != null && Section.IsTeamWait == false)
            {
                if (this.Section.Server.isDouble)
                {
                    lblDisplay.Text = Section.Server.Name + " (Dbl)";
                }
                else
                {
                    lblDisplay.Text = Section.Server.Name;
                }
                lblDisplay.BackColor = Section.Color;
                lblDisplay.Click -= unassignedLabel_Click;
                picClearSection.Image = Resources.erase;
                toolTip.SetToolTip(picClearSection, "Clear Section");
            }
            else if(this.Section.Server == null && Section.IsTeamWait == false)
            {
                lblDisplay.Text = "Unassigned";
                lblDisplay.BackColor = UITheme.ButtonColor;
                lblDisplay.Click += unassignedLabel_Click;
                if (this.Section.IsEmpty())
                {
                    picClearSection.Image = Resources.Trash;
                    toolTip.SetToolTip(picClearSection, "Delete Section");
                }
            }
            else if (this.Section.IsTeamWait)
            {
                foreach (Label label in serverLabels) { label.Tag = null; label.Text = "Unassigned"; };
                foreach (PictureBox pb in removeServerPBs) { pb.Tag = null; };
                lblDisplay.Text = this.Section.ServerCount.ToString() + " Team Section";
                lblDisplay.BackColor = Section.Color;
                toolTip.SetToolTip(picClearSection, "Clear Section");
                if (this.serverLabels.Count == 0) { return; }
                for (int i = 0; i < this.Section.ServerTeam.Count; i++)
                {
                    SetLabelToAssigned(i);
                    //serverLabels[i].Text = Section.ServerTeam[i].Name;
                    //serverLabels[i].Tag = Section.ServerTeam[i];
                    removeServerPBs[i].Tag = Section.ServerTeam[i];

                }
                
            }
            
        }
        public void UpdateSalesAndCovers()
        {
            if(this.Section.IsPickUp) { return; }
            lblCovers.Text = floorplan.GetCoverDifferenceForSection(Section).ToString("F0");
            if (floorplan.GetSalesDifferenceForSection(Section) > 0)
            {
                lblSales.Text = "+" + Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
            else
            {
                lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
        }
        public void PickUpSectionAdjusted()
        {
            lblCovers.Text = floorplan.GetCoverDifferenceForSection(Section).ToString("F0");
            if (floorplan.GetSalesDifferenceForSection(Section) > 0)
            {
                lblSales.Text = "+" + Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
            else
            {
                lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
        }
        public void AddServerRow()
        {

            this.Height += 25;
            Label lblNewServerName = CreateUnassignedLabel();
            this.Controls.Add(lblNewServerName);
            this.serverLabels.Add(lblNewServerName);
            PictureBox RemoveServer = new PictureBox()
            {
                Size = picClearSection.Size,
                Location = new Point(lblNewServerName.Right, lblNewServerName.Top),
                Image = Resources.Trash,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = UITheme.WarningColor

            };
            RemoveServer.Click += RemoveServer_Click;
            this.removeServerPBs.Add(RemoveServer);
            this.Controls.Add(RemoveServer);




            //this.Invalidate();
        }
        private Label CreateUnassignedLabel()
        {
            Label lblNewServerName = new Label()
            {
                Text = "Unassigned",
                BackColor = UITheme.ButtonColor,
                Size = new Size(285, 25),
                Location = new Point(0, this.Height - 25),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = lblDisplay.Font
            };
            lblNewServerName.Click += unassignedSpotClicked;
            return lblNewServerName;
        }
        private void SetLabelToAssigned(int index)
        {
            
            Server server = Section.ServerTeam[index];
            Label label = serverLabels[index];
            label.Click -= unassignedSpotClicked;
            PictureBox removeBox = removeServerPBs[index];
            if(server != null)
            {
                label.BackColor = this.Section.Color;
                label.Text = server.Name;
                if (this.Section.Server.isDouble)
                {
                    lblDisplay.Text = Section.Server.Name + "(Double)";
                }
                label.Tag = server;
                removeBox.Tag = server;
                
            }
            else
            {
                label.BackColor = UITheme.ButtonColor;
                label.Text = "Unassigned";
                label.Tag = null;
                removeBox.Tag = null;
                label.Click += unassignedSpotClicked;
            }
           
           
            
        }
        private void unassignedLabel_Click(object sender, EventArgs e)
        {
            unassignedSpotClicked?.Invoke(this.Section, e);
        }
        private void RemoveServer_Click(object? sender, EventArgs e)
        {
           
            PictureBox clickedBox = sender as PictureBox;
            Server serverToRemove = (Server)clickedBox.Tag;
            this.Section.RemoveServer(serverToRemove);
            clickedBox.Tag = null;
            ServerRemoved?.Invoke(this.Section, e);
            foreach (Label label in serverLabels)
            {
                if (label.Tag == serverToRemove)
                {
                    label.BackColor = UITheme.ButtonColor;
                    label.Text = "Unassigned";
                    label.Tag = null;
                    clickedBox.Tag = null;
                    label.Click += unassignedSpotClicked;
                }
            }
            UpdateLabels();
        }

       
        public void RemoveServerRow()
        {

            this.Height -= 25;
            this.serverLabels.RemoveAt(serverLabels.Count - 1);
            this.removeServerPBs.RemoveAt(serverLabels.Count - 1);


        }
        public void SetToTeamWait()
        {
            lblDisplay.Text = this.Section.ServerCount.ToString() + " Team Section";
            lblDisplay.Width = 245;
            //this.Height += 25;
            AddServerRow();
            AddServerRow();



            //AddServerRow();
            picMinusOneServer.Visible = true;
            picMinusOneServer.Click += picDecreaseServerCount_Click;
            picPlusOneServer.Visible = true;
            picPlusOneServer.Click += picIncreaseServerCount_Click;

            picSetTeamWait.BackColor = UITheme.WarningColor;
            picSetTeamWait.Image = Resources.waiters;
            toolTip.SetToolTip(picSetTeamWait, "Set To Solo");
        }



        private void SetToSolo()
        {
            //lblDisplay.Text = this.Section.ServerCount.ToString() + " Team Section";
            lblDisplay.Width = 325;
            this.Height = 50;
            picMinusOneServer.Visible = false;
            picPlusOneServer.Visible = false;
            picPlusOneServer.Click -= picIncreaseServerCount_Click;
            picMinusOneServer.Click -=picDecreaseServerCount_Click;
            picSetTeamWait.BackColor = UITheme.CTAColor;
            picSetTeamWait.Image = Resources.waiter;
            toolTip.SetToolTip(picSetTeamWait, "Set To TeamWait");
            this.Invalidate();
        }
        public void SetTeamWaitPictureBoxes()
        {
            if (this.Section.IsTeamWait)
            {
                SetToTeamWait();
            }
            else
            {
                SetToSolo();
            }
        }
        private void picIncreaseServerCount_Click(object sender, EventArgs e)
        {
            picAddServerClicked?.Invoke(this, e);

        }
        private void picDecreaseServerCount_Click(object sender, EventArgs e)
        {
            
            picSubtractServerClicked?.Invoke(this, e);

        }
        


        public void CheckBox()
        {
            this.cbSectionSelect.Checked = true;
        }
        public void UnCheckBox()
        {
            this.cbSectionSelect.Checked = false;
        }
        public bool isChecked()
        {
            return this.cbSectionSelect.Checked;
        }
        private void cbSectionSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxChanged?.Invoke(this, e);

        }
        
    }
}
