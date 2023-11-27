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
   public class FloorplanFormManager : ISectionObserver
    {
        public Floorplan? Floorplan
        {
            get
            {
                return this.ShiftManager.SelectedFloorplan;
            }
        }
        public ShiftManager ShiftManager;
        private List<TableControl> _tableControls = new List<TableControl>();    
        private List<SectionLabelControl> _sectionLabels = new List<SectionLabelControl>();
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        private List<ServerControl> _serverControls = new List<ServerControl>();
        public event EventHandler SectionLabelRemoved;
        public event EventHandler<UpdateEventArgs> UpdateRequired;


        public FloorplanFormManager(ShiftManager shiftManager)
        {
            //this.Floorplan = shiftManager.SelectedFloorplan;
            this.ShiftManager = shiftManager;
            
        }
        public List<TableControl> TableControls
        {
            get {  return _tableControls; }
            set { _tableControls = value; }                
            
        }
        public List<SectionLabelControl> SectionLabels
        {
            get { return _sectionLabels; }
            set { _sectionLabels = value; }
        }
        public List<SectionPanelControl> SectionPanels
        {
            get { return _sectionPanels; }
            set { _sectionPanels = value; }
        }
        public List<ServerControl> ServerControls
        {
            get { return _serverControls; }
            set { _serverControls = value; }
        }
        public void AddTableControls(Panel panel)
        {
            _tableControls.Clear();
            panel.Controls.Clear();
            if (this.ShiftManager != null && this.ShiftManager.SelectedDiningArea != null)
            {
                foreach (Table table in this.ShiftManager.SelectedDiningArea.Tables)
                {
                    table.DiningArea = this.ShiftManager.SelectedDiningArea;
                    TableControl tableControl = TableControlFactory.CreateTableControl(table);
                    tableControl.TableClicked += TableControl_TableClicked;
                    _tableControls.Add(tableControl);
                    panel.Controls.Add(tableControl);
                }
            }
        }
        public void CreateSectionControls()
        {
            _sectionLabels.Clear();
            _sectionLabels.Clear();
            if (ShiftManager.SelectedFloorplan == null) { return; }
            foreach(Section section in  ShiftManager.SelectedFloorplan.Sections) 
            {

            }
        }
        public void SetSectionLabels()
        {
            _sectionLabels.Clear();
            if(ShiftManager.SelectedFloorplan == null) { return; }
            foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
            {
                
                if (section.Tables.Count > 0)
                {
                    SectionLabelControl sectionControl = new SectionLabelControl(section, ShiftManager.SelectedFloorplan.ServersWithoutSection);
                    
                    this._sectionLabels.Add(sectionControl);
                    
                }
                if (section.Server != null)
                {
                    ShiftManager.SelectedFloorplan.ServersWithoutSection.Remove(section.Server);
                    
                    
                }
            }
        }
        public void SetSectionPanels()
        {
            _sectionPanels.Clear();
            if( ShiftManager.SelectedFloorplan == null) { return ; }
            foreach(Section section in ShiftManager.SelectedFloorplan.Sections)
            {
                SectionPanelControl sectionPanel = new SectionPanelControl(section, this.ShiftManager.SelectedFloorplan);
                sectionPanel.CheckBoxChanged += setSelectedSection;
                sectionPanel.picEraseSectionClicked += EraseSectionClicked;
                sectionPanel.picTeamWaitClicked += TeamWaitClicked;
                this._sectionPanels.Add(sectionPanel);
            }
            Floorplan.SetSelectedSection(Floorplan.Sections[0]);
        }
       
        public void SetServerControls()
        {
            _serverControls.Clear();
            if (ShiftManager.SelectedFloorplan == null) { return; }
            if (ShiftManager.SelectedFloorplan.Servers.Count <= 0) { return; }
            foreach (Server server in ShiftManager.SelectedFloorplan.Servers)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                ServerControl serverControl = new ServerControl(server, 20);
                serverControl.Click += ServerControl_Click;
                foreach (ShiftControl shiftControl in serverControl.ShiftControls)
                {

                    shiftControl.ShowClose();
                    shiftControl.ShowTeam();
                    shiftControl.HideOutside();
                }

                this._serverControls.Add(serverControl);
            }
            UpdateServerControls();
        }
        private void ServerControl_Click(object? sender, EventArgs e)
        {
            ServerControl serverControl = (ServerControl)sender;
            Server server = serverControl.Server;
            if (ShiftManager.SelectedFloorplan.SectionSelected == null) { return; }   
            
            ShiftManager.SelectedFloorplan.SectionSelected.AddServer(server);
            UpdateServerControls();

            
           
            //foreach (SectionLabelControl sc in sectionControlsManager.SectionControls)
            //{
            //    if (sc.Section == shiftManager.SectionSelected)
            //    {
            //        sc.UpdateLabel();
            //    }
            //}
           // serverControl.Label.BackColor = ShiftManager.SectionSelected.Color;
            //serverControl.Label.ForeColor = ShiftManager.SectionSelected.FontColor;

        }
        private void setSelectedSection(object? sender, EventArgs e)
        {
            
            SectionPanelControl sectionPanelSender = (SectionPanelControl)sender;
            if (sectionPanelSender.isChecked())
            {
                this.ShiftManager.SetSelectedSection(sectionPanelSender.Section);
                foreach (SectionPanelControl sectionPanelControl in this._sectionPanels)
                {
                    if (sectionPanelControl != sectionPanelSender)
                    {
                        sectionPanelControl.UnCheckBox();
                    }
                }
            }
            
        }
        public void IncrementSelectedSection()
        {
            if(this.Floorplan == null) { return; }
            this.Floorplan.MoveToNextSection();
        }
        private void TeamWaitClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EraseSectionClicked(object? sender, EventArgs e)
        {
            SectionPanelControl sectionPanel = (SectionPanelControl)sender;
            Section selectedSection = sectionPanel.Section;
            if (selectedSection != null)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this section?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    //SectionLabelRemoved?.Invoke(this, e);
                    UpdateRequired?.Invoke(this, new UpdateEventArgs(UpdateType.SectionLabel, selectedSection));
                    this._sectionLabels.Remove(sectionLabelBySection(selectedSection));
                    

                    //this.UpdateRequired += FloorplanManager_UpdateRequired;
                    ShiftManager.SelectedFloorplan.UnassignSection(selectedSection);

                    UpdateTableControlColors();
                    
                    //AND CLEAR TABLECONTROL SECTIONS,
                    //REMOVE TABLES FROM SECTION
                    //Update ServerControl 
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
        }
        
        private Section sectionByNumber(int sectionNumber)
        {
            Section sectionbyNumber = ShiftManager.SelectedFloorplan.Sections.FirstOrDefault(s => s.Number == sectionNumber);
            return sectionbyNumber;
        }
        private SectionLabelControl sectionLabelBySection(Section section)
        {
            SectionLabelControl sectionLabel = this._sectionLabels.FirstOrDefault(s => s.Section == section);
            return sectionLabel;
        }
        public void RemoveSectionLabel(Section section, Panel panel)
        {
            panel.Controls.Remove(sectionLabelBySection((Section)section));
            //UpdateTableControlColors(panel);
            panel.Invalidate();

        }
        public void AddSectionLabels(Panel panel)
        {
            List<Control> controlsToRemove  = new List<Control>();
            foreach(Control c in panel.Controls)
            {
                if (c is SectionLabelControl sectionLabel)
                {
                    controlsToRemove.Add(c);
                }
            }
            foreach(Control c in controlsToRemove)
            {
                panel.Controls.Remove(c);
            }
            foreach(SectionLabelControl sectionLabelControl in _sectionLabels)
            {
                panel.Controls.Add(sectionLabelControl);
                sectionLabelControl.BringToFront();
            }
        }
        public void AddSectionPanels(FlowLayoutPanel panel)
        {
            foreach(SectionPanelControl sectionPanel in _sectionPanels)
            {
                panel.Controls.Add(sectionPanel);
            }
        }
        public void AddServerControls(FlowLayoutPanel panel)
        {
            foreach (ServerControl serverControl in _serverControls)
            {
                panel.Controls.Add(serverControl);
            }
        }
        public void UpdateTableControlColors(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {

                if (ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = panel.BackColor;
                    tableControl.TextColor = panel.ForeColor;
                    foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
                    {

                        foreach (Table table in section.Tables)
                        {
                            if (tableControl.Table.TableNumber == table.TableNumber)
                            {
                                tableControl.SetSection(section);
                                //tableControl.BackColor = section.MuteColor(0.35f);
                                tableControl.MuteColors();
                                if (section == this.ShiftManager.SectionSelected)
                                {
                                    tableControl.BackColor = section.MuteColor(1.35f);
                                }

                                //tableControl.ForeColor = section.FontColor;
                                tableControl.Invalidate();
                                break;
                            }
                        }
                    }
                }
            }

        }
        public void UpdateTableControlColors()
        {

            foreach (TableControl tableControl in this._tableControls)
            {
                tableControl.BackColor = tableControl.Parent.BackColor;
                tableControl.TextColor = tableControl.Parent.ForeColor;
                foreach (Section section in ShiftManager.SelectedFloorplan.Sections)
                {

                    foreach (Table table in section.Tables)
                    {
                        if (tableControl.Table.TableNumber == table.TableNumber)
                        {
                            tableControl.SetSection(section);
                            //tableControl.BackColor = section.MuteColor(0.35f);
                            tableControl.MuteColors();
                            if (section == this.ShiftManager.SectionSelected)
                            {
                                tableControl.BackColor = section.MuteColor(1.35f);
                            }

                            //tableControl.ForeColor = section.FontColor;
                            tableControl.Invalidate();
                            break;
                        }
                    }
                } 
            }
              

        }


        public void UpdateTableControlSections(Panel panel)
        {
            foreach(Control ctrl in panel.Controls)
            {
                if(ctrl is TableControl tableControl)
                {
                    tableControl.BackColor = Color.Black; tableControl.ForeColor = Color.Black;
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
                clickedTableControl.BackColor = clickedTableControl.Parent.BackColor;  // Restore the original color
                clickedTableControl.ForeColor = clickedTableControl.Parent.ForeColor;
                clickedTableControl.Invalidate();
                //UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);

                return;
            }
            
            if (ShiftManager.SectionSelected != null)
            {
                if (sectionEdited != null)
                {
                    //sectionEdited.Tables.RemoveAll(t => t.ID == clickedTable.ID);
                    sectionEdited.RemoveTable(clickedTable);
                    clickedTableControl.RemoveSection();
                    //UpdateSectionLabels(sectionEdited, sectionEdited.MaxCovers, sectionEdited.AverageCovers);
                }
                ShiftManager.SectionSelected.AddTable(clickedTable);
                clickedTableControl.SetSection(ShiftManager.SectionSelected);
                
                clickedTableControl.BackColor = ShiftManager.SectionSelected.Color;
                clickedTableControl.TextColor = ShiftManager.SectionSelected.FontColor;

                
                clickedTableControl.Invalidate();
                //UpdateSectionLabels(shiftManager.SectionSelected, shiftManager.SectionSelected.MaxCovers, shiftManager.SectionSelected.AverageCovers);
            }

            

        }
        public void UpdateServerControls()
        {
            if (ShiftManager.SelectedFloorplan == null) return;

            foreach (ServerControl serverControl in _serverControls)
            {
                // Get the server from the server control
                Server server = serverControl.Server;

                // Find the section to which the server is assigned
                Section assignedSection = ShiftManager.SelectedFloorplan.SectionServerMap
                                          .FirstOrDefault(kv => kv.Value == server).Key;

                // Update the label's background color based on the section's color
                if (assignedSection != null)
                {
                    serverControl.Label.BackColor = assignedSection.Color;
                }
                else
                {
                    serverControl.Label.BackColor = UITheme.ButtonColor; // Replace DefaultColor with your default color
                }
            }
        }

        public void UpdateSection(Section section)
        {
           //UpdateServerControls();
        }
    }
}
