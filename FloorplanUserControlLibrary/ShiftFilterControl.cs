﻿using FloorplanClassLibrary;
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

namespace FloorplanUserControlLibrary
{
    public partial class ShiftFilterControl : UserControl
    {
        private ShiftAnalysis _shiftAnalysis { get; set; }
        public ShiftAnalysis ShiftAnalysis { get { return _shiftAnalysis; } }
        public event Action UpdateShift;
        public event Action OpenSalesForm;
        private DiningAreaManager _areaManager { get; set; }
        public ShiftFilterControl()
        {
            InitializeComponent();

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
            _areaManager = new DiningAreaManager();
            ChartManager chartManager = new ChartManager(ShiftAnalysis.FilteredShifts, cartesianChart1);
            chartManager.SetUpMiniStackedArea(_areaManager.DiningAreas);
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
                UpdateChart();
            }
        }

        private void btnViewStatsForm_Click(object sender, EventArgs e)
        {
            OpenSalesForm?.Invoke();
        }
    }
}
