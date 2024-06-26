﻿using System;
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
        public ServerShiftHistory() { }
        public ServerShiftHistory(Server server, DateOnly start, DateOnly end)
        {
            this.Server = server;
            GetShiftsForDateRange(start, end);
            PopulateServerSections();
            PopulateTableCounts();
            SetPercentagesAndCounts();
            PopulateShiftTables();
        }
        public ServerShiftHistory(Server server, DateOnly start, DateOnly end, List<DayOfWeek> days)
        {
            this.Server = server;
            GetShiftsForDateRange(start, end);
            FilterByWeekDay(days);
            PopulateServerSections();
            PopulateTableCounts();
            SetPercentagesAndCounts();
            PopulateShiftTables();
        }
        public ServerShiftHistory(Server server, DateOnly start, DateOnly end, bool isLunch)
        {
            this.Server = server;
            GetShiftsForDateRangeAndIsLunch(start, end, isLunch);
            PopulateServerSections();
            PopulateTableCounts();
            SetPercentagesAndCounts();
            PopulateShiftTables();
        }
        public ServerShiftHistory(Server server, DateOnly start, DateOnly end, bool isLunch, List<DayOfWeek> days)
        {
            this.Server = server;
            GetShiftsForDateRangeAndIsLunch(start, end, isLunch);
            FilterByWeekDay(days);
            PopulateServerSections();
            PopulateTableCounts();
            SetPercentagesAndCounts();
            PopulateShiftTables();
        }
        public Server Server { get; set; }  
        public List<Section> Sections { get; set; }
        public Dictionary<string, int> TableCounts { get; set; }
        public Dictionary<EmployeeShift, string> ShiftTables { get; set; } = new Dictionary<EmployeeShift, string>();


        public float OutsidePercentage { get; set; } = 0f;
        public float CocktailShiftPercentage { get; set; } = 0f;
        public float ClosingPercentage { get; set; } = 0f;
        public float TeamWaitPercentage { get; set; } = 0f;
        public List<EmployeeShift> filteredShifts = new List<EmployeeShift>();
        public List<EmployeeShift> weekdayFilteredShifts = new List<EmployeeShift>();
        public int OutsideShiftCount { get; set; }
        public int CocktailShiftCount { get; set; }
        public int ClosingShiftCount { get; set; }
        public int TeamWaitShiftCount { get; set; }
        public void SetPercentagesAndCounts()
        {
            int shifts = this.filteredShifts.Count();
            int outsideCount = 0;
            int coctailCount = 0;
            int closingCount = 0;
            int TeamWaitCount = 0;
            foreach (EmployeeShift empShift in this.filteredShifts)
            {
                if (!empShift.IsInside)
                {
                    outsideCount++;
                }
                if (empShift.IsCloser) 
                {
                    closingCount++;
                }
                if (empShift.IsCocktail)
                {
                    coctailCount++;
                }
                if(empShift.IsTeamWait)
                {
                    TeamWaitCount++;
                }
            }
            if (shifts > 0)
            {
                OutsideShiftCount = outsideCount;
                CocktailShiftCount = coctailCount;
                ClosingShiftCount = closingCount;
                TeamWaitShiftCount = TeamWaitCount;

                OutsidePercentage = (float)outsideCount / (float)shifts;
                CocktailShiftPercentage = (float)coctailCount / (float)shifts;
                ClosingPercentage = (float)closingCount / (float)shifts;
                TeamWaitPercentage = (float)TeamWaitCount / (float)shifts;
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
        public void FilterByWeekDay(List<DayOfWeek> daysUsed)
        {
            weekdayFilteredShifts.Clear();
            foreach(EmployeeShift shift in this.filteredShifts)
            {
                if (daysUsed.Contains(shift.Date.DayOfWeek))
                {
                    weekdayFilteredShifts.Add(shift);
                }
            }
            filteredShifts.Clear();
            filteredShifts = weekdayFilteredShifts;
        }
        public void FilterByMaxNumberOfShifts(int numberOfShifts)
        {
            filteredShifts = filteredShifts.OrderByDescending(s => s.Date).ToList();
            filteredShifts = filteredShifts.Take(numberOfShifts).ToList();
        }
        public void PopulateShiftTables()
        {
            Dictionary<EmployeeShift, string> shiftTables = new Dictionary<EmployeeShift, string>();

            foreach (EmployeeShift empShift in this.filteredShifts)
            {
                var section = SqliteDataAccess.LoadSectionForShiftHistory(empShift.SectionID);
                if (section.Tables != null)
                {
                    var tableNumbers = section.Tables.Select(t => t.TableNumber).ToList();
                    string tableNumbersString = string.Join(", ", tableNumbers);
                    shiftTables[empShift] = tableNumbersString;
                }
            }

            this.ShiftTables = shiftTables;
        }




    }
}
