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

namespace FloorPlanMakerUI
{
    public partial class frmSpecialDates : Form
    {
        public frmSpecialDates()
        {
            InitializeComponent();
        }

        private void frmSpecialDates_Load(object sender, EventArgs e)
        {
            LoadEventTypeComboBox();
            RefreshSpecialEventListBoxes();
        }

        private void btnCreateEvent_Click(object sender, EventArgs e)
        {
            SpecialEventDate newEventDate = new SpecialEventDate(dtpEventDate.Value, 
                (SpecialEventDate.OutlierType)cboType.SelectedItem, txtEventName.Text);
            SqliteDataAccess.SaveNewEventDate(newEventDate);
            RefreshSpecialEventListBoxes();
        }
        private void RefreshSpecialEventListBoxes()
        {
            lbUpcomingEvents.Items.Clear();
            lbPastEvents.Items.Clear();
            DateOnly today = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            List<SpecialEventDate> allEvents = SqliteDataAccess.LoadSpecialEvents();
            List<SpecialEventDate> pastEvents = allEvents.Where(e => e.DateOnly < today).ToList();
            List<SpecialEventDate> futureEvents = allEvents.Where(e => e.DateOnly >= today).ToList();
            foreach(SpecialEventDate specialEventDate in pastEvents)
            {
                lbPastEvents.Items.Add(specialEventDate);
            }
            foreach(SpecialEventDate specialEventDate in futureEvents)
            {
                lbUpcomingEvents.Items.Add(specialEventDate);
            }
        }
        private void LoadEventTypeComboBox()
        {
            foreach (SpecialEventDate.OutlierType type in Enum.GetValues(typeof(SpecialEventDate.OutlierType)))
            {
                cboType.Items.Add(type);
            }
        }
    }
}
