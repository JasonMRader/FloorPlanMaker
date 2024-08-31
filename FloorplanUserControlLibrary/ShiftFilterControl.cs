using FloorplanClassLibrary;
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
        public ShiftFilterControl()
        {
            InitializeComponent();

        }
        public void SetShiftAnalysis(ShiftAnalysis shiftAnalysis)
        {
            _shiftAnalysis = shiftAnalysis;
            FilterControl tempFilter = new FilterControl("Temp", 80, 90, FilterControl.FilterType.Temperature, shiftAnalysis);
            FilterControl rainFilter = new FilterControl("Rain", 0, 0, FilterControl.FilterType.Rain, shiftAnalysis);
            flowFilters.Controls.Add(tempFilter);
            flowFilters.Controls.Add(rainFilter);
        }

        private void ShiftFilterControl_Load(object sender, EventArgs e)
        {

        }
    }
}
