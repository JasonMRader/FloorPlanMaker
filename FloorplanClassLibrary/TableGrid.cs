using System;
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
        public OverriddenTablePairs overriddenPairs = new OverriddenTablePairs();
        public List<TableNeighborManager> TableNeighborManagers { get; set; } = new List<TableNeighborManager> { };
        public Dictionary<string, Neighbor> NeighborMapping { get; private set; } = new Dictionary<string, Neighbor> { };
        public Dictionary<Table, TableEdgeBorders> TableEdges { get; set; } = new Dictionary<Table, TableEdgeBorders>();
        public List<Section> Sections { get; private set; }
        public TableGrid() { }
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

                    string pairKey = overriddenPairs.GetPairKey(currentTable.TableNumber, otherTable.TableNumber);
                    if (overriddenPairs.ignorePairs.Contains(pairKey)) continue;

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
                    string pairKey = overriddenPairs.GetPairKey(currentTable.TableNumber, otherTable.TableNumber);
                    if (overriddenPairs.ignorePairs.Contains(pairKey)) continue;

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
            this.Neighbors.Clear();
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
                foreach(Neighbor neighbor in table.Neighbors)
                {
                    string pairKey = overriddenPairs.GetPairKey(neighbor.table1, neighbor.table2);
                    if (!overriddenPairs.ignorePairs.Contains(pairKey)) 
                    {
                        this.Neighbors.Add(neighbor);
                    }
                   
                }
                //table.AddRightLeftNeighborsNeighbors();
                
            }
        }
        public List<Edge> GetNeighborEdges()
        {
            CreateNeighbors();
            List<Edge> edges = new List<Edge>();
            //foreach (var table in TableBoarders)
            //{
            foreach (Neighbor neighbor in Neighbors)
            {
                    
                if (neighbor.Edge != null)
                    edges.Add(neighbor.Edge);
            }
            //}
           
            return edges;
        }
        public List<Edge> GetNeighborEdgesForOneTable(string TableNumber)
        {
           
            List<Edge> edges = new List<Edge>();
            TableEdgeBorders selectedTable = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber);
           
            foreach (Neighbor neighbor in selectedTable.Neighbors)
            {
                if (neighbor.Edge != null)
                    edges.Add(neighbor.Edge);
            }
            

            return edges;
        }
        public void ManuallyCreateRLNeighbor(string TableNumber1, string TableNumber2)
        {
            TableEdgeBorders TableBorders1 = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber1);
            TableEdgeBorders TableBorders2 = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber2);
            RightLeftNeighbor rlNeighbor = new RightLeftNeighbor(TableBorders1, TableBorders2);
            TableBorders1.AddNeighbor(rlNeighbor);
            TableBorders2.AddNeighbor(rlNeighbor);
            this.Neighbors.Add(rlNeighbor);
        }
        public void ManuallyCreateTBNeighbor(string TableNumber1, string TableNumber2)
        {
            TableEdgeBorders TableBorders1 = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber1);
            TableEdgeBorders TableBorders2 = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber2);
            TopBottomNeighbor tbNeighbor = new TopBottomNeighbor(TableBorders1, TableBorders2);
            TableBorders1.AddNeighbor(tbNeighbor);
            TableBorders2.AddNeighbor(tbNeighbor);
            this.Neighbors.Add(tbNeighbor);
        }
        public List<string> GetNeighborNames(string TableNumber)
        {
            List<string> names = new List<string>();
            TableEdgeBorders selectedTable = this.TableBoarders.FirstOrDefault(t=>t.Table.TableNumber == TableNumber);
            foreach (Neighbor neighbor in selectedTable.Neighbors)
            {
                if(neighbor is TopBottomNeighbor topBottomNeighbor)
                {
                    if(topBottomNeighbor.TopNeighbor.Table.TableNumber == selectedTable.Table.TableNumber)
                    {
                        names.Add(topBottomNeighbor.BottomNeighbor.Table.TableNumber);
                    }
                    else
                    {
                        names.Add(topBottomNeighbor.TopNeighbor.Table.TableNumber);
                    }
                }
                if (neighbor is RightLeftNeighbor rightLeftNeighbor)
                {
                    if (rightLeftNeighbor.RightNeighbor.Table.TableNumber == selectedTable.Table.TableNumber)
                    {
                        names.Add(rightLeftNeighbor.LeftNeighbor.Table.TableNumber);
                    }
                    else
                    {
                        names.Add(rightLeftNeighbor.RightNeighbor.Table.TableNumber);
                    }
                }

            }
            return names;
        }
        public List<TableEdgeBorders> GetNeighborEdges(string TableNumber)
        {
            List<TableEdgeBorders> names = new List<TableEdgeBorders>();
            TableEdgeBorders selectedTable = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber);
            foreach (Neighbor neighbor in selectedTable.Neighbors)
            {
                if (neighbor is TopBottomNeighbor topBottomNeighbor)
                {
                    if (topBottomNeighbor.TopNeighbor.Table.TableNumber == selectedTable.Table.TableNumber)
                    {
                        names.Add(topBottomNeighbor.BottomNeighbor);
                    }
                    else
                    {
                        names.Add(topBottomNeighbor.TopNeighbor);
                    }
                }
                if (neighbor is RightLeftNeighbor rightLeftNeighbor)
                {
                    if (rightLeftNeighbor.RightNeighbor.Table.TableNumber == selectedTable.Table.TableNumber)
                    {
                        names.Add(rightLeftNeighbor.LeftNeighbor);
                    }
                    else
                    {
                        names.Add(rightLeftNeighbor.RightNeighbor);
                    }
                }

            }
            return names;
        }
        public List<Neighbor> GetNeighbors(string TableNumber)
        {
            List<Neighbor> neighbors = new List<Neighbor>();
            TableEdgeBorders selectedTable = this.TableBoarders.FirstOrDefault(t => t.Table.TableNumber == TableNumber);
            
            foreach (Neighbor neighbor in selectedTable.Neighbors)
            {
                string pairKey = overriddenPairs.GetPairKey(neighbor.table1, neighbor.table2);
                if (!overriddenPairs.ignorePairs.Contains(pairKey))
                {
                    neighbors.Add(neighbor);
                }
                

            }
            return neighbors;
        }
        public List<string> GetDisplayableNeighbors(List<Neighbor> neighbors, Table SelectedTable)
        {
            var displayableNeighbors = new List<string>();
            NeighborMapping.Clear();

            foreach (var neighbor in neighbors)
            {
                string neighborString = GetNeighborString(neighbor, SelectedTable);
                if (neighborString != null)
                {
                    displayableNeighbors.Add(neighborString);
                    NeighborMapping[neighborString] = neighbor;
                }
            }

            return displayableNeighbors;
        }

        private string GetNeighborString(Neighbor neighbor, Table SelectedTable)
        {
            // Assuming that ToString() is overridden in Neighbor subclasses to return a unique string
            if (neighbor is TopBottomNeighbor topBottomNeighbor)
            {
                if (topBottomNeighbor.TopNeighbor.Table.TableNumber != SelectedTable.TableNumber)
                {
                    return topBottomNeighbor.TopNeighbor.Table.TableNumber + " (T)";
                }
                else
                {
                    return topBottomNeighbor.BottomNeighbor.Table.TableNumber + " (B)";
                }
            }
            else if (neighbor is RightLeftNeighbor rightLeftNeighbor)
            {
                if (rightLeftNeighbor.RightNeighbor.Table.TableNumber != SelectedTable.TableNumber)
                {
                    return rightLeftNeighbor.RightNeighbor.Table.TableNumber + " (R)";
                }
                else
                {
                    return rightLeftNeighbor.LeftNeighbor.Table.TableNumber + " (L)";
                }
            }
            return null;
        }
        public List<string> GetTestData()
        {
            CreateNeighbors();
            List<string> data = new List<string>();
            foreach (var table in TableBoarders)
            {
                foreach (Neighbor neighbor in table.Neighbors)
                {
                    if(neighbor is TopBottomNeighbor tb)
                    {
                        string s = new string( "(" + tb.TopNeighbor.Table.TableNumber + " | " + tb.BottomNeighbor.Table.TableNumber + ")" + "\n" +
                            tb.TopNeighbor.ToString() +"\n" + tb.BottomNeighbor.ToString() + "\n" + 
                            " Edge " + tb.MidPoint.ToString() +"Y, From: " + tb.StartNode.X.ToString() + " - "+ tb.EndNode.X.ToString() );
                        if(tb.TopNeighbor.Table.TableNumber == "67" || tb.BottomNeighbor.Table.TableNumber == "67")
                        {
                            data.Add(s);
                        }
                        //data.Add(s);
                    }
                    if (neighbor is RightLeftNeighbor lr)
                    {
                        string s = new string("(" + lr.RightNeighbor.Table.TableNumber + " | " + lr.LeftNeighbor.Table.TableNumber + ")" + "\n" +
                            lr.RightNeighbor.ToString() + "\n" + lr.LeftNeighbor.ToString() + "\n" +
                            " Edge " + neighbor.MidPoint.ToString() + "X, From: " + lr.StartNode.Y.ToString() + " - " + lr.EndNode.Y.ToString());
                        if(lr.RightNeighbor.Table.TableNumber == "67" || lr.LeftNeighbor.Table.TableNumber == "67")
                        {
                            data.Add( s);
                        }
                        //data.Add(s);
                    }

                }
            }

            return data;
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

        
        //public List<Edge> ModifyBottomNeighbors()
        //{
        //    var result = new List<Edge>();
        //    foreach (var table in TableBoarders)
        //    {
        //        table.AddBottomNeighborsNeighbors();                
        //        table.AddLeftNeighborsNeighbors();
        //        result.AddRange(table.GetEdges());
        //    }
        //    return result;
        //}

    }
}
