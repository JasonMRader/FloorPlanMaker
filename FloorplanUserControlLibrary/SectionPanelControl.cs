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
            lblDisplay.Text = "Unassigned";
            cbSectionSelect.Text = "Section " + this.Section.Number.ToString();
            lblCovers.Text = floorplan.GetCoverDifferenceForSection(Section).ToString("F0");
            //lblCovers.Text = (this.Section.MaxCovers - floorplan.MaxCoversPerServer).ToString("F0");
            if (floorplan.GetSalesDifferenceForSection(Section) > 0)
            {
                lblSales.Text = "+" + Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }
            else
            {
                lblSales.Text = Section.FormatAsCurrencyWithoutParentheses(floorplan.GetSalesDifferenceForSection(Section));
            }

            if (this.Section.Server != null && Section.IsTeamWait == false)
            {
                lblDisplay.Text = Section.Server.Name;
            }
            else if (this.Section.IsTeamWait)
            {
                lblDisplay.Text = this.Section.ServerCount.ToString() + " Team Section";
                if (this.serverLabels.Count == 0) { return; }
                for (int i = 0; i < this.Section.ServerTeam.Count; i++)
                {
                    serverLabels[i].Text = Section.ServerTeam[i].Name;
                    serverLabels[i].Tag = Section.ServerTeam[i];
                    removeServerPBs[i].Tag = Section.ServerTeam[i];

                }
                //int i = 0;
                //foreach (Label label in serverLabels)
                //{
                //    label.Text = Section.ServerTeam[i].Name;
                //}
                //foreach (Server server in this.Section.ServerTeam)
                //{
                //    lblDisplay.Text += server.AbbreviatedName + ", ";
                //}
                //AddServerRow();
            }


        }
        private void AddServerRow()
        {

            this.Height += 25;
            Label lblNewServerName = new Label()
            {
                Text = "Unassigned",

                Size = new Size(285, 25),
                Location = new Point(0, this.Height - 25),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = lblDisplay.Font
            };
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

        private void RemoveServer_Click(object? sender, EventArgs e)
        {
            PictureBox clickedBox = sender as PictureBox;
            Server serverToRemove = (Server)clickedBox.Tag;
            this.Section.RemoveServer(serverToRemove);
            clickedBox.Tag = null;
            foreach(Label label in serverLabels)
            {
                if (label.Tag == serverToRemove)
                {
                    label.Tag = null;
                    label.Text = "Unassigned";
                }
            }
        }

        private void RefreshAndAddServerRow()
        {

        }
        private void RemoveServerRow()
        {
          
            this.Height -= 25;
            this.serverLabels.RemoveAt(serverLabels.Count - 1);
            this.removeServerPBs.RemoveAt(serverLabels.Count - 1);


        }
        private void SetToTeamWait()
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
        }



        private void SetToSolo()
        {
            //lblDisplay.Text = this.Section.ServerCount.ToString() + " Team Section";
            lblDisplay.Width = 325;
            this.Height = 50;
            picMinusOneServer.Visible = false;
            picPlusOneServer.Visible = false;
            picPlusOneServer.Click -= picIncreaseServerCount_Click;
            picSetTeamWait.BackColor = UITheme.CTAColor;
            picSetTeamWait.Image = Resources.waiter;
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
            this.Section.IncreaseServerCount();
            AddServerRow();
        }
        private void picDecreaseServerCount_Click(object sender, EventArgs e)
        {
            bool serverRemoved = this.Section.DecreaseServerCount();
            if (serverRemoved)
            {
                if (this.Section.ServerCount == 1)
                {
                    this.Section.MakeSoloSection();
                    SetToSolo();
                    return;
                }
                RemoveServerRow();
            }

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
