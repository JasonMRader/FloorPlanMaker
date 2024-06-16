using FloorplanClassLibrary;
using FloorplanUserControlLibrary.Properties;
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
    public partial class ShiftImgDisplay : UserControl
    {

        public ShiftImgDisplay(EmployeeShift employeeShift)
        {
            InitializeComponent();
            this.employeeShift = employeeShift;
            toolTip.SetToolTip(picShiftType, employeeShift.Date.ToString("ddd, M/d"));
            toolTip.SetToolTip(picWeekDay, employeeShift.Date.ToString("ddd, M/d"));
            picWeekDay.Image = GetDayOfWeekImage();
            picShiftType.Image = GetOutsideImage();
        }
        private Image GetOutsideImage()
        {
            if (this.employeeShift.IsInside)
            {
                return Resources.InsideSolid;
            }
            else
            {
                return Resources.OutSideSolid;
            }
        }
        private Image GetDayOfWeekImage()
        {
            Image image = null;
            DayOfWeek dayOfWeek = this.employeeShift.Date.DayOfWeek;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    image = Resources.MondayText;
                    break;
                case DayOfWeek.Tuesday:
                    image = Resources.TuesdayText;
                    break;
                case DayOfWeek.Wednesday:
                    image = Resources.WednesdayText;
                    break;
                case DayOfWeek.Thursday:
                    image = Resources.ThursdayText;
                    break;
                case DayOfWeek.Friday:
                    image = Resources.FridayText;
                    break;
                case DayOfWeek.Saturday:
                    image = Resources.SaturdayText;
                    break;
                case DayOfWeek.Sunday:
                    image = Resources.SundayText;
                    break;
                default:
                    throw new InvalidOperationException("Unknown day of the week");

            }
            return image;
        }
        private EmployeeShift employeeShift { get; set; }
        private ToolTip toolTip = new ToolTip();
    }
}
