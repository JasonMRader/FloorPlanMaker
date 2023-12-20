﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TableGrid
    {
        public List<Table> Tables { get; set; } = new List<Table>();
        public List<Neighbor> Neighbors { get; set; } = new List<Neighbor> { };
        public List<TableEdgeBorders> TableBoarders { get; set; }
        public List<TableNeighborManager> TableNeighborManagers { get; set; } = new List<TableNeighborManager> { };
        public Dictionary<Table, TableEdgeBorders> TableEdges { get; set; } = new Dictionary<Table, TableEdgeBorders>();
        public List<Section> Sections { get; private set; }
        
        public TableGrid(List<Table> tables)
        {
            Tables = tables;
            this.TableBoarders = new List<TableEdgeBorders>();
            foreach (var table in tables)
            {
                TableEdgeBorders edgeBoarders = new TableEdgeBorders(table);
                this.TableBoarders.Add(edgeBoarders);
                TableEdges.TryAdd(table, edgeBoarders);
            }
        }
        public void SetSections(List<Section> sections)
        {
            Sections = sections;
            foreach (var section in Sections)
            {
                foreach (var table in section.Tables)
                {
                    TableEdgeBorders tableBorders = this.TableBoarders.FirstOrDefault(x => x.Table.TableNumber == table.TableNumber);
                    tableBorders.Section = section;
                }
            }
        }
        public void FindTableNeighbors()
        {
            foreach (var currentTable in Tables)
            {
                int currentTableRight = currentTable.Right;
                int currentTableLeft = currentTable.Left;

                TableEdgeBorders currentTableBoarders = TableBoarders.First(tb => tb.Table == currentTable);

                foreach (var otherTable in Tables)
                {
                    if (otherTable == currentTable) continue;

                    int otherTableLeft = otherTable.Left;
                    int otherTableRight = otherTable.Right;
                    //TableEdgeBorders otherTablesBoarders = TableBoarders.First(tb => tb.Table == otherTable);
                    // Check for vertical overlap
                    bool isVerticalOverlap = (currentTable.Bottom >= otherTable.Top && currentTable.Top <= otherTable.Bottom) ||
                                             (otherTable.Bottom >= currentTable.Top && otherTable.Top <= currentTable.Bottom);

                    // Check for a table on the right
                    if (otherTableLeft > currentTableRight)
                    {
                        bool isNoOtherTableInBetween = !Tables.Any(t => t != currentTable && t != otherTable &&
                                                                       t.Left > currentTableRight &&
                                                                       t.Left < otherTableLeft &&
                                                                       ((t.Bottom >= currentTable.Top && t.Top <= currentTable.Bottom) ||
                                                                        (currentTable.Bottom >= t.Top && currentTable.Top <= t.Bottom)));

                        if (isVerticalOverlap && isNoOtherTableInBetween)
                        {
                            //Neighbors.Add(new RightLeftNeighbor(otherTablesBoarders, currentTableBoarders));
                            currentTableBoarders.RightNeighbors.Add(otherTable);
                            if (TableEdges.TryGetValue(otherTable, out TableEdgeBorders otherTableBoarders))
                            {
                                currentTableBoarders.RightNeighborBorders = otherTableBoarders;
                            }
                        }
                    }

                    // Check for a table on the left
                    if (otherTableRight < currentTableLeft)
                    {
                        bool isNoOtherTableInBetween = !Tables.Any(t => t != currentTable && t != otherTable &&
                                                                       t.Right < currentTableLeft &&
                                                                       t.Right > otherTableRight &&
                                                                       ((t.Bottom >= currentTable.Top && t.Top <= currentTable.Bottom) ||
                                                                        (currentTable.Bottom >= t.Top && currentTable.Top <= t.Bottom)));

                        if (isVerticalOverlap && isNoOtherTableInBetween)
                        {
                            currentTableBoarders.LeftNeighbors.Add(otherTable);
                            //Neighbors.Add(new RightLeftNeighbor(currentTableBoarders, otherTablesBoarders));
                            if (TableEdges.TryGetValue(otherTable, out TableEdgeBorders otherTableBoarders))
                            {
                                currentTableBoarders.LeftNeighborBorders = otherTableBoarders;
                            }

                        }
                    }
                }
            }

            // The TableEdgeBoarders instances now have their RightNeighbors and LeftNeighbors lists populated.
        }
        
        public void FindTableTopBottomNeighbors()
        {
            foreach (var currentTable in Tables)
            {
                int currentTableTop = currentTable.Top;
                int currentTableBottom = currentTable.Bottom;

                TableEdgeBorders currentTableBoarders = TableBoarders.First(tb => tb.Table == currentTable);

                foreach (var otherTable in Tables)
                {
                    if (otherTable == currentTable) continue;

                    int otherTableTop = otherTable.Top;
                    int otherTableBottom = otherTable.Bottom;

                    // Check for horizontal overlap
                    bool isHorizontalOverlap = (currentTable.Right >= otherTable.Left && currentTable.Left <= otherTable.Right) ||
                                               (otherTable.Right >= currentTable.Left && otherTable.Left <= currentTable.Right);

                    // Check for a table above
                    if (otherTableBottom < currentTableTop)
                    {
                        bool isNoOtherTableInBetween = !Tables.Any(t => t != currentTable && t != otherTable &&
                                                                       t.Bottom < currentTableTop &&
                                                                       t.Bottom > otherTableBottom &&
                                                                       ((t.Right >= currentTable.Left && t.Left <= currentTable.Right) ||
                                                                        (currentTable.Right >= t.Left && currentTable.Left <= t.Right)));

                        if (isHorizontalOverlap && isNoOtherTableInBetween)
                        {
                            currentTableBoarders.TopNeighbors.Add(otherTable);
                            if (TableEdges.TryGetValue(otherTable, out TableEdgeBorders otherTableBoarders))
                            {
                                currentTableBoarders.TopNeighborBorders = otherTableBoarders;
                            }
                        }
                    }

                    // Check for a table below
                    if (otherTableTop > currentTableBottom)
                    {
                        bool isNoOtherTableInBetween = !Tables.Any(t => t != currentTable && t != otherTable &&
                                                                       t.Top > currentTableBottom &&
                                                                       t.Top < otherTableTop &&
                                                                       ((t.Right >= currentTable.Left && t.Left <= currentTable.Right) ||
                                                                        (currentTable.Right >= t.Left && currentTable.Left <= t.Right)));

                        if (isHorizontalOverlap && isNoOtherTableInBetween)
                        {
                            currentTableBoarders.BottomNeighbors.Add(otherTable);
                            if (TableEdges.TryGetValue(otherTable, out TableEdgeBorders otherTableBoarders))
                            {
                                currentTableBoarders.BottomNeighborBorders = otherTableBoarders;
                            }
                        }
                    }
                }
                
            }
            


        }
        public void SetTableBoarderMidPoints()
        {
            foreach(var table in TableBoarders)
            {
                table.SetBoarders();
                table.SetNodesAndEdges();
            }
        }
        public void CreateNeighbors()
        {
            foreach(var table in TableBoarders)
            {
                if (table.RightNeighborBorders != null)
                {
                    table.AddNeighbor(new RightLeftNeighbor(table.RightNeighborBorders, table));
                    //Neighbors.Add(new RightLeftNeighbor(table.RightNeighborBorders, table));
                }
                if (table.BottomNeighborBorders != null)
                {
                    table.AddNeighbor(new TopBottomNeighbor(table, table.BottomNeighborBorders));
                    //Neighbors.Add(new TopBottomNeighbor(table, table.BottomNeighborBorders));
                }
                if (table.LeftNeighborBorders != null)
                {
                    table.AddNeighbor(new RightLeftNeighbor(table, table.LeftNeighborBorders));
                }
                if(table.TopNeighborBorders != null)
                {
                    table.AddNeighbor(new TopBottomNeighbor(table.TopNeighborBorders, table));
                }
                table.AddTopBottomNeighborsNeighbors();
                //table.AddRightLeftNeighborsNeighbors();
                
            }
        }
        public List<Edge> GetNeighborEdges()
        {
            CreateNeighbors();
            List<Edge> edges = new List<Edge>();
            foreach (var table in TableBoarders)
            {
                foreach (Neighbor neighbor in table.Neighbors)
                {
                    if (neighbor.Edge != null)
                        edges.Add(neighbor.Edge);
                }
            }
           
            return edges;
        }
        public List<Edge> GetSectionTableBoarders()
        {
            List<Edge> result = new List<Edge>();
            foreach (var tableEdgePair in TableEdges)
            {
                Table table = tableEdgePair.Key;
                TableEdgeBorders tableEdgeBoarders = tableEdgePair.Value;

               
                if (tableEdgeBoarders.TopNeighborBorders != null)
                {
                    bool isDifferentSection = tableEdgeBoarders.TopNeighborBorders.Section != tableEdgeBoarders.Section;

                    if (isDifferentSection)
                    {
                        if(tableEdgeBoarders.TopBorder != null)
                        {
                            result.Add(tableEdgeBoarders.TopBorder);
                        }
                       
                    }
                }

               
                if (tableEdgeBoarders.RightNeighborBorders != null)
                {
                    bool isDifferentSection = tableEdgeBoarders.RightNeighborBorders.Section != tableEdgeBoarders.Section;

                    if (isDifferentSection)
                    {
                        if(tableEdgeBoarders.RightBorder != null)
                        {
                            result.Add(tableEdgeBoarders.RightBorder);
                        }
                        
                    }
                }

                //Add Left and Bot also
            }
            return result;
        }

        public List<Edge> GetAllTableBoarders()
        {
            List<Edge> result = new List<Edge>();   
            foreach (var table in TableBoarders) 
            {
                if(table.TopBorder != null)
                {
                    result.Add(table.TopBorder);
                }
                if (table.RightBorder != null)
                {
                    result.Add(table.RightBorder);
                }
                if (table.BottomBorder != null)
                {
                    result.Add(table.BottomBorder);
                }
                if (table.LeftBorder != null)
                {
                    result.Add(table.LeftBorder);
                }

            }
            return result;
        }
        public List<Edge> ModifyBottomNeighbors()
        {
            var result = new List<Edge>();
            foreach (var table in TableBoarders)
            {
                table.AddBottomNeighborsNeighbors();                
                table.AddLeftNeighborsNeighbors();
                result.AddRange(table.GetEdges());
            }
            return result;
        }

    }
}