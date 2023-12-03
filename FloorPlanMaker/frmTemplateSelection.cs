

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
        ShiftManager ShiftManager;
        private FloorplanFormManager floorplanManager { get; set; }
        private Form1 form1Reference;
        private int serverCount;
        private bool yesTeamWaitFilter;
        private bool noTeamWaitFilter;
        private bool yesPickUpFilter;
        private bool noPickUpFilter;
        private List<SectionPanelControl> _sectionPanels;
        private DiningArea area;
        public frmTemplateSelection(FloorplanFormManager floorplanManager, DiningArea diningArea, Form1 form1Reference)
        {
            InitializeComponent();
            this.floorplanManager = floorplanManager;
            this.form1Reference = form1Reference;
            this.area = diningArea;
            floorplanManager.UpdateTemplatesBasedOnFloorplan();
            addTablesToPanels();
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
            SetTemplatePanels(floorplanManager.TemplateManager.Templates);
        }
        private void SetTemplatePanels(List<FloorplanTemplate> templates)
        {
            Panel[] panels = { panel1, panel2, panel3, panel4 };  // Assuming you have named your panels like this
            int i = 0;
            foreach (var pan in panels)
            {
                if(i >= templates.Count)
                {
                    ClearTemplateSections(pan);
                }
                else
                {
                    SetupPanelWithTemplate(pan, templates[i]);
                }
                
                i++;
            }
        }

        private void ClearTemplateSections(Panel pnl)
        {
            pnl.Tag = null;

            //ShiftManager.SetSectionsToTemplate(template);
            //ShiftManager.AssignSectionNumbers(ShiftManager.TemplateSections);
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Click -= btnSelectTemplate_Click;
                    btn.Tag = null;
                    btn.Visible = false;
                }
                if (ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = pnl.BackColor;
                    tableControl.RemoveSection();
                    tableControl.Visible = false;
                    
                }
            }
        }

        private void addTablesToPanels()
        {
            Panel[] panels = { panel1, panel2, panel3, panel4 };  // Assuming you have named your panels like this

            foreach (var pan in panels)
            {
                foreach (Table table in area.Tables)  // Assuming FloorplanTemplate has a Tables property
                {
                    table.DiningArea = area;
                    TableControl tableControl = TableControlFactory.CreateMiniTableControl(table, (float).4, 27);
                    //tableControl.TableClicked += Table_TableClicked;  // Uncomment if you want to attach event handler

                    pan.Controls.Add(tableControl);
                }
            }

        }
        private void SetupPanelWithTemplate(Panel pnl, FloorplanTemplate template)
        {
            
            pnl.Tag = template;

            //ShiftManager.SetSectionsToTemplate(template);
            //ShiftManager.AssignSectionNumbers(ShiftManager.TemplateSections);
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Click += btnSelectTemplate_Click;
                    btn.Tag = template;
                    btn.Visible = true;
                }
                if (ctrl is TableControl tableControl)
                {
                    
                    foreach (Section section in template.Sections)  //ShiftManager.TemplateSections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.UpdateSection(section);
                                tableControl.BackColor = section.Color;
                                tableControl.Visible = true;
                                tableControl.Invalidate();
                                break; // Once found, no need to check other tables in this section
                            }
                        }
                    }
                }
            }

            // Any other setup logic specific to the template can be added here
        }
        private void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            FloorplanTemplate template = new FloorplanTemplate();


            template = (FloorplanTemplate)button.Tag;
            if (ShiftManager.SelectedFloorplan == null)
            {
                //ShiftManager.SelectedFloorplan = new Floorplan(template);
                //this.Parent.SendToBack();
                //this.Hide();
                _sectionPanels.Clear();
                if (ShiftManager.SelectedFloorplan == null) { return; }
                foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
                {
                    SectionPanelControl sectionPanel = new SectionPanelControl(section, this.ShiftManager.SelectedFloorplan);

                    if (section.IsTeamWait)
                    {
                        sectionPanel.SetToTeamWait();
                    }
                    // sectionPanel += SectionAdded?
                    //sectionPanel.UpdateRequired += FloorplanManager_UpdateRequired;
                    this._sectionPanels.Add(sectionPanel);
                }
            }
            else
            {
                ShiftManager.SelectedFloorplan.CopyTemplateSections(template.Sections);
            }
            //ShiftManager.SelectedFloorplan.CopySectionsIntoSections(template.Sections);
            //ShiftManager.ViewedFloorplan = ShiftManager.SelectedFloorplan;
            this.DialogResult = DialogResult.OK;

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
                lblServerCount.Text = serverCount.ToString();
                floorplanManager.TemplateManager.FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.FilteredList);
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
                floorplanManager.TemplateManager.FilterTemplates(serverCount);
                SetTemplatePanels(floorplanManager.TemplateManager.FilteredList);
            }
            else
            {
                MessageBox.Show("You cannot use a template with a different number"
                    + "\n" + "of servers than the current floorplan has");
            }
        }

        private void rdoYesTeam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoYesPickUp.Checked)
            {
                yesTeamWaitFilter = true;
            }
        }

        private void rdoNoTeam_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoNoPickUp.Checked)
            {
                yesTeamWaitFilter = true;
            }
        }

        private void rdoBothTeam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBothTeam.Checked) {
                noTeamWaitFilter = true;
            }
        }

        private void rdoYesPickUp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoNoPickUp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoBothPickUp_CheckedChanged(object sender, EventArgs e)
        {

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
