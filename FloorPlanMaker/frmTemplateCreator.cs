using FloorplanClassLibrary;
using FloorPlanMaker;
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

namespace FloorPlanMakerUI
{
    public partial class frmTemplateCreator : Form
    {
        public frmTemplateCreator(DiningArea area, Form1 form1)
        {
            InitializeComponent();
            this.diningArea = area;
            this.floorplan = new Floorplan(this.diningArea, DateTime.Now, true, 1, 1);
            this.form1Reference = form1;
        }
        private Form1 form1Reference;
        private DiningArea diningArea = new DiningArea();
        private Floorplan floorplan = new Floorplan();
        private ImageLabelControl coversImageLabel = new ImageLabelControl();
        private ImageLabelControl salesImageLabel = new ImageLabelControl();
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        private List<TableControl> _tableControls = new List<TableControl>();

        private void frmTemplateCreator_Load(object sender, EventArgs e)
        {
            UITheme.FormatAccentColor(this);
            UITheme.FormatCanvasColor(pnlFloorplan);
            UITheme.FormatCanvasColor(flowSectionSelect);
            UITheme.FormatCTAButton(btnAddSection);
            UITheme.FormatCTAButton(btnRemoveSection);
            lblServerCount.ForeColor = Color.Black;
            lblDiningArea.ForeColor = Color.Black;
            LoadTables();
        }
        private void LoadTables()
        {
            foreach (Table table in diningArea.Tables)
            {
                table.DiningArea = diningArea;
                TableControl tableControl = TableControlFactory.CreateTableControl(table);
                tableControl.TableClicked += TableControl_TableClicked;
                _tableControls.Add(tableControl);

                pnlFloorplan.Controls.Add(tableControl);
            }
        }


        private void TableControl_TableClicked(object sender, TableClickedEventArgs e)
        {

            TableControl clickedTableControl = sender as TableControl;
            Table clickedTable = clickedTableControl.Table;
            Section sectionEdited = (Section)clickedTableControl.Section;
            if (e.MouseButton == MouseButtons.Right && clickedTableControl.Section != null)
            {



                sectionEdited.RemoveTable(clickedTable);

                clickedTableControl.RemoveSection();
                clickedTableControl.BackColor = clickedTableControl.Parent.BackColor;  // Restore the original color
                clickedTableControl.ForeColor = clickedTableControl.Parent.ForeColor;
                clickedTableControl.Invalidate();
                if (sectionEdited.IsPickUp)
                {
                    UpdateAveragesPerServer();
                }
                //UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);

                return;
            }

            if (floorplan.SectionSelected != null)
            {
                if (sectionEdited != null)
                {

                    sectionEdited.RemoveTable(clickedTable);
                    clickedTableControl.RemoveSection();
                    if (sectionEdited.IsPickUp)
                    {
                        UpdateAveragesPerServer();
                    }

                }
                floorplan.SectionSelected.AddTable(clickedTable);
                clickedTableControl.SetSection(floorplan.SectionSelected);

                clickedTableControl.BackColor = floorplan.SectionSelected.Color;
                clickedTableControl.TextColor = floorplan.SectionSelected.FontColor;
                if (floorplan.SectionSelected.IsPickUp)
                {
                    UpdateAveragesPerServer();
                }


                clickedTableControl.Invalidate();
                if (AllTablesAreAssigned())
                {
                    //UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.TableControl, UpdateType.Refresh, clickedTableControl));
                }
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
            }



        }
        public void UpdateAveragesPerServer()
        {
            float averagerPerServer = diningArea.ExpectedSales / floorplan.Servers.Count;
            coversImageLabel.UpdateText(floorplan.MaxCoversPerServer.ToString("F0"));
            salesImageLabel.UpdateText(averagerPerServer.ToString("C0"));
            coversImageLabel.Invalidate();
            salesImageLabel.Invalidate();
            foreach (SectionPanelControl sectionPanel in _sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
        }
        private bool AllTablesAreAssigned()
        {
            foreach (TableControl tableControl in _tableControls)
            {
                if (tableControl.Section == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            form1Reference.CloseTemplateCreator();
            this.Close();
        }
    }
}
