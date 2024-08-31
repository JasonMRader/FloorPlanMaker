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
    public partial class DateFilterControl : UserControl
    {
        private bool _choosing = false;
        private int minInt = 0;
        private int maxInt = 0;
        private double minDouble = 0;
        private double maxDouble = 0;
        private bool isInt { get; set; } = false;
        private string filterName { get; set; }
        private FilterType filterType { get; set; }
        private ShiftAnalysis shiftAnalysis { get; set; }
        public enum FilterType
        {
            Temperature,
            Rain,
            Clouds,
            WindMax,
            WindAvg,
            Reservations
        }
        public DateFilterControl()
        {
            InitializeComponent();
        }

        private void DateFilterControl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
