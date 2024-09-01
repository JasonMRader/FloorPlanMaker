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

namespace FloorplanUserControlLibrary
{
    public partial class ShiftFilterControl : UserControl
    {
        private ShiftAnalysis _shiftAnalysis { get; set; }
        public ShiftAnalysis ShiftAnalysis { get { return _shiftAnalysis; } }
        public event Action UpdateShift;
        public ShiftFilterControl()
        {
            InitializeComponent();

        }
        public void SetShiftAnalysis(ShiftAnalysis shiftAnalysis)
        {
            if(_shiftAnalysis != null) {
                _shiftAnalysis.FilterUpdated -= UpdateForFilters;
            }
            _shiftAnalysis = shiftAnalysis;
            shiftAnalysis.FilterUpdated += UpdateForFilters;
            FilterControl tempFilter = new FilterControl("Temp", 80, 90, FilterControl.FilterType.Temperature, shiftAnalysis);
            FilterControl rainFilter = new FilterControl("Rain", 0, 0, FilterControl.FilterType.Rain, shiftAnalysis);
            DateFilterControl dateFilterControl = new DateFilterControl(shiftAnalysis);
            DayOfWeekFilterControl dayOfWeekFilterControl = new DayOfWeekFilterControl(shiftAnalysis);
            MonthFilterControl monthFilterControl = new MonthFilterControl(shiftAnalysis);
            rdoAM.Checked = _shiftAnalysis.IsAM;
            rdoPM.Checked = !_shiftAnalysis.IsAM;
            flowFilters.Controls.Add(dateFilterControl);
            flowFilters.Controls.Add(dayOfWeekFilterControl);
            flowFilters.Controls.Add(monthFilterControl);
            flowFilters.Controls.Add(tempFilter);
            flowFilters.Controls.Add(rainFilter);
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
            _shiftAnalysis.SetIsAM(rdoAM.Checked);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateShift?.Invoke();

        }

        public void UpdateCountLabel()
        {
            if (lblFilteredShiftCount.InvokeRequired) {
                lblFilteredShiftCount.Invoke(new Action(UpdateCountLabel));
            }
            else {
                lblFilteredShiftCount.Text = $"{_shiftAnalysis.FilteredShifts.Count} Filtered Shifts";
                lblFilteredShiftCount.Visible = true;
            }
        }

    }
}
