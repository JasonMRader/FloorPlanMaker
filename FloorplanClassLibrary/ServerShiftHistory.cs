using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ServerShiftHistory
    {
        public ServerShiftHistory(Server server)
        {
            this.Server = server;
            PopulateServerSections();
            PopulateTableCounts();
        }
        public Server Server { get; set; }  
        public List<Section> Sections { get; set; }
        public Dictionary<string, int> TableCounts { get; set; }
        public void PopulateServerSections()
        {
            List<Section> sections = new List<Section>();
            foreach(EmployeeShift empShift in this.Server.Shifts)
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
        public List<EmployeeShift> GetShiftsForDateRange(DateOnly start, DateOnly end)
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

            return employeeShifts;
        }


    }
}
