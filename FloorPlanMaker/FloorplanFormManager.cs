﻿using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
   public class FloorplanFormManager
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
        public void SetTableControls()
        {
            _tableControls.Clear();
            if (this.ShiftManager != null && this.ShiftManager.SelectedDiningArea != null)
            {
                foreach (Table table in this.ShiftManager.SelectedDiningArea.Tables)
                {
                    table.DiningArea = this.ShiftManager.SelectedDiningArea;
                    TableControl tableControl = TableControlFactory.CreateTableControl(table);
                    _tableControls.Add(tableControl);
                }
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
                sectionPanel.picEraseSectionClicked += EraseSectionClicked;
                sectionPanel.picTeamWaitClicked += TeamWaitClicked;
                this._sectionPanels.Add(sectionPanel);
            }
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
                    
                    //UpdateTableControlSections();
                    //REMOVE SECTION LABEL AND CLEAR TABLECONTROL SECTIONS, REMOVE TABLES FROM SECTION
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
            panel.Invalidate();
        }
        public void AddSectionLabels(Panel panel)
        {
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
        private void FillInTableControlColors(Panel panel)
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
                                tableControl.Section = section;
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

        public void SetServerControls()
        {
            _serverControls.Clear();
            if(ShiftManager.SelectedFloorplan == null) { return; }
            if(ShiftManager.SelectedFloorplan.Servers.Count <= 0) { return; }               
            foreach(Server server in ShiftManager.SelectedFloorplan.Servers)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                ServerControl serverControl = new ServerControl(server, 300, 20);
                //serverControl.Click += ServerControl_Click;
                foreach (ShiftControl shiftControl in serverControl.ShiftControls)
                {

                    shiftControl.ShowClose();
                    shiftControl.ShowTeam();
                    shiftControl.HideOutside();
                }

                this._serverControls.Add(serverControl);
            }
        }
        
       
    }
}
