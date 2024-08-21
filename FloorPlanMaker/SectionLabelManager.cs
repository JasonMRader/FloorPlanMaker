using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FloorPlanMakerUI
{
    public class SectionLabelManager : IFloorplanObserver
    {
        private Floorplan? _floorplan { get; set; }
        public Floorplan? Floorplan { get { return _floorplan; } }
        private Shift _shift { get; set; }

        private Panel _pnlFLoorplan;
        private List<SectionLabel> _sectionLabels {  get; set; } = new List<SectionLabel>();
        public List<SectionLabel> SectionLabels { get { return _sectionLabels; } }
        public SectionLabelManager(Floorplan floorplan, Shift shift, Panel panel) 
        {
            this._shift = shift;
            this._floorplan = floorplan;
            this._pnlFLoorplan = panel;
            //floorplan.SubscribeObserver(this);
            //floorplan.SectionRemoved += RemoveSection;
            //floorplan.SectionAdded += AddSection;
            if (floorplan != null)
            {
                CreateSectionLabels();
                AddSectionLabels();
            }
            if (floorplan == null)
            {
                ClearAllLabels();
            }
        }
        public void SetNewFloorplan(Floorplan floorplan)
        {
            _floorplan = floorplan;
            ClearAllLabels();
            CreateSectionLabels();
            AddSectionLabels();
        }
        private SectionLabel sectionLabelBySection(Section section)
        {
            SectionLabel sectionLabel = this._sectionLabels.FirstOrDefault(s => s.Section == section);
            return sectionLabel;
        }
        public void RemoveSectionLabelBySection(Section section)
        {
            SectionLabel sectionLabel = sectionLabelBySection(section);
            _pnlFLoorplan.Controls.Remove(sectionLabel);
            if(sectionLabel != null)
            {
                sectionLabel.Dispose();
            }
           
            this._sectionLabels.Remove(sectionLabelBySection(section));
        }
        public void ClearAllLabels()
        {
            foreach(SectionLabel sectionLabel in _sectionLabels)
            {
                _pnlFLoorplan.Controls.Remove(sectionLabel);
                sectionLabel.Dispose();

            }
            _sectionLabels.Clear();
        }

        private void AddSectionLabels()
        {
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control c in _pnlFLoorplan.Controls)
            {
                if (c is SectionLabel sectionLabel)
                {
                    controlsToRemove.Add(c);
                }
            }
            foreach (Control c in controlsToRemove)
            {
                _pnlFLoorplan.Controls.Remove(c);
            }
            foreach (SectionLabel sectionLabel in _sectionLabels)
            {
                sectionLabel.Location =  new Point(sectionLabel.Section.MidPoint.X - (sectionLabel.Width / 2),
                sectionLabel.Section.MidPoint.Y - (sectionLabel.Height / 2));
                _pnlFLoorplan.Controls.Add(sectionLabel);
                sectionLabel.UpdateControlsForSection();

                sectionLabel.BringToFront();
            }
        }

        private void CreateSectionLabels()
        {
            if(this._floorplan == null) { return; }
            foreach (Section section in _floorplan.Sections)
            {
                if (section.Tables.Count > 0)
                {
                    SectionLabel sectionLabel = new SectionLabel(section, _floorplan);
                    sectionLabel.SectionSelected += SelectSection;
                    sectionLabel.AssignPickUp += AssignPickUp_Click;
                    
                    //sectionLabel.ShowServerList += OpenServerSelection;
                    //sectionLabel.SectionLabelClick += SectionLabel_Clicked;

                    this._sectionLabels.Add(sectionLabel);
                }
               
            }
        }

        private void AssignPickUp_Click(Section section)
        {
           
            frmPickupSectionAssignment pickUpForm = new frmPickupSectionAssignment(section, _shift);
            pickUpForm.StartPosition = FormStartPosition.CenterScreen;
           
            pickUpForm.ShowDialog();
        }

        //private void OpenServerSelection(Section section, Floorplan floorplan)
        //{
        //    SectionLabel sectionLabel = (SectionLabel)sender;
        //    sectionLabel.ShowServerSelectionPanel();
        //}

        private void SelectSection(Section section)
        {
            _floorplan.SetSelectedSection(section);
        }

        public void UpdateFloorplan(Floorplan floorplan)
        {
            
        }

        internal void UpdateCloserStatus()
        {
            throw new NotImplementedException();
        }
    }
}
