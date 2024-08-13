using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class TableControlManager
    {
        private List<TableControl> _tableControls = new List<TableControl>();
        public List<TableControl> TableControls { get {  return _tableControls; } }
        private Floorplan? floorplan { get; set; }
        private DiningArea _diningArea = new DiningArea();
        public DiningArea DiningArea { get {  return _diningArea; } }
        private Panel pnlFloorplan { get; set; }
        public event EventHandler UpdateAveragesPerServer;
        public event EventHandler AllTablesAssigned;
        public TableControlManager(Panel panel)
        {
            pnlFloorplan = panel;
        }
        public void SetDiningArea (DiningArea diningArea)
        {
            this._diningArea = diningArea;
        }
        public void AddTableControls(Panel panel)
        {
            _tableControls.Clear();
            foreach (Control control in panel.Controls)
            {
                control.Dispose();
            }
            panel.Controls.Clear();
            panel.Invalidate();
            if (_diningArea != null)
            {
                foreach (Table table in _diningArea.Tables)
                {
                    table.DiningArea = _diningArea;
                    TableControl tableControl = TableControlFactory.CreateTableControl(table);
                    tableControl.TableClicked += TableControl_TableClicked;
                    _tableControls.Add(tableControl);
                    panel.Controls.Add(tableControl);
                }
            }
        }
        public void UpdateTableControlColors()
        {
            foreach (TableControl tableControl in this._tableControls)
            {
                tableControl.BackColor = tableControl.Parent.BackColor;
                tableControl.TextColor = tableControl.Parent.ForeColor;
                tableControl.Invalidate();

            }
            if (floorplan == null) { return; }
            foreach (TableControl tableControl in this._tableControls)
            {

                foreach (Section section in floorplan.Sections)
                {
                    foreach (Table table in section.Tables)
                    {
                        if (tableControl.Table.TableNumber == table.TableNumber)
                        {
                            tableControl.SetSection(section);
                            tableControl.MuteColors();
                            tableControl.TextColor = section.FontColor;
                            if (section == floorplan.SectionSelected)
                            {
                                tableControl.BackColor = section.MuteColor(1.15f);

                            }
                            tableControl.Invalidate();
                            break;
                        }
                    }
                }
            }

        }
        public void ResetSections()
        {
           
            foreach (TableControl tableControl in this._tableControls)
            {
                tableControl.RemoveSection();
            }
            UpdateTableControlColors();
            

        }
        public void TableControlDisplayModeToSales()
        {
            foreach (TableControl tableControl in this._tableControls)
            {
                tableControl.CurrentDisplayMode = DisplayMode.AverageCovers;
            }
        }
        public void UpdateTableControlSections(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = Color.Black;
                    tableControl.ForeColor = Color.Black;
                }
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
                clickedTableControl.BackColor = clickedTableControl.Parent.BackColor;
                clickedTableControl.ForeColor = clickedTableControl.Parent.ForeColor;
                clickedTableControl.Invalidate();
                if (sectionEdited.IsPickUp)
                {
                    UpdateAveragesPerServer?.Invoke(sender, new EventArgs());
                }

                return;
            }

            if (floorplan != null)
            {
                if (sectionEdited != null)
                {
                    sectionEdited.RemoveTable(clickedTable);
                    clickedTableControl.RemoveSection();
                    if (sectionEdited.IsPickUp)
                    {
                        UpdateAveragesPerServer?.Invoke(sender, new EventArgs());
                    }
                }
                AddTableControlToSelectedSection(clickedTableControl);
            }

        }
        private void AddTableControlToSelectedSection(TableControl clickedTableControl)
        {
            Table clickedTable = clickedTableControl.Table;
            floorplan.SectionSelected.AddTable(clickedTable);
            clickedTableControl.SetSection(floorplan.SectionSelected);
            clickedTableControl.BackColor = floorplan.SectionSelected.Color;
            clickedTableControl.TextColor = floorplan.SectionSelected.FontColor;
            if (floorplan.SectionSelected.IsPickUp)
            {
                UpdateAveragesPerServer?.Invoke(clickedTableControl, new EventArgs());
            }
            clickedTableControl.Invalidate();
            if (AllTablesAreAssigned())
            {
                AllTablesAssigned?.Invoke(clickedTableControl, new EventArgs());
                //UpdateRequired?.Invoke(this, new UpdateEventArgs(ControlType.TableControl, UpdateType.Refresh, clickedTableControl));
            }
        }
        public bool AllTablesAreAssigned()
        {
            foreach (TableControl tableControl in TableControls)
            {
                if (tableControl.Section == null)
                {
                    return false;
                }
            }
            return true;
        }
        public int NumberOfUnassignedTables()
        {
            int unassignedTables = 0;
            foreach (TableControl tableControl in TableControls)
            {
                if (tableControl.Section == null)
                {
                    unassignedTables++;
                }
            }
            return unassignedTables;
        }

        public void SelectTables(List<TableControl> selectedTables)
        {
            foreach (var tableControl in selectedTables)
            {
                TableClickedEventArgs args = new TableClickedEventArgs(tableControl.Table, false);
                TableControl_TableClicked(tableControl, args);
            }
        }
    }
}
