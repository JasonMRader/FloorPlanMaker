

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
        private Form1 form1Reference;
        private List<SectionPanelControl> _sectionPanels;
        public frmTemplateSelection(ShiftManager shiftManager, Form1 form1Reference)
        {
            InitializeComponent();
            this.ShiftManager = shiftManager;
            this.form1Reference = form1Reference;
        }

        private void frmTemplateSelection_Load(object sender, EventArgs e)
        {
            ShiftManager.Templates.Clear();
            if (ShiftManager.SelectedFloorplan != null)
            {
                ShiftManager.Templates = SqliteDataAccess.LoadTemplatesByDiningAreaAndServerCount(ShiftManager.SelectedDiningArea, ShiftManager.SelectedFloorplan.Servers.Count);
            }
            else
            {
                ShiftManager.Templates = SqliteDataAccess.LoadTemplatesByDiningArea(ShiftManager.SelectedDiningArea);
            }


            Panel[] panels = { panel1, panel2, panel3, panel4 };  // Assuming you have named your panels like this

            for (int i = 0; i < 4 && i < ShiftManager.Templates.Count; i++)
            {
                SetupPanelWithTemplate(panels[i], ShiftManager.Templates[i]);
            }
        }
        private void SetupPanelWithTemplate(Panel pnl, FloorplanTemplate template)
        {
            // Clear the current controls

            //pnl.Controls.Clear();
            pnl.Tag = template;
            foreach (Table table in ShiftManager.SelectedDiningArea.Tables)  // Assuming FloorplanTemplate has a Tables property
            {
                table.DiningArea = ShiftManager.SelectedDiningArea;
                TableControl tableControl = TableControlFactory.CreateMiniTableControl(table, (float).4, 27);
                //tableControl.TableClicked += Table_TableClicked;  // Uncomment if you want to attach event handler

                pnl.Controls.Add(tableControl);
            }
            ShiftManager.SetSectionsToTemplate(template);
            ShiftManager.AssignSectionNumbers(ShiftManager.TemplateSections);
            foreach (Control ctrl in pnl.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Click += btnSelectTemplate_Click;
                    btn.Tag = template;
                }
                if (ctrl is TableControl tableControl)
                {
                    foreach (Section section in ShiftManager.TemplateSections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.UpdateSection(section);
                                tableControl.BackColor = section.Color;
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
            //this.Close();

            //this.Dispose();
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
