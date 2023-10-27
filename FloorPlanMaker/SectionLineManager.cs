﻿using FloorplanClassLibrary;
using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace FloorPlanMakerUI
{
    public class SectionLineManager
    {
        public List<TableControl> TableControls { get; private set; } = new List<TableControl>();
        public List<SectionLine> SectionLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> TopLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> RightLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> BottomLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> LeftLines { get; private set; } = new List<SectionLine>();
        private List<string> testData = new List<string>();
        public Dictionary<Section, List<TableControl>> SectionToTableControls { get; private set; } = new Dictionary<Section, List<TableControl>>();

        public SectionLineManager(List<TableControl> tableControls)
        {
            TableControls = tableControls;

            // Populate the dictionary based on the list of table controls
            foreach (var tableControl in tableControls)
            {
                if (tableControl.Section != null) // Assuming Section is a nullable type
                {
                    if (!SectionToTableControls.ContainsKey(tableControl.Section))
                    {
                        SectionToTableControls[tableControl.Section] = new List<TableControl>();
                    }
                    SectionToTableControls[tableControl.Section].Add(tableControl);
                }
            }
        }

        public void AddTableControl(TableControl tableControl)
        {
            TableControls.Add(tableControl);
        }

        public void DrawSectionLines()
        {
            foreach (Section section in this.SectionToTableControls.Keys)
            {

            }
        }
        public void DrawSeparationLines(Panel pnlFloorPlan)
        {
            SectionLines.Clear();

            foreach (var currentTable in TableControls)
            {
                var closestTables = TableControls
                    .Where(t => t != currentTable)
                    .OrderBy(t => CalculateDistance(t, currentTable))
                    .Take(6)
                    .ToList();

                foreach (var adjacentTable in closestTables)
                {
                    if (currentTable.Section != adjacentTable.Section)
                    {
                        if (Math.Abs(currentTable.Top - adjacentTable.Top) < currentTable.Height) // Assume nearly horizontal
                        {
                            int startX, startY, endX, endY;

                            startY = (currentTable.Top);
                            endY = (currentTable.Bottom);

                            if (currentTable.Left < adjacentTable.Left) // currentTable is to the left of adjacentTable
                            {
                                startX = (currentTable.Right + adjacentTable.Left) / 2;

                                endX = startX;
                            }
                            else
                            {
                                startX = (currentTable.Left + adjacentTable.Right) / 2;

                                endX = startX;
                            }

                            SectionLine sectionLine = new SectionLine();
                            sectionLine.StartPoint = new System.Drawing.Point(startX, startY);
                            sectionLine.EndPoint = new System.Drawing.Point(endX, endY);
                            sectionLine.Invalidate();
                            //pnlFloorPlan.Controls.Add(sectionLine);
                            //pnlFloorPlan.CreateGraphics().DrawLine(Pens.Black, startX, startY, endX, endY);
                        }
                        if (Math.Abs(currentTable.Left - adjacentTable.Left) < currentTable.Width) // Assume nearly vertical
                        {
                            int startX, startY, endX, endY;

                            startX = currentTable.Left;
                            endX = currentTable.Right;

                            if (currentTable.Top < adjacentTable.Top) // currentTable is above adjacentTable
                            {
                                startY = (currentTable.Bottom + adjacentTable.Top) / 2;
                                endY = startY;
                            }
                            else
                            {
                                startY = (currentTable.Top + adjacentTable.Bottom) / 2;
                                endY = startY;
                            }
                            bool hasIntersectingTable = TableControls.Any(table =>
                                
                                table != currentTable &&
                                table != adjacentTable &&
                                IsVerticallyBetween(currentTable, adjacentTable, table)
                            );

                            if (!hasIntersectingTable)
                            {
                                SectionLine sectionLine = new SectionLine();
                                sectionLine.StartPoint = new System.Drawing.Point(startX, startY);
                                sectionLine.EndPoint = new System.Drawing.Point(endX, endY);
                                sectionLine.currentTableNumber = currentTable.Table.TableNumber;
                                sectionLine.adjacentTableNumber = adjacentTable.Table.TableNumber;
                                sectionLine.Invalidate();
                                //pnlFloorPlan.Controls.Add(sectionLine);
                                SectionLines.Add(sectionLine);
                                testData.Add("########## Line added: " +currentTable + ", " + adjacentTable);
                            }
                            else
                            {
                                testData.Add("Line Not Added: " + currentTable + ", " + adjacentTable);
                            }


                            //pnlFloorPlan.CreateGraphics().DrawLine(Pens.Black, startX, startY, endX, endY);
                        }
                        else
                        {
                            // Diagonal
                            int startX, startY, endX, endY;

                            if (currentTable.Left < adjacentTable.Left) // currentTable is to the left of adjacentTable
                            {
                                startX = currentTable.Right;
                                endX = adjacentTable.Left;
                            }
                            else
                            {
                                startX = adjacentTable.Right;
                                endX = currentTable.Left;
                            }

                            if (currentTable.Top < adjacentTable.Top) // currentTable is above adjacentTable
                            {
                                startY = currentTable.Bottom;
                                endY = adjacentTable.Top;
                            }
                            else
                            {
                                startY = adjacentTable.Bottom;
                                endY = currentTable.Top;
                            }

                            //pnlFloorPlan.CreateGraphics().DrawLine(Pens.Black, startX, startY, endX, endY);
                        }
                    }
                }
            }
            foreach (SectionLine sectionLine in SectionLines)
            {
                pnlFloorPlan.Controls.Add(sectionLine);
            }
        }
        

        public double CalculateDistance(TableControl a, TableControl b)
        {
            return Math.Sqrt(Math.Pow(a.Left - b.Left, 2) + Math.Pow(a.Top - b.Top, 2));
        }

        public bool IsVerticallyBetween(TableControl currentTable, TableControl adjacentTable, TableControl tableChecked)
        {
            testData.Add("Current Table: " + currentTable + ", adjacentTable: " + adjacentTable + ", TableChecked: " + tableChecked);
            bool isVerticallyPositioned;
            // Check if the top of the checked table is below the current Table 
            // AND the Checked Tables Bottom is Above the Adjacent Tables top
            //adjacentTable is Above
            if (currentTable.Top < adjacentTable.Top)
            {
                isVerticallyPositioned =
              tableChecked.Top > currentTable.Bottom &&
              tableChecked.Bottom < adjacentTable.Top;
            }
            
            //adjacentTable is below
            else
            {
                isVerticallyPositioned =
             tableChecked.Bottom < currentTable.Top &&
             tableChecked.Top > adjacentTable.Bottom;
            }
            testData.Add(isVerticallyPositioned.ToString());

            // EITHER the right of the checked Table is greater than the left of the currentTable 
            // OR the left of the checkedTable is less than the right of the currentTable
            bool overlapsWithCurrentHorizontally =
        (tableChecked.Right > currentTable.Left && tableChecked.Right < currentTable.Right) ||
        (tableChecked.Left > currentTable.Left && tableChecked.Left < currentTable.Right);
            testData.Add(overlapsWithCurrentHorizontally.ToString());
            //isVerticallyPositioned = overlapsWithCurrentHorizontally;
            bool overlapsWithAdjacentHorizontally =
        (tableChecked.Right > adjacentTable.Left && tableChecked.Right < adjacentTable.Right) ||
        (tableChecked.Left > adjacentTable.Left && tableChecked.Left < adjacentTable.Right);
            testData.Add(overlapsWithAdjacentHorizontally.ToString());

            return isVerticallyPositioned && (overlapsWithCurrentHorizontally || overlapsWithAdjacentHorizontally);

        }

        public bool IsHorizontallyBetween(TableControl currentTable, TableControl AdjacentTable, TableControl tableChecked)
        {
            // Check if candidate table's X-axis range is between the other two tables
            bool isHorizontallyBetween =
                (tableChecked.Left > Math.Min(currentTable.Left, AdjacentTable.Left) &&
                 tableChecked.Left < Math.Max(currentTable.Right, AdjacentTable.Right)) ||
                (tableChecked.Right > Math.Min(currentTable.Left, AdjacentTable.Left) &&
                 tableChecked.Right < Math.Max(currentTable.Right, AdjacentTable.Right));

            // Ensure candidate table's Y-axis range does not overlap with either of the two tables
            bool doesNotOverlapVertically =
                tableChecked.Bottom < Math.Min(currentTable.Top, AdjacentTable.Top) ||
                tableChecked.Top > Math.Max(currentTable.Bottom, AdjacentTable.Bottom);

            return isHorizontallyBetween && doesNotOverlapVertically;
        }
        public void MakeSectionTableOutlines()
        {
            float thickness = 5f;
            int gap = 6;
            foreach(TableControl tableControl in this.TableControls)
            {
                SectionLine topLine = new SectionLine(tableControl.Left- gap,tableControl.Top - gap, tableControl.Right + gap, tableControl.Top - gap, thickness);
                SectionLines.Add(topLine);
                TopLines.Add(topLine);
                SectionLine rightLine = new SectionLine(tableControl.Right + gap, tableControl.Top - gap, tableControl.Right + gap, tableControl.Bottom + gap, thickness);
                SectionLines.Add(rightLine);
                RightLines.Add(rightLine);
                SectionLine bottomLine = new SectionLine(tableControl.Right + gap, tableControl.Bottom + gap, tableControl.Left - gap, tableControl.Bottom + gap, thickness);
                SectionLines.Add(bottomLine);
                BottomLines.Add(bottomLine);
                SectionLine leftLine = new SectionLine(tableControl.Left - gap, tableControl.Bottom + gap, tableControl.Left - gap, tableControl.Top - gap, thickness);
                SectionLines.Add(leftLine);
                LeftLines.Add(leftLine);

            }
        }
        //public void RemoveBottomLines(Panel panel)
        //{
        //    TopLines = TopLines.OrderBy(sl => sl.StartPoint.Y).ToList();
        //    List<SectionLine> bottomLinesToRemove = new List<SectionLine>();
        //    foreach (SectionLine bottomLine in BottomLines)
        //    {
        //        foreach (SectionLine topLine in TopLines)
        //        {
        //            if (bottomLine.StartPoint.Y < topLine.StartPoint.Y &&
        //                bottomLine.StartPoint.X > topLine.StartPoint.X &&
        //                bottomLine.EndPoint.X < topLine.EndPoint.X)
        //            {
        //                bottomLinesToRemove.Add(bottomLine);
        //                break; // Break out of the inner loop since we found a matching topLine
        //            }
        //        }
        //    }
        //    foreach (SectionLine lineToRemove in bottomLinesToRemove)
        //    {
        //        panel.Controls.Remove(lineToRemove);
        //        BottomLines.Remove(lineToRemove);
        //    }

        //}
        public void RemoveBottomLines(Panel panel)
        {
            // Sort TopLines based on the Y coordinate of the StartPoint.
            TopLines = TopLines.OrderBy(sl => sl.StartPoint.Y).ToList();

            // Create lists to hold bottom lines and top lines that should be removed
            List<SectionLine> bottomLinesToRemove = new List<SectionLine>();
            List<SectionLine> topLinesToRemove = new List<SectionLine>();
            List<SectionLine> newTopLines = new List<SectionLine>();

            foreach (SectionLine bottomLine in BottomLines)
            {
                foreach (SectionLine topLine in TopLines)
                {
                    if (bottomLine.StartPoint.Y < topLine.StartPoint.Y &&
                        bottomLine.StartPoint.X > topLine.StartPoint.X &&
                        bottomLine.EndPoint.X < topLine.EndPoint.X)
                    {
                        bottomLinesToRemove.Add(bottomLine);

                        // Add the corresponding topLine to be removed
                        topLinesToRemove.Add(topLine);

                        // Create the two new TopLines
                        float thickness = topLine.LineThickness; // Assuming you want to use the same thickness
                        SectionLine newTopLine1 = new SectionLine(topLine.StartPoint.X, topLine.StartPoint.Y, bottomLine.StartPoint.X, topLine.StartPoint.Y, thickness);
                        SectionLine newTopLine2 = new SectionLine(bottomLine.EndPoint.X, topLine.StartPoint.Y, topLine.EndPoint.X, topLine.StartPoint.Y, thickness);

                        newTopLines.Add(newTopLine1);
                        newTopLines.Add(newTopLine2);

                        break; // Break out of the inner loop since we found a matching topLine
                    }
                }
            }

            // Now remove the bottom lines and top lines that met the criteria from the panel and lists
            foreach (SectionLine lineToRemove in bottomLinesToRemove)
            {
                panel.Controls.Remove(lineToRemove);
                BottomLines.Remove(lineToRemove);
            }
            foreach (SectionLine lineToRemove in topLinesToRemove)
            {
                panel.Controls.Remove(lineToRemove);
                TopLines.Remove(lineToRemove);
            }

            // Add the new top lines to the panel and the TopLines list
            foreach (SectionLine newTopLine in newTopLines)
            {
                panel.Controls.Add(newTopLine);
                TopLines.Add(newTopLine);
            }
        }

        // You may add other methods or functionality as per your needs.
    }

}
