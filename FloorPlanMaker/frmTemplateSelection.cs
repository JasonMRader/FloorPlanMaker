

using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FloorPlanMaker
{
    public partial class frmTemplateSelection : Form
    {
        Shift ShiftManager;
        private FloorplanFormManager floorplanManager { get; set; }
        private Form1 form1Reference;
        private int serverCount;
        private bool yesTeamWaitFilter;
        private bool noTeamWaitFilter;
        private bool yesPickUpFilter;
        private bool noPickUpFilter;
        private List<SectionPanelControl> _sectionPanels;
        private DiningArea area;
        private Panel[] panels;
        private int displayedPage = 1;
        public frmTemplateSelection(FloorplanFormManager floorplanManager, DiningArea diningArea, Form1 form1Reference)
        {
            InitializeComponent();
            this.floorplanManager = floorplanManager;
            this.form1Reference = form1Reference;
            this.area = diningArea;
            floorplanManager.TemplateManager.serverCount = serverCount;
            floorplanManager.TemplateManager.DiningArea = diningArea;
            floorplanManager.UpdateTemplatesBasedOnFloorplan();
            panels = new Panel[] { panel1, panel2, panel3, panel4 };
            this.floorplanManager.TemplateManager.PreviewTemplateClicked += PreviewTemplate_Clicked;
            this.floorplanManager.TemplateManager.ApplyTemplateClicked += ApplyTemplate_Clicked;
            this.floorplanManager.TemplateManager.CancelPreviewedTemplate += CancelViewedTemplate;

        }

        private void CancelViewedTemplate(object? sender, EventArgs e)
        {
            if (floorplanManager.Shift.SelectedFloorplan != null)
            {
                floorplanManager.ResetSections();
                form1Reference.UpdateWithTemplate();
            }
        }
        // TODO: replace templates with no table 50
        // TODO: sometimes when viewing templates, the average sales are way off (SEE 3 person FP 1 Teamwait left side)
        private void ApplyTemplate_Clicked(object? sender, EventArgs e)
        {
            this.Parent.SendToBack();
            this.Hide();
        }

        private void PreviewTemplate_Clicked(object? sender, EventArgs e)
        {
            //Button btnClicked = (Button)sender;
            FloorplanTemplate template = (FloorplanTemplate)sender;
            if (floorplanManager.Shift.SelectedFloorplan != null)
            {
                floorplanManager.CopyTemplateSections(template);
                form1Reference.UpdateWithTemplate();
            }
            else
            {
                floorplanManager.SetFloorplanToTemplate(template);
                form1Reference.UpdateWithTemplate();
            }

        }

        private void frmTemplateSelection_Load(object sender, EventArgs e)
        {

        }
        private void frmTemplateSelection_Shown(object sender, EventArgs e)
        {
            if (floorplanManager.Floorplan == null)
            {
                serverCount = 5;
                lblServerCount.Text = serverCount.ToString();
            }
            else
            {

                serverCount = floorplanManager.Floorplan.Servers.Count;
                lblServerCount.Text = serverCount.ToString();
            }
            floorplanManager.UpdateTemplatesBasedOnFloorplan();
            floorplanManager.TemplateManager.serverCount = serverCount;
            floorplanManager.TemplateManager.FilterTemplates(serverCount);
            SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
        }
        // TODO: Remove redundancy with setting up template list multiple times
        private void SetTemplatePanels(List<FloorplanTemplate> templates)
        {
            floorplanManager.TemplateManager.CreateTemplatePanels(templates);
            List<Panel> panelList = floorplanManager.TemplateManager.PanelsForThisPage(displayedPage);

            int i = 0;
            foreach (var pan in panels)
            {
                if (i >= panelList.Count)
                {
                    ClearTemplateSections(pan);
                }
                else
                {
                    //SetupPanelWithTemplate(pan, templates[i]);
                    pan.Controls.Clear();
                    pan.Controls.Add(panelList[i]);
                }

                i++;
            }
            SetEnabledStatusOfPageButtons();

        }



        private void ClearTemplateSections(Panel pnl)
        {
            pnl.Tag = null;
            pnl.Controls.Clear();
        }
        private void addTablesToPanel(Panel panel, FloorplanTemplate template)
        {

            floorplanManager.TemplateManager.DisplayMiniTableControls(template, panel);
            floorplanManager.TemplateManager.CreateSectionPicBox(template, panel);

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.SendToBack();
            this.Hide();
        }
        private void btnIncreaseServers_Click(object sender, EventArgs e)
        {
            if (floorplanManager.Floorplan == null)
            {
                serverCount++;
                floorplanManager.TemplateManager.serverCount = serverCount;
                lblServerCount.Text = serverCount.ToString();
                floorplanManager.TemplateManager.FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
            }
            else
            {
                MessageBox.Show("You cannot use a template with a different number"
                    + "\n" + "of servers than the current floorplan has");
            }

        }

        private void btnDecreaseServers_Click(object sender, EventArgs e)
        {
            if (floorplanManager.Floorplan == null)
            {
                serverCount--;
                lblServerCount.Text = serverCount.ToString();
                floorplanManager.TemplateManager.serverCount = serverCount;
                floorplanManager.TemplateManager.FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
            }
            else
            {
                MessageBox.Show("You cannot use a template with a different number"
                    + "\n" + "of servers than the current floorplan has");
            }
        }

        private void rdoYesTeam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoYesTeam.Checked)
            {
                yesTeamWaitFilter = true;
                //floorplanManager.TemplateManager.FilterTemplates(serverCount, hasTeamWait: true);
                floorplanManager.TemplateManager.HasTeamFilter = true;
                floorplanManager.TemplateManager.FilterTeamYes = true;
                floorplanManager.TemplateManager.SetFilter();//FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
                //SetTemplatePanels(floorplanManager.TemplateManager.FilteredList);
            }
        }

        private void rdoNoTeam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNoTeam.Checked)
            {
                //yesTeamWaitFilter = true;
                floorplanManager.TemplateManager.HasTeamFilter = true;
                floorplanManager.TemplateManager.FilterTeamYes = false;
                floorplanManager.TemplateManager.SetFilter();//FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
            }
        }

        private void rdoBothTeam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBothTeam.Checked)
            {
                noTeamWaitFilter = true;
                floorplanManager.TemplateManager.HasTeamFilter = false;
                floorplanManager.TemplateManager.FilterTeamYes = false;
                floorplanManager.TemplateManager.SetFilter();//FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
                //SetTemplatePanels(floorplanManager.TemplateManager.FilteredList);
            }
        }

        private void rdoYesPickUp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoYesPickUp.Checked)
            {
                floorplanManager.TemplateManager.HasPickFilter = true;
                floorplanManager.TemplateManager.FilterPickYes = true;
                floorplanManager.TemplateManager.SetFilter();
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
            }
        }

        private void rdoNoPickUp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNoPickUp.Checked)
            {
                floorplanManager.TemplateManager.HasPickFilter = true;
                floorplanManager.TemplateManager.FilterPickYes = false;
                floorplanManager.TemplateManager.SetFilter();
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
            }
        }

        private void rdoBothPickUp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBothPickUp.Checked)
            {
                floorplanManager.TemplateManager.HasPickFilter = false;
                floorplanManager.TemplateManager.FilterPickYes = true;
                floorplanManager.TemplateManager.SetFilter();
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());
            }
        }
        public void GetFilteredList()
        {

        }

        private void btnNextTemplates_Click(object sender, EventArgs e)
        {


            if (displayedPage < floorplanManager.TemplateManager.PagesOfPanelsToDisplay(floorplanManager.TemplateManager.GetFilteredList()))
            {
                displayedPage++;
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());

            }
        }

        private void btnPreviousTemplates_Click(object sender, EventArgs e)
        {
            if (displayedPage > 1)
            {
                displayedPage--;
                SetTemplatePanels(floorplanManager.TemplateManager.GetFilteredList());

            }
        }
        private void SetEnabledStatusOfPageButtons()
        {
            if (displayedPage == 1)
            {
                btnPreviousTemplates.Enabled = false;
            }
            if (displayedPage < floorplanManager.TemplateManager.PagesOfPanelsToDisplay(floorplanManager.TemplateManager.GetFilteredList()))
            {
                btnNextTemplates.Enabled = true;
            }
            if (displayedPage == floorplanManager.TemplateManager.PagesOfPanelsToDisplay(floorplanManager.TemplateManager.GetFilteredList()))
            {
                btnNextTemplates.Enabled = false;
            }
            if (displayedPage > 1)
            {
                btnPreviousTemplates.Enabled = true;
            }
        }

        //public void AddSectionPanels(FlowLayoutPanel panel)
        //{
        //    panel.Controls.Clear();
        //    coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (panel.Width / 2) - 7, 30);
        //    salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (panel.Width / 2) - 7, 30);
        //    panel.Controls.Add(coversImageLabel);
        //    panel.Controls.Add(salesImageLabel);

        //    foreach (SectionPanelControl sectionPanel in _sectionPanels)
        //    {
        //        sectionPanel.Width = panel.Width - 10;
        //        sectionPanel.Margin = new Padding(5);
        //        panel.Controls.Add(sectionPanel);
        //    }
        //    Button btnAddPickup = new Button
        //    {
        //        Text = "Add Pick-Up Section",
        //        AutoSize = false,
        //        Size = new Size(panel.Width - 10, 25),
        //        Font = new Font("Segoe UI", 10F),
        //        FlatStyle = FlatStyle.Flat,
        //        BackColor = UITheme.ButtonColor,
        //        ForeColor = Color.Black
        //    };
        //    btnAddPickup.Click += btnAddPickupSection_Click;
        //    panel.Controls.Add(btnAddPickup);

        //}
    }
}
