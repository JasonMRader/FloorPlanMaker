using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FloorplanUserControlLibrary
{
    public partial class ShiftFilterControl : UserControl
    {
        private ShiftAnalysis _shiftAnalysis { get; set; }
        public ShiftAnalysis ShiftAnalysis { get { return _shiftAnalysis; } }
        public event Action UpdateShift;
        public event Action OpenSalesForm;
        public event Action<bool> ToggleDayOf;
        private DiningAreaManager _areaManager { get; set; } = new DiningAreaManager();
        public ShiftFilterControl()
        {
            InitializeComponent();
            foreach (DiningArea area in _areaManager.DiningAreas) {
                if (area.ID == 6) { continue; }
                flowDiningAreas.Controls.Add(CreateAreaRadio(area));
            }

        }
        public void ChangeShiftAnalysisDateIsAM(DateOnly dateOnly, bool isAM)
        {
            if (_shiftAnalysis != null) {
                _shiftAnalysis.SetStandardFiltersForDateAndShiftType(isAM, dateOnly);
                RefreshForNewShiftAnalysis();
            }

        }
        public void SetShiftAnalysis(ShiftAnalysis shiftAnalysis)
        {

            if (_shiftAnalysis != null) {
                _shiftAnalysis.FilterUpdated -= UpdateForFilters;
            }
            _shiftAnalysis = shiftAnalysis;
            if (shiftAnalysis.SpecialEventsAllowed) {
                cbHolidaysExcluded.Checked = false;
            }
            else { cbHolidaysExcluded.Checked = true; }
            shiftAnalysis.FilterUpdated += UpdateForFilters;
            RefreshForNewShiftAnalysis();


        }
        private void RefreshForNewShiftAnalysis()
        {
            flowFilters.Controls.Clear();

            FilterControl tempFilter = new FilterControl("Temp", 80, 90, FilterControl.FilterType.Temperature, _shiftAnalysis);
            FilterControl rainFilter = new FilterControl("Rain", 0, 0, FilterControl.FilterType.Rain, _shiftAnalysis);
            FilterControl cloudFilter = new FilterControl("Clouds", 0, 80, FilterControl.FilterType.Clouds, _shiftAnalysis);
            FilterControl windMaxControl = new FilterControl("Max Wind", 0, 15, FilterControl.FilterType.WindMax, _shiftAnalysis);
            FilterControl windAvgControl = new FilterControl("Avg Wind", 0, 10, FilterControl.FilterType.WindAvg, _shiftAnalysis);
            FilterControl resoControl = new FilterControl("Reservations", 100, 300, FilterControl.FilterType.Reservations, _shiftAnalysis);
            DateFilterControl dateFilterControl = new DateFilterControl(_shiftAnalysis);
            DayOfWeekFilterControl dayOfWeekFilterControl = new DayOfWeekFilterControl(_shiftAnalysis);
            MonthFilterControl monthFilterControl = new MonthFilterControl(_shiftAnalysis);
            rdoAM.Checked = _shiftAnalysis.IsAM;
            rdoPM.Checked = !_shiftAnalysis.IsAM;
            flowFilters.Controls.Add(dateFilterControl);
            flowFilters.Controls.Add(resoControl);
            flowFilters.Controls.Add(dayOfWeekFilterControl);
            flowFilters.Controls.Add(monthFilterControl);
            flowFilters.Controls.Add(tempFilter);
            flowFilters.Controls.Add(rainFilter);
            flowFilters.Controls.Add(cloudFilter);
            flowFilters.Controls.Add(windMaxControl);
            flowFilters.Controls.Add(windAvgControl);
        }
        private void UpdateForFilters()
        {
            UpdateCountLabel();

        }

        private void ShiftFilterControl_Load(object sender, EventArgs e)
        {

        }

        private void cbHolidaysExcluded_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHolidaysExcluded.Checked) {
                cbHolidaysExcluded.BackColor = UITheme.CTAColor;
                cbHolidaysExcluded.Text = "Holidays Excluded";
                _shiftAnalysis.SetIsFilteredBySpecialEvent(true);
            }
            else {
                cbHolidaysExcluded.BackColor = UITheme.ButtonColor;
                cbHolidaysExcluded.Text = "Holidays Included";
                _shiftAnalysis.SetIsFilteredBySpecialEvent(false);
            }
        }

        private void rdoAM_CheckedChanged(object sender, EventArgs e)
        {
            if (_shiftAnalysis != null) {
                _shiftAnalysis.SetIsAM(rdoAM.Checked);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateShift?.Invoke();


        }

        private void UpdateChart()
        {
            //_areaManager = new DiningAreaManager();
            // ChartManager chartManager = new ChartManager(ShiftAnalysis.FilteredShifts, cartesianChart1);
            // chartManager.SetUpMiniStackedArea(_areaManager.DiningAreas);
        }

        public void UpdateCountLabel()
        {
            if (lblFilteredShiftCount.InvokeRequired) {
                lblFilteredShiftCount.Invoke(new Action(UpdateCountLabel));

            }
            else {
                lblFilteredShiftCount.Text = $"{_shiftAnalysis.FilteredShifts.Count} Shifts";
                lblAvg.Text = $"{ShiftAnalysis.FilteredShiftAvgSales:C0} Avg";
                lblMin.Text = $"{ShiftAnalysis.FilteredShiftMinSales:C0} Min";
                lblMax.Text = $"{ShiftAnalysis.FilteredShiftMaxSales:C0} Max";
                lblFilteredShiftCount.Visible = true;
                lblMin.Visible = true;
                lblMax.Visible = true;
                lblAvg.Visible = true;
                UpdateAreaLabels();
                // UpdateChart();
            }
        }

        private void UpdateAreaLabels()
        {
           foreach(Control c in flowDiningAreas.Controls) {
                if(c is RadioButton rdo) {
                    if(rdo.Checked) {
                        DiningArea area = (DiningArea)rdo.Tag;
                        DiningAreaStats areaStats = _shiftAnalysis.DiningAreaStats.FirstOrDefault(s => s.DiningAreaID == area.ID);
                        if (areaStats != null) {
                            lblAreaAvg.Text = $"Avg: {areaStats.AvgSales:C0}";
                            lblAreaMax.Text = $"Max: {areaStats.MaxSales:C0}";
                            lblAreaMin.Text = $"Min: {areaStats.MinSales:C0}";
                            lblAreaAvg.Visible = true;
                            lblAreaMax.Visible = true;
                            lblAreaMin.Visible = true;
                        }
                    }
                }
            }
        }

        private void btnViewStatsForm_Click(object sender, EventArgs e)
        {
            OpenSalesForm?.Invoke();
        }

        private RadioButton CreateAreaRadio(DiningArea area)
        {
            RadioButton btn = new RadioButton() {

                Image = UITheme.GetDiningAreaImage(area),
                Size = new Size(flowDiningAreas.Width / (_areaManager.DiningAreas.Count - 1), flowDiningAreas.Height),
                Margin = new System.Windows.Forms.Padding(0, 0, 0, 0),
                Tag = area
            };
            btn.Click += areaButtonClicked;
            UITheme.FormatCTAButton(btn);
            btn.BackColor = UITheme.ButtonColor;
            btn.FlatAppearance.CheckedBackColor = UITheme.DarkenColor(.3f, UITheme.ButtonColor);
            //toolTip.SetToolTip(btn, area.Name);
            if (area.ID == 6) {
                btn.Enabled = false;
            }
            return btn;
        }
        private void areaButtonClicked(object? sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            DiningArea area = (DiningArea)radioButton.Tag;
            if (radioButton.Checked) {
                DiningAreaStats areaStats = _shiftAnalysis.DiningAreaStats.FirstOrDefault(s => s.DiningAreaID == area.ID);
                if (areaStats != null) {
                    lblAreaAvg.Text = $"Avg: {areaStats.AvgSales:C0}";
                    lblAreaMax.Text = $"Max: {areaStats.MaxSales:C0}";
                    lblAreaMin.Text = $"Min: {areaStats.MinSales:C0}";
                    lblAreaAvg.Visible = true;
                    lblAreaMax.Visible = true;
                    lblAreaMin.Visible = true;
                }

            }
        }

        private void cbDayOfStats_CheckedChanged(object sender, EventArgs e)
        {
            if(cbDayOfStats.Checked) {
                cbDayOfStats.BackColor = UITheme.NoColor;
                ToggleDayOf?.Invoke(true);
            }
            else {
                cbDayOfStats.BackColor = Color.LightGray;
                ToggleDayOf?.Invoke(false);
            }
        }
    }
}
