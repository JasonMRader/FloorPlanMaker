using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class HotSchedulesDataAccess
    {
        private static List<HotSchedulesSchedule> todaySchedule = new List<HotSchedulesSchedule>();
        private static List<HotSchedulesSchedule> tomorrowSchedule = new List<HotSchedulesSchedule>();
        public static List<HotSchedulesSchedule> TodaySchedule
        {
            get { return todaySchedule; }
        }
        public static List<HotSchedulesSchedule> TommorrowSchedule
        {
            get { return tomorrowSchedule; }
        }
        public static async Task InitializeAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly tomorrow = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
            
            todaySchedule = await HotSchedulesApiAccess.GetSchedule(today);
            tomorrowSchedule = await HotSchedulesApiAccess.GetSchedule(tomorrow);

        }
    }
}
