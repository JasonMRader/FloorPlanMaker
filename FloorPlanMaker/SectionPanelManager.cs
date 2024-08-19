using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FloorPlanMakerUI
{
    public class SectionPanelManager : IFloorplanObserver
    {
        private List<SectionInfoPanel> _sectionPanels = new List<SectionInfoPanel>();
        public List<SectionInfoPanel> SectionPanels { get {  return _sectionPanels; } }
        private Floorplan? _floorplan { get; set; }
        private ToolTip toolTip = new ToolTip();
        public Floorplan? Floorplan { get {  return _floorplan; } }
        private DiningArea _diningArea = new DiningArea();
        private FlowLayoutPanel _flowLayoutPanel = new FlowLayoutPanel();
        private ImageLabelControl serverCountImageLabel = new ImageLabelControl();
        private ImageLabelControl coversImageLabel = new ImageLabelControl();
        private ImageLabelControl salesImageLabel = new ImageLabelControl();
        public SectionPanelManager(Floorplan floorplan, FlowLayoutPanel flowLayoutPanel)
        {
            _floorplan = floorplan;
            floorplan.SubscribeObserver(this);
            floorplan.SectionRemoved += RemoveSection;
            floorplan.SectionAdded += AddSection;
            _flowLayoutPanel = flowLayoutPanel;
            _flowLayoutPanel.Controls.Clear();
            if(floorplan != null)
            {
                AddSectionPanels();
            }
        }
        public SectionPanelManager(DiningArea area, FlowLayoutPanel flowLayoutPanel)
        {
            _floorplan = null;
            
            _flowLayoutPanel = flowLayoutPanel;
            _flowLayoutPanel.Controls.Clear();
            
            
        }
        private void SetSectionImageLabels()
        {
            serverCountImageLabel = new ImageLabelControl(UITheme.waiter, "0", (_flowLayoutPanel.Width / 4) - 7, 30);
            serverCountImageLabel.Margin = new Padding(6, 3, 3, 3);
            coversImageLabel = new ImageLabelControl(UITheme.covers, "0", (_flowLayoutPanel.Width / 4) - 7, 30);
            salesImageLabel = new ImageLabelControl(UITheme.sales, "$0", (_flowLayoutPanel.Width / 2) - 7, 30);
            serverCountImageLabel.SetTooltip("Servers Assigned to this Floorplan");
            coversImageLabel.SetTooltip("Covers per Server");
            salesImageLabel.SetTooltip("Sales Per Server");
            _flowLayoutPanel.Controls.Add(serverCountImageLabel);
            _flowLayoutPanel.Controls.Add(coversImageLabel);
            _flowLayoutPanel.Controls.Add(salesImageLabel);
            UpdateImageLabels();
        }
        private void UpdateImageLabels()
        {
            if (this.Floorplan == null)
            {
                serverCountImageLabel.UpdateText("0");
                coversImageLabel.UpdateText(_diningArea.GetMaxCovers().ToString("F0"));
                salesImageLabel.UpdateText(_diningArea.ExpectedSales.ToString("C0"));
            }
            else
            {
                serverCountImageLabel.UpdateText(Floorplan.Servers.Count.ToString());
                coversImageLabel.UpdateText(_floorplan.MaxCoversPerServer.ToString("F0"));
                salesImageLabel.UpdateText(_floorplan.AvgSalesPerServer.ToString("C0"));
            }

            serverCountImageLabel.Invalidate();
            coversImageLabel.Invalidate();
            salesImageLabel.Invalidate();
        }
        private void AddSection(Section sectionAdded, Floorplan floorplan)
        {
            SectionInfoPanel infoPanel = new SectionInfoPanel(sectionAdded, _floorplan, _flowLayoutPanel.Width);
            infoPanel.SectionSelected += SelectSection;
            _flowLayoutPanel.Controls.Add(infoPanel);
        }

        private void RemoveSection(Section sectionRemoved, Floorplan floorplan)
        {
            SectionInfoPanel panelToRemove = null;
           
            foreach (Control control in _flowLayoutPanel.Controls)
            {                
                if (control is SectionInfoPanel sectionInfoPanel)
                {                   
                    if (sectionInfoPanel.Section == sectionRemoved)
                    {
                        panelToRemove = sectionInfoPanel;
                        break; 
                    }
                }
            }
           
            if (panelToRemove != null)
            {
                _flowLayoutPanel.Controls.Remove(panelToRemove);
                panelToRemove.Dispose(); 
            }
        }


        public void ChangeFloorplan(Floorplan floorplan)
        {

        }

        public void UpdateFloorplan(Floorplan floorplan)
        {
            throw new NotImplementedException();
        }
        public void AddSectionPanels()
        {
            SetSectionImageLabels();
            
            Button btnAddPickup = new Button
            {
                Text = "Add Pick-Up Section",
                AutoSize = false,
                Size = new Size(_flowLayoutPanel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F)               
            };
            Button btnAddSection = new Button
            {
                Text = "Add A Section",
                AutoSize = false,
                Size = new Size(_flowLayoutPanel.Width - 10, 25),
                Font = new Font("Segoe UI", 10F)
               
            };
            UITheme.FormatCTAButton(btnAddSection);
            UITheme.FormatCTAButton(btnAddPickup);
            btnAddPickup.Margin = new Padding(5,5,5,15);
            btnAddSection.Margin = new Padding(5, 5, 5, 5);
            _flowLayoutPanel.Controls.Add(btnAddSection);
            _flowLayoutPanel.Controls.Add(btnAddPickup);
            foreach (Section section in _floorplan.Sections)
            {
                SectionInfoPanel infoPanel = new SectionInfoPanel(section, _floorplan, _flowLayoutPanel.Width);

                infoPanel.SectionSelected += SelectSection;
                _flowLayoutPanel.Controls.Add(infoPanel);
            }
            toolTip.SetToolTip(btnAddPickup, "Add a Pickup Section [P]");
            toolTip.SetToolTip(btnAddSection, "Add a Section");
            btnAddSection.Click += btnAddSection_Click;
            btnAddPickup.Click += btnAddPickupSection_Click;
           

        }

        private void btnAddPickupSection_Click(object? sender, EventArgs e)
        {
            Section pickUp = new Section(Floorplan);
            pickUp.Name = "Pickup";
            pickUp.IsPickUp = true;
            Floorplan.AddSection(pickUp);    
            Floorplan.SetSelectedSection(pickUp);
        }

        private void btnAddSection_Click(object? sender, EventArgs e)
        {
            Section section = new Section();

            section.IsPickUp = false;
            Floorplan.AddSection(section);

            //AddNewPanelControl(section);
            Floorplan.SetSelectedSection(section);
        }

        private void SelectSection(Section section)
        {
            _floorplan.SetSelectedSection(section);
        }
    }
}
