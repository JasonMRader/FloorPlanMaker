﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorplanClassLibrary
{
    public class DiningArea
    {
        public DiningArea(string name, bool isInside) 
        {
            Name = name;
            IsInside = isInside;
        
        }
        public DiningArea(string name, bool isInside, bool isCocktail, int ID, float testSales)
        {
            this.Name = name;
            this.IsInside = isInside;
            this.ID = ID;
            this.TestSales = testSales;
            this.IsCocktail = isCocktail;
        }
        public DiningArea() { }
        public TableSalesManager TableSalesManager = new TableSalesManager();
        public float ExpectedSales { get; private set; }
        public float TestSales { get; set; }
       
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public bool IsInside { get; set; }
        public bool IsCocktail { get; set; }
        public List<Table>? Tables { get; set; }

        public void SetTableSales(List<TableStat> tableStats)
        {
            float totalSales = 0f;

            // Separate AM and PM sales
            var amStats = tableStats.Where(t => t.IsLunch).ToList();
            var pmStats = tableStats.Where(t => !t.IsLunch).ToList();

            // Convert TableStatNumber to int for accurate filtering
            int minInsideTable = 120;
            int maxInsideTable = 154;
            int minOutsideTable = 1300;
            int maxOutsideTable = 1699;

            // Filter and calculate inside bar sales for AM
            var insideBarSalesAM = amStats
                .Where(t => int.TryParse(t.TableStatNumber, out int num) && num >= minInsideTable && num <= maxInsideTable)
                .Sum(t => t.Sales ?? 0);

            // Filter and calculate outside bar sales for AM
            var outsideBarSalesAM = amStats
                .Where(t => int.TryParse(t.TableStatNumber, out int num) && num >= minOutsideTable && num <= maxOutsideTable)
                .Sum(t => t.Sales ?? 0);

            // Filter and calculate inside bar sales for PM
            var insideBarSalesPM = pmStats
                .Where(t => int.TryParse(t.TableStatNumber, out int num) && num >= minInsideTable && num <= maxInsideTable)
                .Sum(t => t.Sales ?? 0);

            // Filter and calculate outside bar sales for PM
            var outsideBarSalesPM = pmStats
                .Where(t => int.TryParse(t.TableStatNumber, out int num) && num >= minOutsideTable && num <= maxOutsideTable)
                .Sum(t => t.Sales ?? 0);


            foreach (Table table in this.Tables)
            {
                if (table.TableNumber == "INSIDE BAR")
                {
                    float insideBarSales = insideBarSalesAM + insideBarSalesPM;
                    table.AverageSales = insideBarSales;
                    totalSales += insideBarSales;
                }
                else if (table.TableNumber == "OUTSIDE BAR")
                {
                    float outsideBarSales = outsideBarSalesAM + outsideBarSalesPM;
                    table.AverageSales = outsideBarSales;
                    totalSales += outsideBarSales;
                }
                else if (table.TableNumber == "308" || table.TableNumber == "418")
                {
                    var matchedStatAM = amStats.FirstOrDefault(t => t.TableStatNumber == "308" || t.TableStatNumber == "418");
                    var matchedStatPM = pmStats.FirstOrDefault(t => t.TableStatNumber == "308" || t.TableStatNumber == "418");

                    float matchedSalesAM = matchedStatAM?.Sales ?? 0;
                    float matchedSalesPM = matchedStatPM?.Sales ?? 0;

                    table.AverageSales = matchedSalesAM + matchedSalesPM;
                    totalSales += matchedSalesAM + matchedSalesPM;
                }
                else if (table.TableNumber == "309" || table.TableNumber == "419")
                {
                    var matchedStatAM = amStats.FirstOrDefault(t => t.TableStatNumber == "309" || t.TableStatNumber == "419");
                    var matchedStatPM = pmStats.FirstOrDefault(t => t.TableStatNumber == "309" || t.TableStatNumber == "419");

                    float matchedSalesAM = matchedStatAM?.Sales ?? 0;
                    float matchedSalesPM = matchedStatPM?.Sales ?? 0;

                    table.AverageSales = matchedSalesAM + matchedSalesPM;
                    totalSales += matchedSalesAM + matchedSalesPM;
                }
                else
                {
                    var matchedStatAM = amStats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                    var matchedStatPM = pmStats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);

                    float matchedSalesAM = matchedStatAM?.Sales ?? 0;
                    float matchedSalesPM = matchedStatPM?.Sales ?? 0;

                    table.AverageSales = matchedSalesAM + matchedSalesPM;
                    totalSales += matchedSalesAM + matchedSalesPM;
                }
            }
            ExpectedSales = totalSales;
        }
        public float GetTotalSalesForDateAndIsAm(bool isLunch, DateOnly dateOnly)
        {
            List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(isLunch, dateOnly);
            float totalAreaSales = 0f;
            foreach (Table table in Tables)
            {
                var matchedStat = stats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                if (matchedStat != null)
                {
                    table.AverageSales = (float)matchedStat.Sales;
                    totalAreaSales += (float)matchedStat.Sales;

                }
                else { table.AverageSales = 0; }

            }
            

            return totalAreaSales;
        }


        public int GetMaxCovers()
        {
            if (Tables == null)
            {
                return 0;
            }
            int maxCovers = 0;  
            foreach(Table table in Tables)
            {
                maxCovers += table.MaxCovers;
            }
            return maxCovers;
        }
        public float GetAverageSales()
        {
            if (Tables == null)
            {
                return 0;
            }
            float avgSales = 0;
            foreach (Table table in Tables)
            {
                avgSales += table.AverageSales;
            }
            return avgSales;
        }
        public enum SubArea
        {
            Awning,
            Extended,
            Dungeon,
            Fireplace,
            VIP

        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            DiningArea other = (DiningArea)obj;
            // Assuming ID is a unique identifier for DiningArea
            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            // Assuming ID is an int and a unique identifier for DiningArea
            return this.ID.GetHashCode();
        }
        
    }
}
