using FloorplanClassLibrary;
using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace FloorPlanMakerUI
{
    public class SectionLineManager
    {
        public List<TableControl> TableControls { get; private set; } = new List<TableControl>();
        private List<TableControl> intrudingTables { get; set;} = new List<TableControl>();
        private List<TableControl> topIntrudingTables { get; set; } = new List<TableControl>();
        private List<TableControl> rightIntrudingTables { get; set; } = new List<TableControl>();
        private List<TableControl> bottomIntrudingTables { get; set; } = new List<TableControl>();
        private List<TableControl> leftIntrudingTables { get; set; } = new List<TableControl>();
        public List<SectionLine> SectionLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> TopLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> RightLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> BottomLines { get; private set; } = new List<SectionLine>();
        public List<SectionLine> LeftLines { get; private set; } = new List<SectionLine>();
        public List <SectionLine> ParallelLines { get; private set; } = new List<SectionLine> { };
        private int minX = int.MaxValue;
        private int maxX = int.MinValue;
        private int minY = int.MaxValue;
        private int maxY = int.MinValue;
        public Point BorderTopLeftPoint { get { return new Point(minX, minY); } }

        public Point BorderTopRightPoint { get { return new Point(maxX, minY); } }

        public Point BorderBottomRightPoint { get { return new Point(maxX, maxY); } }

        public Point BorderBottomLeftPoint { get { return new Point(minX, maxY); } }
        public enum ClosestBorder
        {
            None,
            Top,
            Right,
            Bottom,
            Left
        }
        public void AddTopLines(Panel panel)
        {
           
            foreach (Section section in this.SectionToTableControls.Keys)
            {
                
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];
                TableControl? current = this.TopLeftMost(sectionTableControls);
                while (current != null)
                {
                    SectionLine currentTopLine = this.TopLine(current);
                    currentTopLine.LineColor = section.Color;
                    currentTopLine.LineThickness = 10f;
                    currentTopLine.Section = section;
                    currentTopLine.Edge = SectionLine.BorderEdge.Top;
                    SectionLines.Add(currentTopLine);

                    TableControl? next = nextTopSectionLine(current, sectionTableControls);
                    if (next != null)
                    {
                        //Need to change this so that it does not 
                        SectionLine nextTopLine = this.TopLine(next);

                        // Same X Coordinate, Straight Line
                        if (currentTopLine.EndPoint.X == nextTopLine.StartPoint.X)
                        {
                            SectionLine sl = new SectionLine(currentTopLine.EndPoint.X, currentTopLine.EndPoint.Y,
                                                             nextTopLine.StartPoint.X, nextTopLine.StartPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            sl.Edge = SectionLine.BorderEdge.Top;
                            SectionLines.Add(sl);
                        }
                        // Next table's top is below the current table's top
                        else if (nextTopLine.StartPoint.Y > currentTopLine.EndPoint.Y)
                        {
                            current.RightLine.BackColor = section.Color;
                            current.Section = section;
                            SectionLines.Add(current.RightLine);  // Using the RightLine of the current table up to next table's top
                            SectionLines.Last().Edge = SectionLine.BorderEdge.Right;
                            

                            // Modify the current RightLine's endpoint to stop at the next table's top
                            SectionLines.Last().EndPoint = new Point(current.RightLine.EndPoint.X, nextTopLine.StartPoint.Y);
                            SectionLine sl = new SectionLine(SectionLines.Last().EndPoint, nextTopLine.StartPoint);
                            sl.Edge = SectionLine.BorderEdge.Top;
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add(sl);
                        }
                        // Next table's top is above the current table's top
                        else
                        {
                            // Horizontal line from current table's endpoint to next table's LeftLine
                            SectionLine sl = new SectionLine(currentTopLine.EndPoint.X, currentTopLine.EndPoint.Y,
                                                             next.LeftLine.StartPoint.X, currentTopLine.EndPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            sl.Edge = SectionLine.BorderEdge.Top;
                            SectionLines.Add(sl);

                            // Vertical line upwards from that point to next table's TopLine
                            SectionLine sLine = new SectionLine(next.LeftLine.StartPoint.X, currentTopLine.EndPoint.Y,
                                                             next.LeftLine.StartPoint.X, nextTopLine.StartPoint.Y);
                            sLine.Section = section;
                            sLine.Edge = SectionLine.BorderEdge.Left;
                            sLine.LineColor = section.Color;
                            SectionLines.Add(sLine);
                        }
                    }

                    current = next;
                }
                int minY = SectionLines.Last().EndPoint.Y;
                List<SectionLine> rightLines = RightSectionLines(sectionTableControls, minY);
                rightLines = AddRightConnectorLines(rightLines);
                SectionLines.AddRange(rightLines);
                foreach (SectionLine sectionLine in SectionLines)
                {
                    sectionLine.LineThickness = 15f;
                }

            }
            //SectionLines = OrderSectionLines(SectionLines);
            //UpdateSectionLinePositions();
            foreach (SectionLine sectionLine in this.SectionLines)
            {
                panel.Controls.Add(sectionLine);
            }

        }
        private bool IsPartiallyInsideBounds(TableControl tableControl)
        {
            return !(tableControl.BottomLine.EndPoint.Y < BorderTopLeftPoint.Y ||
                        tableControl.TopLine.StartPoint.Y > BorderBottomRightPoint.Y ||
                        tableControl.RightLine.EndPoint.X < BorderTopLeftPoint.X ||
                        tableControl.LeftLine.StartPoint.X > BorderBottomRightPoint.X);
        }


        private List<SectionLine> AddRightConnectorLines(List<SectionLine> rightLines)
        {
            rightLines = rightLines.OrderBy(l => l.StartPoint.Y).ToList();
            List<SectionLine> connectorLines = new List<SectionLine>();
            for (int i = 0; i < rightLines.Count - 1; i++)
            {
                //if the current Line is Left of the Next One
                if (rightLines[i].EndPoint.X < rightLines[i + 1].StartPoint.X)
                {
                    rightLines[i].EndPoint = new Point(rightLines[i].StartPoint.X, rightLines[1 + i].StartPoint.Y);
                    //SectionLine line = new SectionLine(rightLines[i].EndPoint, new Point(rightLines[i].StartPoint.X, rightLines[i + 1].StartPoint.Y), rightLines[i].Section);
                    SectionLine line2 = new SectionLine(new Point(rightLines[i].StartPoint.X, rightLines[i + 1].StartPoint.Y), rightLines[i + 1].StartPoint, rightLines[i].Section);
                    //SectionLine line2 = new SectionLine(rightLines[i].EndPoint, rightLines.[1+i].StartPoint, rightLines[i].Section);
                    //RightLines.Add(line);
                    //rightLines[i].LineColor = rightLines[i].Section.Color;
                    //rightLines[i].Edge = SectionLine.BorderEdge.Right;
                    //connectorLines.Add(line);
                    TopLines.Add(line2);
                    line2.LineColor = rightLines[i].Section.Color;
                    line2.Edge = SectionLine.BorderEdge.Top;
                    connectorLines.Add(line2);
                }
                //Current is right of the next one
                else if (rightLines[i].EndPoint.X > rightLines[i + 1].StartPoint.X)
                {
                   
                    SectionLine line = new SectionLine(rightLines[i].EndPoint, new Point(rightLines[i + 1].StartPoint.X, rightLines[i].EndPoint.Y), rightLines[i].Section);
                    //SectionLine line2 = new SectionLine(new Point(rightLines[i + 1].StartPoint.X, rightLines[i].EndPoint.Y), rightLines[i + 1].StartPoint, rightLines[i + 1].Section);
                    BottomLines.Add(line);
                    line.LineColor = rightLines[i].Section.Color;
                    line.Edge = SectionLine.BorderEdge.Bottom;
                    connectorLines.Add(line);
                    rightLines[i + 1].StartPoint = new Point(rightLines[i + 1].StartPoint.X, rightLines[i].EndPoint.Y);
                    //RightLines.Add(line2);
                    //line2.LineColor = rightLines[i].Section.Color;
                    //line2.Edge = SectionLine.BorderEdge.Right;
                    //connectorLines.Add(line2);
                }
                else
                {
                    SectionLine line = new SectionLine(rightLines[i].EndPoint, rightLines[i + 1].StartPoint, rightLines[i].Section);
                    RightLines.Add(line);
                    line.LineColor = rightLines[i].Section.Color;
                    line.Edge= SectionLine.BorderEdge.Right;
                    connectorLines.Add(line);
                }
            }

            rightLines.AddRange(connectorLines);
            return rightLines;

        }
        private TableControl? nextTopSectionLine(TableControl tableControl, List<TableControl> sectionTableControls)
        {
            Point referencePoint = tableControl.TopRight;
            double minDistance = double.MaxValue;
            TableControl? nearestTable = null;

            foreach (TableControl tc in sectionTableControls)
            {
                if (tc == tableControl) continue;  // We don't want to compare the table with itself.

                if (tc.TopLeft.X <= tableControl.TopLeft.X) continue; // Ensure that the table's TopLeft corner is to the right of the current table's TopLeft.
                //if it is, I also need to make sure the topleft is the to right of the current tables topRIGHT corner, if so, i need to make adjustments 
                double distance = Distance(referencePoint, tc.TopLeft);

                if (hasTableAbove(tc, sectionTableControls)) continue;

                // If this table has the same distance as the current nearest table, but is higher, update the nearest table
                if (distance == minDistance && tc.Top < nearestTable?.Top)
                {
                    nearestTable = tc;
                }
                // If this table is nearer than the current nearest table, update both the distance and the nearest table
                else if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTable = tc;
                }
            }

            return nearestTable;
        }
        private List<SectionLine> RightSectionLines(List<TableControl> sectionTables, int minY)
        {
            List<SectionLine> RightLines = new List<SectionLine>();
            foreach (TableControl tableControl in sectionTables)
            {
                if (!hasTableToRight(tableControl, sectionTables) && tableControl.Top > minY)
                {
                    RightLines.Add(new SectionLine(tableControl.RightLine.StartPoint, tableControl.RightLine.EndPoint));
                    RightLines.Last().Edge = SectionLine.BorderEdge.Right;
                    RightLines.Last().LineColor = tableControl.Section.Color;
                }
            }
            return RightLines;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private bool hasTableAbove(TableControl tableControl, List<TableControl> tableControls)
        {
            if (tableControl == null) return false;
            foreach (TableControl tc in tableControls)
            {
                if (tc == tableControl)
                    continue;

                bool isDirectlyAbove = tc.Bottom <= tableControl.Top && tc.Top < tableControl.Top;
                bool isHorizontallyOverlapping = (tc.Left < tableControl.Right) && (tc.Right > tableControl.Left);

                if (isDirectlyAbove && isHorizontallyOverlapping)
                    return true;
            }

            return false;
        }
        private bool hasTableToRight(TableControl tableControl, List<TableControl> tableControls)
        {
            if (tableControl == null) return false;

            foreach (TableControl tc in tableControls)
            {
                if (tc == tableControl)
                    continue;

                bool isDirectlyToRight = tc.Left >= tableControl.Right;
                bool isVerticallyOverlapping = (tc.Top < tableControl.Bottom) && (tc.Bottom > tableControl.Top);

                if (isDirectlyToRight && isVerticallyOverlapping)
                    return true;
            }

            return false;
        }
       
        private bool hasTableBelow(TableControl tableControl, List<TableControl> tableControls)
        {
            if (tableControl == null) return false;

            foreach (TableControl tc in tableControls)
            {
                if (tc == tableControl)
                    continue;

                // Check if a table is directly below the given table
                bool isDirectlyBelow = tc.Top >= tableControl.Bottom && tc.Bottom > tableControl.Bottom;

                // Check if the tables are horizontally overlapping
                bool isHorizontallyOverlapping = (tc.Left < tableControl.Right) && (tc.Right > tableControl.Left);

                if (isDirectlyBelow && isHorizontallyOverlapping)
                    return true;
            }

            return false;
        }

        private bool hasTableToLeft(TableControl tableControl, List<TableControl> tableControls)
        {
            if (tableControl == null) return false;

            foreach (TableControl tc in tableControls)
            {
                if (tc == tableControl)
                    continue;

                // Check if a table is directly to the left of the given table
                bool isDirectlyToLeft = tc.Right <= tableControl.Left && tc.Left < tableControl.Left;

                // Check if the tables are vertically overlapping
                bool isVerticallyOverlapping = (tc.Top < tableControl.Bottom) && (tc.Bottom > tableControl.Top);

                if (isDirectlyToLeft && isVerticallyOverlapping)
                    return true;
            }

            return false;
        }
        private bool tableControlIsLeftOfLine(TableControl tableControl, SectionLine sectionLine)
        {
            bool isDirectlyToLeft = tableControl.Right <= sectionLine.StartPoint.X;
            bool isVerticallyOverlapping = (tableControl.Top <= sectionLine.EndPoint.Y) && (tableControl.Bottom >= sectionLine.StartPoint.Y);
            return isDirectlyToLeft && isVerticallyOverlapping;
        }
        private bool tableControlIsRightOfLine(TableControl tableControl, SectionLine sectionLine)
        {
            bool isDirectlyToLeft = tableControl.Left >= sectionLine.StartPoint.X;
            bool isVerticallyOverlapping = (tableControl.Top <= sectionLine.EndPoint.Y) && (tableControl.Bottom >= sectionLine.StartPoint.Y);
            return isDirectlyToLeft && isVerticallyOverlapping;
        }
        private int midPointForVerticalLine(SectionLine sectionLine, List<TableControl> tableControls)
        {
            int midPoint = sectionLine.StartPoint.X;
            int closetTableFromSection = int.MaxValue;
            int closestTableFromOutSideOfSection = int.MaxValue;
            int RightTableLeftSideX = 0;
            int LeftTableRightSideX = 0;
            bool hasTableToLeft = false;
            bool hasTableToRight = false;   
            
            if (sectionLine.Edge == SectionLine.BorderEdge.Right)
            {
                foreach(TableControl tableControl in tableControls)
                {
                    if (tableControl.Section == sectionLine.Section && tableControlIsLeftOfLine(tableControl, sectionLine))
                    {
                        int distance = sectionLine.StartPoint.X - tableControl.Right;
                        if (distance < closetTableFromSection)
                        {
                            closetTableFromSection = distance;
                            hasTableToLeft = true;
                            LeftTableRightSideX = tableControl.Right;
                            
                        }
                    }
                    else if (tableControl.Section != sectionLine.Section && tableControlIsRightOfLine(tableControl, sectionLine))
                    {
                        int distance = tableControl.Left - sectionLine.StartPoint.X;
                        if (distance < closestTableFromOutSideOfSection)
                        {
                            closestTableFromOutSideOfSection = distance;
                            hasTableToRight = true;
                            RightTableLeftSideX = tableControl.Left;
                        }
                    }
                }
            }
            if (hasTableToRight && hasTableToLeft)
            {
                midPoint = (RightTableLeftSideX+LeftTableRightSideX) / 2;
            }
            return midPoint;
        }
        public void oldUpdateSectionLinePositions()
        {
            foreach (var line in SectionLines)
            {
                if (line.Edge == SectionLine.BorderEdge.Right)
                {
                    int newMidPointX = midPointForVerticalLine(line, TableControls);

                    // Calculate the distance we need to move the line
                    int deltaX = newMidPointX - line.StartPoint.X;
                    
                    // Move the line by that distance
                    line.StartPoint = new Point(line.StartPoint.X + deltaX, line.StartPoint.Y);
                    line.EndPoint = new Point(line.EndPoint.X + deltaX, line.EndPoint.Y);
                    
                    //line.BringToFront();
                    
                }
            }
        }
        private void AdjustAdjacentLines(SectionLine movedLine, int index)
        {
            // If there's a previous line and it belongs to the same section, adjust its end point
            if (index > 0 && SectionLines[index - 1].Section == movedLine.Section)
            {
                SectionLines[index - 1].EndPoint = movedLine.StartPoint;
            }

            // If there's a next line and it belongs to the same section, adjust its start point
            if (index < SectionLines.Count - 1 && SectionLines[index + 1].Section == movedLine.Section)
            {
                SectionLines[index + 1].StartPoint = movedLine.EndPoint;
            }
        }

        public void UpdateSectionLinePositions()
        {
            for (int i = 0; i < SectionLines.Count; i++)
            {
                var line = SectionLines[i];
                if (line.Edge == SectionLine.BorderEdge.Right)
                {
                    int newMidPointX = midPointForVerticalLine(line, TableControls);

                    // Calculate the distance we need to move the line
                    int deltaX = newMidPointX - line.StartPoint.X;

                    // Move the line by that distance
                    line.StartPoint = new Point(line.StartPoint.X + deltaX, line.StartPoint.Y);
                    line.EndPoint = new Point(line.EndPoint.X + deltaX, line.EndPoint.Y);

                    // Adjust the adjacent lines that belong to the same section
                    AdjustAdjacentLines(line, i);

                    //line.BringToFront(); // Uncomment if necessary
                }
            }
        }


        private bool IsInsideBounds(TableControl tableControl, Point topLeft, Point bottomRight)
        {
            bool isHorizontallyInside =
                (tableControl.LeftLine.StartPoint.X >= topLeft.X && tableControl.LeftLine.StartPoint.X <= bottomRight.X) || // Left edge is inside
                (tableControl.RightLine.EndPoint.X >= topLeft.X && tableControl.RightLine.EndPoint.X <= bottomRight.X);     // Right edge is inside

            bool isVerticallyInside =
                (tableControl.TopLine.StartPoint.Y >= topLeft.Y && tableControl.TopLine.StartPoint.Y <= bottomRight.Y) || // Top edge is inside
                (tableControl.BottomLine.EndPoint.Y >= topLeft.Y && tableControl.BottomLine.EndPoint.Y <= bottomRight.Y); // Bottom edge is inside

            return isHorizontallyInside && isVerticallyInside;
        }
        private ClosestBorder GetClosestBorder(TableControl tableControl, Point topLeft, Point bottomRight)
        {
            if (!IsInsideBounds(tableControl, topLeft, bottomRight))
                return ClosestBorder.None;

            double topDistance = tableControl.TopLine.StartPoint.Y - topLeft.Y;
            double bottomDistance = bottomRight.Y - tableControl.BottomLine.EndPoint.Y;
            double leftDistance = tableControl.LeftLine.StartPoint.X - topLeft.X;
            double rightDistance = bottomRight.X - tableControl.RightLine.EndPoint.X;

            double minDistance = Math.Min(Math.Min(topDistance, bottomDistance), Math.Min(leftDistance, rightDistance));

            if (minDistance == topDistance) return ClosestBorder.Top;
            if (minDistance == bottomDistance) return ClosestBorder.Bottom;
            if (minDistance == leftDistance) return ClosestBorder.Left;
            return ClosestBorder.Right;
        }
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
        public TableControl? TopLeftMost(List<TableControl> sectionTableControls)
        {
            sectionTableControls = sectionTableControls.OrderBy(x => x.Left).ToList();
            foreach (TableControl tableControl in sectionTableControls)
            {
                if (!hasTableAbove(tableControl, sectionTableControls))
                {
                    return tableControl;
                }
            }

            return null;
        }
        private TableControl TopRightMost(List<TableControl> sectionTableControls)
        {
            return sectionTableControls
                .OrderBy(tc => tc.TopRightLinePoint.Y)
                .ThenByDescending(tc => tc.TopRightLinePoint.X)
                .First();
        }
        public Dictionary<Section, List<SectionLine>> SeparateAndOrderSectionLines(List<SectionLine> lines)
        {
            // Separate lines by section
            var sectionLinesDict = new Dictionary<Section, List<SectionLine>>();
            foreach (var line in lines)
            {
                if (!sectionLinesDict.ContainsKey(line.Section))
                {
                    sectionLinesDict[line.Section] = new List<SectionLine>();
                }
                sectionLinesDict[line.Section].Add(line);
            }

            // Order lines within each section
            var orderedSectionLinesDict = new Dictionary<Section, List<SectionLine>>();
            foreach (var section in sectionLinesDict.Keys)
            {
                orderedSectionLinesDict[section] = OrderSectionLines(sectionLinesDict[section]);
            }

            return orderedSectionLinesDict;
        }

        public List<SectionLine> oldOrderSectionLines(List<SectionLine> lines)
        {
            List<SectionLine> orderedLines = new List<SectionLine>();

            // Find the first line
            SectionLine firstLine = lines.FirstOrDefault(line => !lines.Any(l => l.EndPoint == line.StartPoint));
            if (firstLine != null)
            {
                orderedLines.Add(firstLine);

                SectionLine currentLine = firstLine;
                while (true)
                {
                    SectionLine nextLine = lines.FirstOrDefault(line => line.StartPoint == currentLine.EndPoint && line != currentLine);
                    if (nextLine == null) break;

                    orderedLines.Add(nextLine);
                    currentLine = nextLine;
                }
            }

            return orderedLines;
        }
        public List<SectionLine> OrderSectionLines(List<SectionLine> lines)
        {
            List<SectionLine> orderedLines = new List<SectionLine>();
            List<SectionLine> workingList = new List<SectionLine>(lines); // Clone the list to work with

            while (workingList.Count > 0)
            {
                // Find the first line
                SectionLine firstLine = workingList.FirstOrDefault(line => !workingList.Any(l => l.EndPoint == line.StartPoint));
                if (firstLine == null) break; // No more "first" lines found, exit loop

                orderedLines.Add(firstLine);
                workingList.Remove(firstLine); // Remove the found line from the working list

                SectionLine currentLine = firstLine;
                while (true)
                {
                    SectionLine nextLine = workingList.FirstOrDefault(line => line.StartPoint == currentLine.EndPoint && line != currentLine);
                    if (nextLine == null) break;

                    orderedLines.Add(nextLine);
                    workingList.Remove(nextLine); // Remove the found line from the working list
                    currentLine = nextLine;
                }
            }

            return orderedLines;
        }


        private void moveLinesToMidpointBetweenTables(List<SectionLine> lines)
        {
            lines = OrderSectionLines(lines);
            foreach (SectionLine line in lines)
            {
                
            }
        }

        private List<string> testData = new List<string>();

        //---------------------------------------------------------------------------------
        //
        //-------------------UNUSED METHODS-----------------------------------------------------
        //
        //----------------------------------------------------------------------------------------
        private Point TopLeftPoint(List<TableControl> sectionTableControls)
        {
            return sectionTableControls
                .Select(tc => tc.TopLeftLinePoint)
                .OrderBy(p => p.X)
                .ThenBy(p => p.Y)
                .First();
        }
        private Point TopRightPoint(List<TableControl> sectionTableControls)
        {
            return sectionTableControls
                .Select(tc => tc.TopRightLinePoint) // Assuming you have a property named TopRightLinePoint in your TableControl class
                .OrderBy(p => p.Y)
                .ThenByDescending(p => p.X)
                .First();
        }
        private void setTableControlBounds(List<TableControl> tableControls)
        {
            foreach (TableControl tableControl in tableControls)
            {
                if (tableControl.LeftLine.StartPoint.X < this.minX)
                    this.minX = tableControl.LeftLine.StartPoint.X;

                if (tableControl.RightLine.EndPoint.X > this.maxX)
                    this.maxX = tableControl.RightLine.EndPoint.X;

                if (tableControl.TopLine.StartPoint.Y < this.minY)
                    this.minY = tableControl.TopLine.StartPoint.Y;

                if (tableControl.BottomLine.EndPoint.Y > this.maxY)
                    this.maxY = tableControl.BottomLine.EndPoint.Y;
            }
        }
        
       
        

        public void AddTableControl(TableControl tableControl)
        {
            TableControls.Add(tableControl);
        }

        public void oldDrawSectionLines(Panel panel)
        {
            foreach (Section section in this.SectionToTableControls.Keys)
            {
                minX = int.MaxValue;
                maxX = int.MinValue;
                minY = int.MaxValue;
                maxY = int.MinValue;
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];
                setTableControlBounds(sectionTableControls);
                SectionLine topBoarder = new SectionLine(BorderTopLeftPoint, BorderTopRightPoint);
                SectionLine bottomBoarder = new SectionLine(BorderBottomLeftPoint, BorderBottomRightPoint);
                SectionLine leftBoarder = new SectionLine(BorderTopLeftPoint, BorderBottomLeftPoint);
                SectionLine rightBoarder = new SectionLine(BorderTopRightPoint, BorderBottomRightPoint);
                topBoarder.LineThickness = 20f;
                topBoarder.LineColor = section.Color;
                rightBoarder.LineThickness = 15f;
                rightBoarder.LineColor = section.Color;
                bottomBoarder.LineThickness = 10f;
                bottomBoarder.LineColor = section.Color;
                leftBoarder.LineThickness = 5f;
                leftBoarder.LineColor = section.Color;
                panel.Controls.Add(topBoarder);
                panel.Controls.Add(leftBoarder);
                panel.Controls.Add(bottomBoarder);
                panel.Controls.Add(rightBoarder);
                
            }
        }
        public void DrawSectionLines(Panel panel)
        {
            foreach (Section section in this.SectionToTableControls.Keys)
            {
                minX = int.MaxValue;
                maxX = int.MinValue;
                minY = int.MaxValue;
                maxY = int.MinValue;
                List<SectionLine> linesToRemove = new List<SectionLine>();
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];
                setTableControlBounds(sectionTableControls);

                SectionLine topBoarder = new SectionLine(BorderTopLeftPoint, BorderTopRightPoint);
                SectionLine bottomBoarder = new SectionLine(BorderBottomLeftPoint, BorderBottomRightPoint);
                SectionLine leftBoarder = new SectionLine(BorderTopLeftPoint, BorderBottomLeftPoint);
                SectionLine rightBoarder = new SectionLine(BorderTopRightPoint, BorderBottomRightPoint);
                topBoarder.LineThickness = 20f;
                topBoarder.LineColor = section.Color;
                rightBoarder.LineThickness = 15f;
                rightBoarder.LineColor = section.Color;
                bottomBoarder.LineThickness = 10f;
                bottomBoarder.LineColor = section.Color;
                leftBoarder.LineThickness = 5f;
                leftBoarder.LineColor = section.Color;
                
                //TopLines.Add(topBoarder);
                //BottomLines.Add(bottomBoarder);
                //LeftLines.Add(leftBoarder);
                RightLines.Add(rightBoarder);
                // Now, check for tables from different sections inside the right border
                foreach (TableControl otherTable in TableControls)
                {
                    // Check if the table is from a different section and is partially inside the current section's right border
                    if (otherTable.Section != section && IsPartiallyInsideBounds(otherTable))
                    {
                        intrudingTables.Add(otherTable);
                        ClosestBorder border = GetClosestBorder(otherTable);
                        if (border == ClosestBorder.Top)
                        {

                            topIntrudingTables.Add(otherTable);
                            //RightLines.Remove(rightBoarder);
                            //AdjustBordersAroundTable(otherTable, section, linesToRemove);
                        }
                    }
                }
                foreach (TableControl otherTable in intrudingTables)
                {
                    // Check if the table is from a different section and is partially inside the current section's right border
                    if (otherTable.Section != section && IsPartiallyInsideBounds(otherTable))
                    {
                        ClosestBorder border = GetClosestBorder(otherTable);
                        if (border == ClosestBorder.Right)
                        {
                           
                            rightIntrudingTables.Add(otherTable);
                            //RightLines.Remove(rightBoarder);
                            //AdjustBordersAroundTable(otherTable, section, linesToRemove);
                        }
                    }
                }
                List<SectionLine> topBorderIntruderAdjustmeent = topBorderIntrudingTableAdjustments(topBoarder, section);
                List<SectionLine> rightBorderIntruderAdjustment = rightBorderIntrudingTableAdjustments(rightBoarder, section);
                foreach (SectionLine topAdjustment in topBorderIntruderAdjustmeent)
                {
                    TopLines.Add(topAdjustment);
                }
                foreach (SectionLine rightAdjustment in rightBorderIntruderAdjustment)
                {
                    RightLines.Add(rightAdjustment);
                }
                //foreach (SectionLine lineToRemove in linesToRemove)
                //{
                //    RightLines.Remove(lineToRemove);
                //}
                foreach (SectionLine sectionLine in TopLines)
                {
                    panel.Controls.Add(sectionLine);
                }
                //foreach (SectionLine sectionLine in BottomLines)
                //{
                //    panel.Controls.Add(sectionLine);
                //}
                //foreach (SectionLine sectionLine in LeftLines)
                //{
                //    panel.Controls.Add(sectionLine);
                //}
                //foreach (SectionLine sectionLine in RightLines)
                //{
                //    panel.Controls.Add(sectionLine);
                //}
                //panel.Controls.Add(topBoarder);
                //panel.Controls.Add(leftBoarder);
                //panel.Controls.Add(bottomBoarder);
                //panel.Controls.Add(rightBoarder);
            }
        }

        private List<SectionLine> topBorderIntrudingTableAdjustments(SectionLine topBoarder, Section section)
        {
            List<SectionLine> topLines = new List<SectionLine>();
            //TableControl tc = TopLeftMost(TableControls);
            foreach (TableControl tableControl in topIntrudingTables)
            {
                SectionLine sectionLine = new SectionLine(tableControl.BottomLeftLinePoint, tableControl.BottomRightLinePoint);
                sectionLine.LineColor = section.Color;
                topLines.Add(sectionLine);
            }
            if (!topLines.Any()) return
            // Sort the rightLines list based on the X value of the start point
            topLines = topLines.OrderBy(line => line.StartPoint.Y).ToList();
            
            for (int i = 0; i < topLines.Count - 1; i++)
            {
                SectionLine currentLine = topLines[i];
                SectionLine nextLine = topLines[i + 1];

                // Check if the lines overlap
                if ((currentLine.StartPoint.X < nextLine.EndPoint.X && currentLine.StartPoint.X > nextLine.StartPoint.X) ||
                    (currentLine.EndPoint.X < nextLine.EndPoint.X && currentLine.EndPoint.X > nextLine.StartPoint.X))
                {
                    // Adjust the current line to cover the entire overlap range
                    int newXStart = Math.Min(currentLine.StartPoint.X, nextLine.StartPoint.X);
                    int newXEnd = Math.Max(currentLine.EndPoint.X, nextLine.EndPoint.X);

                    currentLine.StartPoint = new Point(newXStart, currentLine.StartPoint.Y);
                    currentLine.EndPoint = new Point(newXEnd, currentLine.EndPoint.Y);


                    // Remove the next line
                    topLines.RemoveAt(i + 1);

                    // Decrement the iterator to check the current line against the next again
                    i--;
                }
            }
            topBoarder.EndPoint = new Point(topBoarder.StartPoint.X, topLines[0].StartPoint.Y);

            return topLines;
        }
        private List<SectionLine> rightBorderIntrudingTableAdjustments(SectionLine rightBoarder, Section section)
        {
            List<SectionLine> rightLines = new List<SectionLine>();
            
            foreach (TableControl tableControl in rightIntrudingTables)
            {
                SectionLine sectionLine = new SectionLine(tableControl.TopLeftLinePoint, tableControl.BottomLeftLinePoint);
                sectionLine.LineColor = section.Color;
                rightLines.Add(sectionLine);
            }
            if (!rightLines.Any())  return 
            // Sort the rightLines list based on the X value of the start point
            rightLines = rightLines.OrderBy(line => line.StartPoint.X).ToList();
            
            for (int i = 0; i < rightLines.Count - 1; i++)
            {
                SectionLine currentLine = rightLines[i];
                SectionLine nextLine = rightLines[i + 1];

                // Check if the lines overlap
                if (currentLine.EndPoint.Y >= nextLine.StartPoint.Y)
                {
                    // Adjust the current line to cover the entire overlap range
                    int newYStart = Math.Min(currentLine.StartPoint.Y, nextLine.StartPoint.Y);
                    int newYEnd = Math.Max(currentLine.EndPoint.Y, nextLine.EndPoint.Y);

                    currentLine.StartPoint = new Point(currentLine.StartPoint.X, newYStart);
                    currentLine.EndPoint = new Point(currentLine.EndPoint.X, newYEnd);

                    // Remove the next line
                    rightLines.RemoveAt(i + 1);

                    // Decrement the iterator to check the current line against the next again
                    i--;
                }
            }
            rightBoarder.EndPoint = new Point(rightBoarder.StartPoint.X, rightLines[0].StartPoint.Y);
            return rightLines;
        }


        // Adjust the section's borders based on table controls that might be inside the section's bounds.
        public void AdjustSectionBorders(Section section)
        {
            if (SectionToTableControls.ContainsKey(section))
            {
                foreach (TableControl table in SectionToTableControls[section])
                {
                    //AdjustBordersAroundTable(table, section);
                }
            }
        }

        private void AdjustBordersAroundTable(TableControl table, Section section, List<SectionLine> linesToRemove)
        {
            ClosestBorder border = GetClosestBorder(table);
            switch (border)
            {
                case ClosestBorder.Right:
                    // Remove the existing right border
                    //RightLines.RemoveAll(b => b.StartPoint.X > table.Left && b.StartPoint.X < table.Right);
                    SectionLine currentSectionRightBorder = RightLines.Find(b =>
                        b.StartPoint.Equals(BorderTopRightPoint) &&
                        b.EndPoint.Equals(BorderBottomRightPoint));

                    // Add it to the linesToRemove list
                    if (currentSectionRightBorder != null)
                    {
                        linesToRemove.Add(currentSectionRightBorder);
                    }
                    // Add the adjusted lines
                    RightLines.Add(new SectionLine(BorderTopRightPoint, new Point(BorderTopRightPoint.X,table.TopLeftLinePoint.Y)));
                    SectionLine newBottomLine = new SectionLine(RightLines.Last().EndPoint, table.TopLeftLinePoint);
                    BottomLines.Add(newBottomLine);
                    SectionLine newRightLine = new SectionLine(table.TopLeftLinePoint, table.BottomLeftLinePoint);
                    RightLines.Add(newRightLine);
                    SectionLine topLine = new SectionLine(RightLines.Last().EndPoint,table.BottomRightLinePoint);
                    CheckNewBorderLine(section, newBottomLine, newRightLine);
                    // I need a method here to check if any tables from my section are to the right of the newRightLine, and below the BottomLine I 
                    // added right before the newRightLine
                    break;

                // ... Similarly, handle other borders. ...

                default:
                    break;
            }
        }
        private void CheckNewBorderLine(Section section, SectionLine connectorLine, SectionLine newBorderLine)
        {
           
            List<TableControl> sectionTableControls = this.SectionToTableControls[section];
            foreach(TableControl tableControl in sectionTableControls)
            {
                if(tableControl.RightLine.StartPoint.X > newBorderLine.StartPoint.X && 
                    tableControl.TopLine.StartPoint.Y > connectorLine.StartPoint.Y)
                {
                    SectionLine newRightLine = new SectionLine();
                    SectionLine newConnectorLine = new SectionLine();

                    newBorderLine.EndPoint = new Point(newBorderLine.StartPoint.X, tableControl.TopLine.StartPoint.Y);
                    newConnectorLine.StartPoint = newBorderLine.EndPoint;
                    newConnectorLine.EndPoint = tableControl.RightLine.EndPoint;
                    newRightLine.StartPoint = newConnectorLine.EndPoint;
                    newRightLine.EndPoint = new Point(newRightLine.StartPoint.X, maxY);
                    RightLines.Add(newRightLine);
                    RightLines.Add(newConnectorLine);
                    


                }
            }
            RightLines.Add(newBorderLine);
        }

        


        private ClosestBorder GetClosestBorder(TableControl tableControl)
        {
            if (!IsPartiallyInsideBounds(tableControl))
                return ClosestBorder.None;

            double topDistance = tableControl.TopLine.StartPoint.Y - BorderTopLeftPoint.Y;
            double bottomDistance = BorderBottomRightPoint.Y - tableControl.BottomLine.EndPoint.Y;
            double leftDistance = tableControl.LeftLine.StartPoint.X - BorderTopLeftPoint.X;
            double rightDistance = BorderBottomRightPoint.X - tableControl.RightLine.EndPoint.X;

            double minDistance = Math.Min(Math.Min(topDistance, bottomDistance), Math.Min(leftDistance, rightDistance));

            if (minDistance == topDistance) return ClosestBorder.Top;
            if (minDistance == bottomDistance) return ClosestBorder.Bottom;
            if (minDistance == leftDistance) return ClosestBorder.Left;
            return ClosestBorder.Right;
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
                SectionLine topLine = tableControl.TopLine;// new SectionLine(tableControl.Left- gap,tableControl.Top - gap, tableControl.Right + gap, tableControl.Top - gap, thickness);
                
                SectionLines.Add(topLine);
                TopLines.Add(topLine);
                SectionLine rightLine = tableControl.RightLine;//new SectionLine(tableControl.Right + gap, tableControl.Top - gap, tableControl.Right + gap, tableControl.Bottom + gap, thickness);
                SectionLines.Add(rightLine);
                RightLines.Add(rightLine);
                SectionLine bottomLine = tableControl.BottomLine;//new SectionLine(tableControl.Right + gap, tableControl.Bottom + gap, tableControl.Left - gap, tableControl.Bottom + gap, thickness);
                SectionLines.Add(bottomLine);
                BottomLines.Add(bottomLine);
                SectionLine leftLine = tableControl.LeftLine;//new SectionLine(tableControl.Left - gap, tableControl.Bottom + gap, tableControl.Left - gap, tableControl.Top - gap, thickness);
                SectionLines.Add(leftLine);
                LeftLines.Add(leftLine);

            }
        }
        public void MakeTopLines(Panel panel)
        {
            MakeSectionTableOutlines();
            foreach(SectionLine topLine in TopLines)
            {
                if(hasLineAbove(topLine, TopLines))
                {
                    TopLines.Remove(topLine);
                }
            }
            foreach(SectionLine topLine in TopLines)
            {
                panel.Controls.Add(topLine);
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
            List<SectionLine> newLines = new List<SectionLine>();
            

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
                        SectionLine newTopLine1 = new SectionLine(topLine.StartPoint.X, topLine.StartPoint.Y, bottomLine.StartPoint.X, topLine.StartPoint.Y);
                        SectionLine newTopLine2 = new SectionLine(bottomLine.EndPoint.X, bottomLine.EndPoint.Y, topLine.EndPoint.X, bottomLine.EndPoint.Y);

                        newLines.Add(newTopLine1);
                        newLines.Add(newTopLine2);

                        break; // Break out of the inner loop since we found a matching topLine
                    }
                    if (bottomLine.StartPoint.Y < topLine.StartPoint.Y &&
                        topLine.StartPoint.X >  bottomLine.StartPoint.X &&
                        topLine.StartPoint.X <  bottomLine.EndPoint.X)
                    {
                        float thickness = topLine.LineThickness;
                        bottomLinesToRemove.Add(bottomLine);
                        topLinesToRemove.Add(topLine);
                        SectionLine newBottomLine = new SectionLine(bottomLine.StartPoint.X, bottomLine.StartPoint.Y, topLine.StartPoint.X, bottomLine.EndPoint.Y, thickness);
                        newLines.Add(newBottomLine);
                        SectionLine newTopLine = new SectionLine(bottomLine.EndPoint.X, topLine.StartPoint.Y, topLine.EndPoint.X, topLine.EndPoint.Y, thickness);
                        newLines.Add(newTopLine);
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
            foreach (SectionLine newTopLine in newLines)
            {
                panel.Controls.Add(newTopLine);
                TopLines.Add(newTopLine);
            }
        }
        public void RemoveRightLines(Panel panel)
        {
            // Sort LeftLines based on the X coordinate of the StartPoint.
            LeftLines = LeftLines.OrderBy(sl => sl.StartPoint.X).ToList();

            // Create lists to hold right lines and left lines that should be removed
            List<SectionLine> rightLinesToRemove = new List<SectionLine>();
            List<SectionLine> leftLinesToRemove = new List<SectionLine>();
            List<SectionLine> newLines = new List<SectionLine>();

            foreach (SectionLine rightLine in RightLines)
            {
                foreach (SectionLine leftLine in LeftLines)
                {
                    // Check if the rightLine's start is to the left of leftLine's start and its endpoints are vertically within the bounds of leftLine.
                    if (rightLine.StartPoint.X < leftLine.StartPoint.X &&
                        rightLine.StartPoint.Y > leftLine.StartPoint.Y &&
                        rightLine.EndPoint.Y < leftLine.EndPoint.Y)
                    {
                        rightLinesToRemove.Add(rightLine);
                        leftLinesToRemove.Add(leftLine);

                        float thickness = leftLine.LineThickness;

                        // Create the two new LeftLines
                        SectionLine newLeftLine1 = new SectionLine(leftLine.StartPoint.X, leftLine.StartPoint.Y, rightLine.StartPoint.X, leftLine.StartPoint.Y, thickness);
                        SectionLine newLeftLine2 = new SectionLine(rightLine.EndPoint.X, leftLine.StartPoint.Y, leftLine.EndPoint.X, leftLine.StartPoint.Y, thickness);

                        newLines.Add(newLeftLine1);
                        newLines.Add(newLeftLine2);

                        break;
                    }

                    // Check if the rightLine's start is to the left of leftLine's start and its start point is vertically within the bounds of leftLine.
                    if (rightLine.StartPoint.X < leftLine.StartPoint.X &&
                        leftLine.StartPoint.Y > rightLine.StartPoint.Y &&
                        leftLine.StartPoint.Y < rightLine.EndPoint.Y)
                    {
                        float thickness = leftLine.LineThickness;
                        rightLinesToRemove.Add(rightLine);
                        leftLinesToRemove.Add(leftLine);

                        SectionLine newRightLine = new SectionLine(rightLine.StartPoint.X, rightLine.StartPoint.Y, leftLine.StartPoint.X, rightLine.EndPoint.Y, thickness);
                        newLines.Add(newRightLine);

                        SectionLine newLeftLine = new SectionLine(rightLine.EndPoint.X, leftLine.StartPoint.Y, leftLine.EndPoint.X, leftLine.EndPoint.Y, thickness);
                        newLines.Add(newLeftLine);
                    }
                }
            }

            // Remove the right lines and left lines that met the criteria from the panel and lists
            foreach (SectionLine lineToRemove in rightLinesToRemove)
            {
                panel.Controls.Remove(lineToRemove);
                RightLines.Remove(lineToRemove);
            }
            foreach (SectionLine lineToRemove in leftLinesToRemove)
            {
                panel.Controls.Remove(lineToRemove);
                LeftLines.Remove(lineToRemove);
            }

            // Add the new left lines to the panel and the LeftLines list
            foreach (SectionLine newLeftLine in newLines)
            {
                panel.Controls.Add(newLeftLine);
                LeftLines.Add(newLeftLine);
            }
        }
       
        public void AddRightLines(Panel panel)
        {
            foreach (Section section in this.SectionToTableControls.Keys)
            {
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];
                TableControl? current = this.TopRightMost(sectionTableControls); // Start with the bottom-right most table

                while (current != null)
                {
                    SectionLine currentRightLine = this.RightLine(current); // Assuming you have a RightLine method
                    currentRightLine.LineColor = section.Color;
                    currentRightLine.LineThickness = 10f;
                    currentRightLine.Section = section;
                    SectionLines.Add(currentRightLine);

                    TableControl? next = nextRightSectionLine(current, sectionTableControls); // Assuming you have a nextRightSectionLine method

                    if (next != null)
                    {
                        SectionLine nextRightLine = this.RightLine(next);

                        // Same Y Coordinate, Straight Line
                        if (currentRightLine.EndPoint.Y == nextRightLine.StartPoint.Y)
                        {
                            SectionLine sl = new SectionLine(currentRightLine.EndPoint.X, currentRightLine.EndPoint.Y,
                                                             nextRightLine.StartPoint.X, nextRightLine.StartPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add(sl);
                        }
                        // Next table's right is to the left of the current table's right
                        else if (nextRightLine.StartPoint.X < currentRightLine.EndPoint.X)
                        {
                            current.BottomLine.BackColor = section.Color;
                            current.Section = section;
                            SectionLines.Add(current.BottomLine);  // Using the BottomLine of the current table up to next table's right

                            // Modify the current BottomLine's endpoint to stop at the next table's right
                            SectionLines.Last().EndPoint = new Point(nextRightLine.StartPoint.X, current.BottomLine.EndPoint.Y);
                            SectionLine sl = new SectionLine(SectionLines.Last().EndPoint, nextRightLine.StartPoint);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add(sl);
                        }
                        // Next table's right is to the right of the current table's right
                        else
                        {
                            // Horizontal line from current table's endpoint to next table's BottomLine
                            SectionLine sl = new SectionLine(currentRightLine.EndPoint.X, currentRightLine.EndPoint.Y,
                                                             currentRightLine.EndPoint.X, next.BottomLine.StartPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add(sl);

                            // Vertical line to the right from that point to next table's RightLine
                            SectionLine sLine = new SectionLine(currentRightLine.EndPoint.X, next.BottomLine.StartPoint.Y,
                                                                nextRightLine.StartPoint.X, next.BottomLine.StartPoint.Y);
                            sLine.Section = section;
                            sLine.LineColor = section.Color;
                            SectionLines.Add(sLine);
                        }
                    }

                    current = next;
                }
            }

            foreach (SectionLine sectionLine in this.SectionLines)
            {
                panel.Controls.Add(sectionLine);
            }
        }

        public void AddRightBorders(Panel panel)
        {
            // Assuming you have the SectionLines already sorted in the desired order
            foreach (Section section in this.SectionToTableControls.Keys)
            {
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];

                // Get the last table control added for top lines for this section
                TableControl? last = sectionTableControls.LastOrDefault();

                if (last == null) continue;

                // Continue drawing the right side borders
                while (last != null)
                {
                    SectionLine currentRightLine = this.RightLine(last);
                    currentRightLine.LineColor = section.Color;
                    currentRightLine.LineThickness = 10f;
                    currentRightLine.Section = section;
                    SectionLines.Add(currentRightLine);

                    last = nextRightSectionLine(last, sectionTableControls);  // This method should get the next table control for the right border
                }
            }

            foreach (SectionLine sectionLine in this.SectionLines)
            {
                panel.Controls.Add(sectionLine);
            }
        }

        // Assuming the method TopLine returns the top border of a TableControl
        private SectionLine RightLine(TableControl tableControl)
        {
            return tableControl.RightLine;
        }

        // This method should get the next TableControl for which we need to draw the right border
        private TableControl? nextRightSectionLine(TableControl tableControl, List<TableControl> sectionTableControls)
        {
            Point referencePoint = tableControl.BottomRight;
            double minDistance = double.MaxValue;
            TableControl? nearestTable = null;

            foreach (TableControl tc in sectionTableControls)
            {
                if (tc == tableControl) continue;  // We don't want to compare the table with itself.

                if (tc.TopRight.Y <= tableControl.TopRight.Y) continue; // Ensure that the table's TopLeft corner is to the right of the current table's TopLeft.
                //if it is, I also need to make sure the topleft is the to right of the current tables topRIGHT corner, if so, i need to make adjustments 
                double distance = Distance(referencePoint, tc.TopRight);

                if (hasTableToRight(tc, sectionTableControls)) continue;

                // If this table has the same distance as the current nearest table, but is higher, update the nearest table
                if (distance == minDistance && tc.Right > nearestTable?.Right)
                {
                    nearestTable = tc;
                }
                // If this table is nearer than the current nearest table, update both the distance and the nearest table
                else if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTable = tc;
                }
            }

            return nearestTable;
            return null;
        }

        public void AddTopLine(Panel panel)
        {
            foreach(Section section in this.SectionToTableControls.Keys)
            {
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];
                TableControl? current = this.TopLeftMost(sectionTableControls);
                while (current != null)
                {
                    SectionLines.Add(this.TopLine(current));
                    current = nextTopSectionLine(current, sectionTableControls);
                }

            }
            foreach(SectionLine sectionLine in this.SectionLines)
            {
                panel.Controls.Add(sectionLine);
            }
            
        }
        public SectionLine TopLine(TableControl tableControl)
        {
            SectionLine sectionLine = tableControl.TopLine;

            return sectionLine;
        }
       

       

        public void AddBottomLines(Panel panel)
        {
            foreach (Section section in this.SectionToTableControls.Keys)
            {
                List<TableControl> sectionTableControls = this.SectionToTableControls[section];
                TableControl? current = this.BottomRightMost(sectionTableControls);
                while (current != null)
                {
                    SectionLine currentBottomLine = this.BottomLine(current);
                    currentBottomLine.LineColor = section.Color;
                    currentBottomLine.LineThickness = 10f;
                    currentBottomLine.Section = section;
                    SectionLines.Add(currentBottomLine);

                    TableControl? next = nextBottomSectionLine(current, sectionTableControls);
                   

                    if (next != null)
                    {
                        SectionLine nextBottomLine = this.BottomLine(next);

                        // Same X Coordinate, Straight Line
                        if (currentBottomLine.EndPoint.X == nextBottomLine.StartPoint.X)
                        {
                            SectionLine sl = new SectionLine(currentBottomLine.EndPoint.X, currentBottomLine.EndPoint.Y,
                                                             nextBottomLine.StartPoint.X, nextBottomLine.StartPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add((sl));
                        }
                        // Next table's bottom is above the current table's bottom
                        else if (nextBottomLine.StartPoint.Y < currentBottomLine.EndPoint.Y && !hasTableBelow(next,sectionTableControls))
                        {
                           
                            //SectionLines.Add(current.LeftLine);  // Using the LeftLine of the current table up to next table's bottom
                            SectionLine sl = new SectionLine(currentBottomLine.StartPoint.X,currentBottomLine.StartPoint.Y , currentBottomLine.StartPoint.X , nextBottomLine.StartPoint.Y);
                            // Modify the current LeftLine's endpoint to stop at the next table's bottom
                            //SectionLines.Last().EndPoint = new Point(current.LeftLine.EndPoint.X, nextBottomLine.StartPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add((sl));
                            SectionLine sLine = new SectionLine(SectionLines.Last().StartPoint, nextBottomLine.EndPoint);
                            sLine.LineColor = section.Color;
                            sLine.Section = section;
                            SectionLines.Add((sLine));
                        }
                        // Next table's bottom is below the current table's bottom
                        else
                        {
                            if (hasTableBelow(next, sectionTableControls))
                            {
                                next = nextBottomSectionLine(next, sectionTableControls);
                                nextBottomLine = this.BottomLine(next);

                            }
                            if (nextBottomLine == null)
                            {
                                break;
                            }
                            // Horizontal line from current table's endpoint to next table's RightLine
                            SectionLine sLine =new SectionLine(currentBottomLine.StartPoint.X, currentBottomLine.EndPoint.Y,
                                                             next.RightLine.StartPoint.X, currentBottomLine.EndPoint.Y);
                            sLine.LineColor = section.Color;
                            sLine.Section = section;
                            SectionLines.Add((sLine));
                            // Vertical line downwards from that point to next table's BottomLine
                            SectionLine sl = new SectionLine(next.RightLine.StartPoint.X, currentBottomLine.EndPoint.Y,
                                                             next.RightLine.StartPoint.X, nextBottomLine.StartPoint.Y);
                            sl.LineColor = section.Color;
                            sl.Section = section;
                            SectionLines.Add((sl));
                        }
                    }


                    current = next;
                }
            }

            foreach (SectionLine sectionLine in this.SectionLines)
            {
                panel.Controls.Add(sectionLine);
            }
        }

        public SectionLine BottomLine(TableControl tableControl)
        {
            if(tableControl == null)
            {
                return null;
            }
            SectionLine sectionLine = tableControl.BottomLine;
            return sectionLine;
        }

        public TableControl? BottomRightMost(List<TableControl> sectionTableControls)
        {
            sectionTableControls = sectionTableControls.OrderByDescending(x => x.Right).ThenByDescending(y => y.Bottom).ToList();
            foreach (TableControl tableControl in sectionTableControls)
            {
                if (!hasTableBelow(tableControl, sectionTableControls))
                {
                    return tableControl;
                }
            }

            return null;
        }

        private TableControl? nextBottomSectionLine(TableControl tableControl, List<TableControl> sectionTableControls)
        {
            Point referencePoint = tableControl.BottomLeft;
            double minDistance = double.MaxValue;
            TableControl? nearestTable = null;

            foreach (TableControl tc in sectionTableControls)
            {
                if (tc == tableControl) continue;

                if (tc.BottomRight.X >= tableControl.BottomRight.X) continue;

                double distance = Distance(referencePoint, tc.BottomRight);

                //if (hasTableBelow(tc, sectionTableControls)) continue;
                //if (tc.Bottom < tableControl.Bottom && !hasTableBelow(tc,sectionTableControls)) continue;

                if (distance == minDistance && tc.Bottom > nearestTable?.Bottom)
                {
                    nearestTable = tc;
                }
                else if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTable = tc;
                }
            }

            return nearestTable;
        }

       

        private bool hasLineAbove(SectionLine sectionLine, List<SectionLine> sectionLines)
        {
            if (sectionLine == null) return false;
            foreach (SectionLine sl in sectionLines)
            {
                if (sl == sectionLine)
                    continue;

                bool isAbove = sl.StartPoint.Y <= sectionLine.StartPoint.Y;
                bool isHorizontallyOverlapping = (sl.StartPoint.X < sectionLine.EndPoint.X) && (sl.StartPoint.X > sl.StartPoint.X);

                if (isAbove && isHorizontallyOverlapping)
                    return true;
            }

            return false;
        }
        private List<(SectionLine, SectionLine)> FindParallelLinesFromDifferentSections()
        {
            var parallelLines = new List<(SectionLine, SectionLine)>();

            for (int i = 0; i < SectionLines.Count; i++)
            {
                for (int j = i + 1; j < SectionLines.Count; j++)
                {
                    if (SectionLines[i].Section != SectionLines[j].Section && AreParallel(SectionLines[i], SectionLines[j]))
                    {
                        parallelLines.Add((SectionLines[i], SectionLines[j]));
                    }
                }
            }

            return parallelLines;
        }

        private bool AreParallel(SectionLine line1, SectionLine line2)
        {
            return (line1.StartPoint.Y == line1.EndPoint.Y && line2.StartPoint.Y == line2.EndPoint.Y) || // Both horizontal
                   (line1.StartPoint.X == line1.EndPoint.X && line2.StartPoint.X == line2.EndPoint.X);   // Both vertical
        }
        private bool HasObstacleBetween(SectionLine line1, SectionLine line2, List<TableControl> tableControls)
        {
            foreach (var line in SectionLines)
            {
                if (IsBetween(line1, line2, line))
                    return true;
            }

            foreach (var tableControl in tableControls)
            {
                if (IsBetween(line1, line2, tableControl))
                    return true;
            }

            return false;
        }

        private bool IsBetween(SectionLine line1, SectionLine line2, Control control)
        {
            // Let's assume the lines are vertical
            if (line1.StartPoint.X == line1.EndPoint.X && line2.StartPoint.X == line2.EndPoint.X)
            {
                // Both lines are vertical, so check if the control's X is between the two lines
                float minX = Math.Min(line1.StartPoint.X, line2.StartPoint.X);
                float maxX = Math.Max(line1.StartPoint.X, line2.StartPoint.X);

                return control.Left > minX && control.Right < maxX;
            }
            // Let's assume the lines are horizontal
            else if (line1.StartPoint.Y == line1.EndPoint.Y && line2.StartPoint.Y == line2.EndPoint.Y)
            {
                // Both lines are horizontal, so check if the control's Y is between the two lines
                float minY = Math.Min(line1.StartPoint.Y, line2.StartPoint.Y);
                float maxY = Math.Max(line1.StartPoint.Y, line2.StartPoint.Y);

                return control.Top > minY && control.Bottom < maxY;
            }
            return false;
        }

        private SectionLine CreateLineBetween(SectionLine line1, SectionLine line2)
        {
            if (AreParallel(line1, line2) && !HasObstacleBetween(line1, line2, this.SectionToTableControls.Values.SelectMany(list => list).ToList()))
            {
                // If both lines are vertical
                if (line1.StartPoint.X == line1.EndPoint.X && line2.StartPoint.X == line2.EndPoint.X)
                {
                    int avgX = (line1.StartPoint.X + line2.StartPoint.X) / 2;
                    int minY = Math.Max(line1.StartPoint.Y, line2.StartPoint.Y);
                    int maxY = Math.Min(line1.EndPoint.Y, line2.EndPoint.Y);

                    return new SectionLine(avgX, minY, avgX, maxY, 15f);  // Using the thickness of line1 as an example
                }
                // If both lines are horizontal
                else if (line1.StartPoint.Y == line1.EndPoint.Y && line2.StartPoint.Y == line2.EndPoint.Y)
                {
                    int avgY = (line1.StartPoint.Y + line2.StartPoint.Y) / 2;
                    int minX = Math.Max(line1.StartPoint.X, line2.StartPoint.X);
                    int maxX = Math.Min(line1.EndPoint.X, line2.EndPoint.X);

                    return new SectionLine(minX, avgY, maxX, avgY, 15f);  // Using the thickness of line1 as an example
                }
            }
            return null;
        }
        public void AddParallelLines(Panel panel)
        {
            var parallelLinePairs = FindParallelLinesFromDifferentSections();
           
            foreach (var (line1, line2) in parallelLinePairs)
            {
                var newLine = CreateLineBetween(line1, line2);

                if (newLine != null)
                {
                    ParallelLines.Add(newLine);
                    panel.Controls.Add(newLine);
                }
            }
        }
        public void RemoveAllLines(Panel panel)
        {
            foreach (SectionLine sectionLine in SectionLines)
            {
                panel.Controls.Remove(sectionLine);
            }
            ParallelLines.Clear();
        }

        private Point? getUncoveredTopPoint(TableControl tableControl, List<TableControl> tableControls)
        {
            if (tableControl == null) return null;

            foreach (TableControl tc in tableControls)
            {
                if (tc == tableControl)
                    continue;

                bool isDirectlyAbove = tc.Bottom <= tableControl.Top && tc.Top < tableControl.Top;
                bool leftEdgeIsAboveTable = (tc.Left < tableControl.Right) && (tc.Left > tableControl.Left);

                if (isDirectlyAbove && leftEdgeIsAboveTable)
                {
                   
                    return new Point(tc.LeftLine.StartPoint.X, tableControl.TopLine.StartPoint.Y);
                    

                    // Similarly, you can check for the RightLine if required in the future.
                    //if(tc.RightLine.Start.Y <= tableControl.Top && tc.RightLine.End.Y >= tableControl.Top)
                    //{
                    //    return new Point(tc.RightLine.Start.X, tableControl.Top);
                    //}
                }
            }

            return null;
        }


        // You may add other methods or functionality as per your needs.
    }

}
