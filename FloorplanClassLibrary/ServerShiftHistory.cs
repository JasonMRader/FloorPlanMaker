using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FloorplanClassLibrary
{
    public class ServerShiftHistory
    {
        public ServerShiftHistory(Server server, DateOnly start, DateOnly end)
        {
            this.Server = server;
            GetShiftsForDateRange(start, end);
            PopulateServerSections();
            PopulateTableCounts();
            SetInsideOutsidePercentage();
        }
        public Server Server { get; set; }  
        public List<Section> Sections { get; set; }
        public Dictionary<string, int> TableCounts { get; set; }
        public float OutsidePercentage { get; set; } = 0f;
        public List<EmployeeShift> filteredShifts = new List<EmployeeShift>();  
        public void SetInsideOutsidePercentage()
        {
            int shifts = this.filteredShifts.Count();
            int OutsideCount = 0;
            foreach (EmployeeShift empShift in this.filteredShifts)
            {
                if (!empShift.IsInside)
                {
                    OutsideCount++;
                }
            }
            if (shifts > 0)
            {
                OutsidePercentage = (float)OutsideCount / (float)shifts;
            }
        }
        public void PopulateServerSections()
        {
            List<Section> sections = new List<Section>();
            foreach(EmployeeShift empShift in this.filteredShifts)
            {
                sections.Add(SqliteDataAccess.LoadSectionForShiftHistory(empShift.SectionID));
            }
            this.Sections = sections;            
        }
        public void PopulateTableCounts()
        {
            Dictionary<string, int> tableCounts = new Dictionary<string, int>();

            foreach (Section section in this.Sections)
            {
                if (section.Tables == null) continue;

                foreach (Table table in section.Tables)
                {
                    if (tableCounts.ContainsKey(table.TableNumber))
                    {
                        tableCounts[table.TableNumber]++;
                    }
                    else
                    {
                        tableCounts[table.TableNumber] = 1;
                    }
                }
            }

            this.TableCounts = tableCounts;
        }
        public override string ToString()
        {
            
            return this.Server.AbbreviatedName + " (History)";
        }
        public void GetShiftsForDateRange(DateOnly start, DateOnly end)
        {
            List<EmployeeShift> employeeShifts = new List<EmployeeShift>();

            foreach (EmployeeShift shift in this.Server.Shifts)
            {
                DateOnly shiftDate = DateOnly.FromDateTime(shift.Date);
                if (shiftDate >= start && shiftDate <= end)
                {
                    employeeShifts.Add(shift);
                }
            }

            filteredShifts = employeeShifts;
        }
        public void GetShiftsForDateRangeAndIsLunch(DateOnly start, DateOnly end, bool isLunch)
        {
            List<EmployeeShift> employeeShifts = new List<EmployeeShift>();

            foreach (EmployeeShift shift in this.Server.Shifts)
            {
                DateOnly shiftDate = DateOnly.FromDateTime(shift.Date);
                if (shiftDate >= start && shiftDate <= end && shift.isLunch == isLunch)
                {
                    employeeShifts.Add(shift);
                }
            }

            filteredShifts = employeeShifts;
        }


    }
}
