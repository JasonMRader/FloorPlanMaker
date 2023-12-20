using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TableEdgeBorders
    {
        private Dictionary<string, string> hardcodedTopBottomNeighbors = new Dictionary<string, string>()
        {
            {"42", "41"},
            {"52", "61"},
            {"61", "52"},
            {"441","300" }

        };
        private Dictionary<string, string> hardcodedRightLeftNeighbors = new Dictionary<string, string>()
        {
            

        };

        private HashSet<string> ignorePairs = new HashSet<string>()
        {
            "61-53", 
            "51-63",
            "61-63",
            "51-53",
            "445-300",
            "65-54",
            "434-445",
            "441-418",
            "418-441"



        };
        private string GetPairKey(string tableNumberOne, string tableNumberTwo)
        {
            return tableNumberOne.CompareTo(tableNumberTwo) < 0
                ? tableNumberOne + "-" + tableNumberTwo
                : tableNumberTwo + "-" + tableNumberOne;
        }

        public TableEdgeBorders(Table table)
        {
            this.Table = table;            
            initEdgesAndNodes();
        }
        public Table Table { get; set; }
        public Section Section { get; set; }
        public List<Table> RightNeighbors { get; set; } = new List<Table>();
        public List<Table> LeftNeighbors { get; set; } = new List<Table>();
        public List<Table> TopNeighbors { get; set; } = new List<Table>();
        public List<Table> BottomNeighbors { get; set; } = new List<Table>();
        public List<Neighbor> Neighbors { get; private set; } = new List<Neighbor> ();
        public void AddNeighbor(Neighbor neighbor)
        {
            // Check for null neighbor
            if (neighbor == null) return;

            // Check for valid neighbor based on type and prevent duplicates
            if (neighbor is TopBottomNeighbor topBottomNeighbor)
            {
                if (!IsDuplicateNeighbor(topBottomNeighbor.TopNeighbor, topBottomNeighbor.BottomNeighbor) &&
                    topBottomNeighbor.TopNeighbor.Table.TableNumber != topBottomNeighbor.BottomNeighbor.Table.TableNumber)
                {
                    Neighbors.Add(neighbor);
                }
            }
            else if (neighbor is RightLeftNeighbor rightLeftNeighbor)
            {
                if (!IsDuplicateNeighbor(rightLeftNeighbor.RightNeighbor, rightLeftNeighbor.LeftNeighbor) &&
                    rightLeftNeighbor.RightNeighbor.Table.TableNumber != rightLeftNeighbor.LeftNeighbor.Table.TableNumber)
                {
                    Neighbors.Add(neighbor);
                }
            }
        }

        private bool IsDuplicateNeighbor(TableEdgeBorders neighborOne, TableEdgeBorders neighborTwo)
        {
            return Neighbors.Any(n =>
                (n is TopBottomNeighbor tbNeighbor &&
                    ((tbNeighbor.TopNeighbor == neighborOne && tbNeighbor.BottomNeighbor == neighborTwo) ||
                     (tbNeighbor.BottomNeighbor == neighborOne && tbNeighbor.TopNeighbor == neighborTwo))) ||
                (n is RightLeftNeighbor rlNeighbor &&
                    ((rlNeighbor.RightNeighbor == neighborOne && rlNeighbor.LeftNeighbor == neighborTwo) ||
                     (rlNeighbor.LeftNeighbor == neighborOne && rlNeighbor.RightNeighbor == neighborTwo))));
        }

        
       
        public bool OverlapsVertically(TableEdgeBorders otherTableBorders)
        {
            bool isVerticalOverlap = (this.TopBorderY <= otherTableBorders.BottomBorderY && this.BottomBorderY >= otherTableBorders.TopBorderY) ||
                                     (otherTableBorders.TopBorderY <= this.BottomBorderY && otherTableBorders.BottomBorderY >= this.TopBorderY);
            return isVerticalOverlap;
        }
        public bool OverlapsHorizontally(TableEdgeBorders otherTableBorders)
        {
            bool isHorizontalOverlap = (this.RightBorderX >= otherTableBorders.LeftBorderX && this.LeftBorderX <= otherTableBorders.RightBorderX) ||
                                              (otherTableBorders.RightBorderX >= this.LeftBorderX && otherTableBorders.LeftBorderX <= this.RightBorderX);
            return isHorizontalOverlap;
        }
        public void AddTopBottomNeighborsNeighbors()
        {
            List<Neighbor> newNeighbors = new List<Neighbor>();
            foreach (Neighbor neighbor in Neighbors)
            {
                

                if (neighbor is TopBottomNeighbor topBottomNeighbor)
                {
                   

                    // Process for the top neighbor
                    if (topBottomNeighbor.TopNeighbor.Table.TableNumber == this.Table.TableNumber)
                    {
                        ProcessTopNeighbor(topBottomNeighbor, newNeighbors);
                    }
                    // Process for the bottom neighbor
                    if (topBottomNeighbor.BottomNeighbor.Table.TableNumber == this.Table.TableNumber)
                    {
                        ProcessBottomNeighbor(topBottomNeighbor, newNeighbors);
                    }
                    AddHardcodedNeighbors(newNeighbors);
                }
            }

            // Add the new neighbors
            foreach (var newNeighbor in newNeighbors)
            {
                AddNeighbor(newNeighbor);
                if (newNeighbor is TopBottomNeighbor tbNeighbor)
                {
                    tbNeighbor.TopNeighbor.AddNeighbor(tbNeighbor);
                    tbNeighbor.BottomNeighbor.AddNeighbor(tbNeighbor);
                }
            }
        }

        private void ProcessTopNeighbor(TopBottomNeighbor topBottomNeighbor, List<Neighbor> newNeighbors)
        {
            foreach (Neighbor rightLeftNeighbor in topBottomNeighbor.BottomNeighbor.Neighbors)
            {
                if (rightLeftNeighbor is RightLeftNeighbor rlNeighbor)
                {
                    if (IsSuitableForTopBottomNeighbor(topBottomNeighbor, rlNeighbor))
                    {
                        string pairKey = GetPairKey(this.Table.TableNumber, rlNeighbor.RightNeighbor.Table.TableNumber);

                        if (ignorePairs.Contains(pairKey))
                        {
                            continue; // Skip this iteration if the pair should be ignored
                        }
                        TopBottomNeighbor newNeighbor = new TopBottomNeighbor(this, rlNeighbor.RightNeighbor);
                        newNeighbors.Add(newNeighbor);
                    }
                }
            }
        }

        private void ProcessBottomNeighbor(TopBottomNeighbor topBottomNeighbor, List<Neighbor> newNeighbors)
        {
            foreach (Neighbor rightLeftNeighbor in topBottomNeighbor.TopNeighbor.Neighbors)
            {
                if (rightLeftNeighbor is RightLeftNeighbor rlNeighbor)
                {
                    if (IsSuitableForTopBottomNeighbor(topBottomNeighbor, rlNeighbor))
                    {
                        string pairKey = GetPairKey(this.Table.TableNumber, rlNeighbor.RightNeighbor.Table.TableNumber);

                        if (ignorePairs.Contains(pairKey))
                        {
                            continue; // Skip this iteration if the pair should be ignored
                        }
                        TopBottomNeighbor newNeighbor = new TopBottomNeighbor(rlNeighbor.RightNeighbor, this);
                        newNeighbors.Add(newNeighbor);
                    }
                }
            }
        }


        private bool IsSuitableForTopBottomNeighbor(TopBottomNeighbor topBottomNeighbor, RightLeftNeighbor rlNeighbor)
        {
            // Check if rlNeighbor is between top and bottom neighbors vertically
            bool isBetweenVertically = rlNeighbor.LeftNeighbor.Table.Top < topBottomNeighbor.TopNeighbor.Table.Bottom &&
                                       rlNeighbor.RightNeighbor.Table.Bottom > topBottomNeighbor.BottomNeighbor.Table.Top;

            return OverlapsHorizontally(rlNeighbor.RightNeighbor) && !isBetweenVertically;
        }
        private void AddHardcodedNeighbors(List<Neighbor> newNeighbors)
        {
            foreach (var pair in hardcodedTopBottomNeighbors)
            {
                if (this.Table.TableNumber == pair.Key)
                {
                    TableEdgeBorders hardcodedBottomNeighbor = FindTableByNumber(pair.Value);
                    if (hardcodedBottomNeighbor != null)
                    {
                        TopBottomNeighbor newNeighbor = new TopBottomNeighbor(this, hardcodedBottomNeighbor);
                        newNeighbors.Add(newNeighbor);
                    }
                }
            }
        }
        private TableEdgeBorders FindTableByNumber(string tableNumber)
        {
            HashSet<string> visitedTables = new HashSet<string>();
            return FindTableByNumberRecursive(tableNumber, this, visitedTables);
        }

        private TableEdgeBorders FindTableByNumberRecursive(string tableNumber, TableEdgeBorders currentTable, HashSet<string> visitedTables)
        {
            // Check if the current table is the one we're looking for
            if (currentTable.Table.TableNumber == tableNumber)
            {
                return currentTable;
            }

            // Mark the current table as visited
            visitedTables.Add(currentTable.Table.TableNumber);

            // Iterate through each neighbor
            foreach (Neighbor neighbor in currentTable.Neighbors)
            {
                TableEdgeBorders nextTable = null;

                // Determine the next table to visit based on the type of neighbor
                if (neighbor is TopBottomNeighbor topBottomNeighbor)
                {
                    nextTable = topBottomNeighbor.TopNeighbor.Table.TableNumber == currentTable.Table.TableNumber
                                ? topBottomNeighbor.BottomNeighbor
                                : topBottomNeighbor.TopNeighbor;
                }
                else if (neighbor is RightLeftNeighbor rightLeftNeighbor)
                {
                    nextTable = rightLeftNeighbor.RightNeighbor.Table.TableNumber == currentTable.Table.TableNumber
                                ? rightLeftNeighbor.LeftNeighbor
                                : rightLeftNeighbor.RightNeighbor;
                }

                // Check if we've already visited this table
                if (nextTable != null && !visitedTables.Contains(nextTable.Table.TableNumber))
                {
                    // Recursively search from the next table
                    TableEdgeBorders foundTable = FindTableByNumberRecursive(tableNumber, nextTable, visitedTables);
                    if (foundTable != null)
                    {
                        return foundTable; // Found the table
                    }
                }
            }

            return null; // Table not found in the current path
        }


        public void AddRightLeftNeighborsNeighbors()
        {
            List<Neighbor> newNeighbors = new List<Neighbor>();
            foreach (Neighbor neighbor in Neighbors)
            {
                if (neighbor is RightLeftNeighbor rightLeftNeighbor)
                {
                    // Process for the right neighbor
                    if (rightLeftNeighbor.RightNeighbor.Table.TableNumber == this.Table.TableNumber)
                    {
                        ProcessRightNeighbor(rightLeftNeighbor, newNeighbors);
                    }
                    // Process for the left neighbor
                    if (rightLeftNeighbor.LeftNeighbor.Table.TableNumber == this.Table.TableNumber)
                    {
                        ProcessLeftNeighbor(rightLeftNeighbor, newNeighbors);
                    }
                }
            }

            AddHardcodedRightLeftNeighbors(newNeighbors);

            // Add the new neighbors
            foreach (var newNeighbor in newNeighbors)
            {
                AddNeighbor(newNeighbor);
                if (newNeighbor is RightLeftNeighbor rlNeighbor)
                {
                    rlNeighbor.RightNeighbor.AddNeighbor(rlNeighbor);
                    rlNeighbor.LeftNeighbor.AddNeighbor(rlNeighbor);
                }
            }
        }
        private bool IsSuitableForRightLeftNeighbor(TopBottomNeighbor tbNeighbor, RightLeftNeighbor rlNeighbor)
        {
            // Check if rlNeighbor is between top and bottom neighbors horizontally
            bool isBetweenHorizontally = rlNeighbor.LeftNeighbor.Table.Left < tbNeighbor.TopNeighbor.Table.Right &&
                                         rlNeighbor.RightNeighbor.Table.Right > tbNeighbor.BottomNeighbor.Table.Left;

            return OverlapsVertically(rlNeighbor.RightNeighbor) && !isBetweenHorizontally;
        }

        private void ProcessRightNeighbor(RightLeftNeighbor rightLeftNeighbor, List<Neighbor> newNeighbors)
        {
            foreach (Neighbor topBottomNeighbor in rightLeftNeighbor.LeftNeighbor.Neighbors)
            {
                if (topBottomNeighbor is TopBottomNeighbor tbNeighbor && IsSuitableForRightLeftNeighbor(tbNeighbor, rightLeftNeighbor))
                {
                    string pairKeyTop = GetPairKey(this.Table.TableNumber, tbNeighbor.TopNeighbor.Table.TableNumber);
                    string pairKeyBottom = GetPairKey(this.Table.TableNumber, tbNeighbor.BottomNeighbor.Table.TableNumber);

                    if (!ignorePairs.Contains(pairKeyTop))
                    {
                        RightLeftNeighbor newNeighbor = new RightLeftNeighbor(this, tbNeighbor.TopNeighbor);
                        newNeighbors.Add(newNeighbor);
                    }

                    if (!ignorePairs.Contains(pairKeyBottom))
                    {
                        RightLeftNeighbor newNeighbor = new RightLeftNeighbor(this, tbNeighbor.BottomNeighbor);
                        newNeighbors.Add(newNeighbor);
                    }
                }
            }
        }



        private void ProcessLeftNeighbor(RightLeftNeighbor rightLeftNeighbor, List<Neighbor> newNeighbors)
        {
            foreach (Neighbor topBottomNeighbor in rightLeftNeighbor.RightNeighbor.Neighbors)
            {
                if (topBottomNeighbor is TopBottomNeighbor tbNeighbor && IsSuitableForRightLeftNeighbor(tbNeighbor, rightLeftNeighbor))
                {
                    string pairKeyTop = GetPairKey(this.Table.TableNumber, tbNeighbor.TopNeighbor.Table.TableNumber);
                    string pairKeyBottom = GetPairKey(this.Table.TableNumber, tbNeighbor.BottomNeighbor.Table.TableNumber);

                    if (!ignorePairs.Contains(pairKeyTop))
                    {
                        RightLeftNeighbor newNeighbor = new RightLeftNeighbor(tbNeighbor.TopNeighbor, this);
                        newNeighbors.Add(newNeighbor);
                    }

                    if (!ignorePairs.Contains(pairKeyBottom))
                    {
                        RightLeftNeighbor newNeighbor = new RightLeftNeighbor(tbNeighbor.BottomNeighbor, this);
                        newNeighbors.Add(newNeighbor);
                    }
                }
            }
        }


        private void AddHardcodedRightLeftNeighbors(List<Neighbor> newNeighbors)
        {
            foreach (var pair in hardcodedRightLeftNeighbors)
            {
                if (this.Table.TableNumber == pair.Key)
                {
                    TableEdgeBorders hardcodedRightLeftNeighbor = FindTableByNumber(pair.Value);
                    if (hardcodedRightLeftNeighbor != null)
                    {
                        RightLeftNeighbor newNeighbor = new RightLeftNeighbor(this, hardcodedRightLeftNeighbor);
                        newNeighbors.Add(newNeighbor);
                    }
                }
            }
        }

        // Existing methods: FindTableByNumber, FindTableByNumberRecursive, etc.

       

       

        public List<Edge> GetEdges()
        {
            List<Edge> result = new List<Edge>();
            foreach (Edge edge in this.NeighborBorders.Values)
            {
                if(edge != null)
                {
                    result.Add(edge);
                }
               
            }
            return result;
        }
        public TableEdgeBorders TopNeighborBorders { get; set; }
        public TableEdgeBorders RightNeighborBorders { get; set; }
        public TableEdgeBorders LeftNeighborBorders { get; set; }
        public TableEdgeBorders BottomNeighborBorders { get; set; } 
        public Dictionary<TableEdgeBorders, Edge> NeighborBorders { get; set; } = new Dictionary<TableEdgeBorders, Edge>();
        public int TopBorderY { get; private set; } = -1;
        public int BottomBorderY { get; private set; } = -1;
        public int RightBorderX { get; private set; } = -1;
        public int LeftBorderX { get; private set; } = -1;
        public Node TopLeftNode { get; private set; }
        public Node TopRightNode { get; private set; }
        public Node BottomRightNode { get; private set; }
        public Node BottomLeftNode { get; private set; }
        public Edge TopBorder { get; private set; }
        public Edge RightBorder { get; private set; }
        public Edge BottomBorder { get; private set; }
        public Edge LeftBorder { get; private set; }
        public Edge RightSide { get; set; } 
        public Edge LeftSide { get; set; }
        public Edge TopSide { get; set; }
        public Edge BottomSide { get; set;}
        public Node TopLeft { get; set; }
        public Node TopRight { get; set; }
        public Node BottomRight { get; set; }
        public Node BottomLeft { get; set;}
        private void initEdgesAndNodes()
        {
            this.TopLeft = new Node(Table.TopLeft.X, Table.TopLeft.Y);
            this.TopRight = new Node(Table.TopRight.X, Table.TopRight.Y);
            this.BottomRight = new Node(Table.BottomRight.X, Table.BottomRight.Y);
            this.BottomLeft = new Node(Table.BottomLeft.X, Table.BottomLeft.Y);
            TopLeft.Parent = BottomLeft;
            TopLeft.Child = TopRight;
            TopRight.Parent = TopLeft;
            TopRight.Child = BottomRight;
            BottomRight.Parent = TopRight;
            BottomRight.Child = BottomLeft;
            BottomLeft.Parent = BottomRight;
            BottomLeft.Child = TopLeft;
            TopSide = new Edge(TopLeft, TopRight, Edge.Boarder.Top);
            RightSide = new Edge(TopRight, BottomRight, Edge.Boarder.Right);
            BottomSide = new Edge(BottomRight, BottomLeft, Edge.Boarder.Bottom);
            LeftSide = new Edge(BottomLeft, TopLeft, Edge.Boarder.Left);

        }
        public void SetBoarders()
        {

            if (RightNeighbors.Count > 0)
            {
                RightBorderX = (this.Table.Right + RightNeighbors[0].Left) / 2;
            }
            else
            {
                RightBorderX = this.Table.Right + 5;
            }
            if(LeftNeighbors.Count > 0)
            {
                LeftBorderX = (this.Table.Left + LeftNeighbors[0].Right)/2;
            }
            else
            {
                LeftBorderX= this.Table.Left + -5;
            }
            if (TopNeighbors.Count > 0)
            {
                TopBorderY = (this.Table.Top + TopNeighbors[0].Bottom)/2;
            }
            else
            {
                TopBorderY = this.Table.Top - 5;
            }
            if(BottomNeighbors.Count > 0)
            {
                BottomBorderY = (this.Table.Bottom + BottomNeighbors[0].Top)/2;    
            }
            else
            {
                BottomBorderY = this.Table.Bottom + 5;
            }
            
        }
        public void SetNodesAndEdges()
        {
            if (LeftBorderX > 0 && TopBorderY > 0)
            {
                TopLeftNode = new Node(LeftBorderX, TopBorderY);
            }
            if (TopBorderY > 0 && RightBorderX > 0)
            {
                TopRightNode = new Node(RightBorderX, TopBorderY);
            }
            if(BottomBorderY > 0 && RightBorderX > 0)
            {
                BottomRightNode = new Node(RightBorderX, BottomBorderY);
            }
            if(BottomBorderY > 0 &&  LeftBorderX > 0)
            {
                BottomLeftNode = new Node(LeftBorderX, BottomBorderY);
            }
            //Methods to replace missing node
            if (TopLeftNode != null && TopRightNode == null)
            {
                TopRightNode = new Node(this.Table.Right + 5, TopLeftNode.Y);
                
            }
            if (TopLeftNode == null && TopRightNode != null)
            {
               TopLeftNode = new Node(this.Table.Left - 5, TopRightNode.Y);
               
            }
            if (TopRightNode == null && BottomRightNode != null)
            {
                TopRightNode = new Node(BottomRightNode.X, this.Table.Top - 5);
            }
            if(BottomRightNode == null && TopRightNode != null && BottomLeftNode == null)
            {
                BottomRightNode = new Node(TopRightNode.X, this.Table.Bottom +5);
            }
            if (BottomRightNode == null && TopRightNode != null && BottomLeftNode != null)
            {
                BottomRightNode = new Node(TopRightNode.X, BottomLeftNode.Y);
            }
            GetBorderEdges();
            
        }
        private void GetBorderEdges()
        {
            if (TopLeftNode != null && TopRightNode != null)
            {
                TopBorder = new Edge(TopLeftNode, TopRightNode);
            }
            if (TopRightNode != null && BottomRightNode != null)
            {
                RightBorder = new Edge(TopRightNode, BottomRightNode);
            }
            if (BottomRightNode != null && BottomLeftNode != null)
            {
                BottomBorder = new Edge(BottomRightNode, BottomLeftNode);
            }
            if (BottomLeftNode != null && TopLeftNode != null)
            {
                LeftBorder = new Edge(BottomLeftNode, TopLeftNode);
            }

           
           
        }
        public override string ToString()
        {
            return Table.ToString() + "\n" + " R " + RightBorderX.ToString() + " L " + LeftBorderX.ToString()
                + " T " + TopBorderY.ToString() + " B " + BottomBorderY.ToString();
        }
        //public void AddRightLeftNeighborsNeighbors()
        //{
        //    List<Neighbor> newNeighbors = new List<Neighbor>();
        //    foreach (Neighbor neighbor in Neighbors)
        //    {
        //        if (neighbor is RightLeftNeighbor rightLeftNeighbor)
        //        {
        //            if (rightLeftNeighbor.RightNeighbor.Table.TableNumber == this.Table.TableNumber)
        //            {
        //                foreach (Neighbor topBottomNeighbor in rightLeftNeighbor.LeftNeighbor.Neighbors)
        //                {
        //                    if (topBottomNeighbor is TopBottomNeighbor tbNeighbor)
        //                    {
        //                        // Check for vertical border overlap with the current table
        //                        if (OverlapsVertically(tbNeighbor.TopNeighbor))
        //                        {
        //                            if(tbNeighbor.TopNeighbor.Table.TableNumber != this.Table.TableNumber)
        //                            {
        //                                RightLeftNeighbor newNeighbor = new RightLeftNeighbor(this, tbNeighbor.TopNeighbor);
        //                                newNeighbors.Add(newNeighbor);
        //                            }

        //                        }
        //                        if (OverlapsVertically(tbNeighbor.BottomNeighbor))
        //                        {

        //                            RightLeftNeighbor newNeighbor = new RightLeftNeighbor(this, tbNeighbor.BottomNeighbor);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                    }
        //                }
        //            }
        //            if (rightLeftNeighbor.LeftNeighbor.Table.TableNumber == this.Table.TableNumber)
        //            {
        //                foreach (Neighbor topBottomNeighbor in rightLeftNeighbor.RightNeighbor.Neighbors)
        //                {
        //                    if (topBottomNeighbor is TopBottomNeighbor tbNeighbor)
        //                    {
        //                        if (OverlapsVertically(tbNeighbor.TopNeighbor))
        //                        {
        //                            RightLeftNeighbor newNeighbor = new RightLeftNeighbor( tbNeighbor.TopNeighbor, this);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                        if (OverlapsVertically(tbNeighbor.BottomNeighbor))
        //                        {
        //                            RightLeftNeighbor newNeighbor = new RightLeftNeighbor(tbNeighbor.BottomNeighbor, this);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    foreach (var newNeighbor in newNeighbors)
        //    {
        //        AddNeighbor(newNeighbor);
        //        if (newNeighbor is RightLeftNeighbor rightLeftNeighbor)
        //        {
        //            rightLeftNeighbor.RightNeighbor.AddNeighbor(rightLeftNeighbor);
        //            rightLeftNeighbor.LeftNeighbor.AddNeighbor(rightLeftNeighbor);
        //        }
        //    }

        //}
        //public void AddBottomNeighborsNeighbors()
        //{

        //    if (this.BottomNeighborBorders != null)
        //    {
        //        this.NeighborBorders.TryAdd(BottomNeighborBorders, BottomNeighborBorders.TopBorder);
        //        if (BottomNeighborBorders.RightNeighbors.Count > 0)
        //        {
        //            if (BottomNeighborBorders.RightBorderX < this.RightBorderX)
        //            {
        //                this.BottomNeighbors.Add(BottomNeighborBorders.RightNeighbors[0]);
        //                //this.NeighborBorders.Add(BottomNeighborBorders, BottomNeighborBorders.TopBorder);
        //                this.NeighborBorders.TryAdd(BottomNeighborBorders.RightNeighborBorders, BottomNeighborBorders.RightNeighborBorders.TopBorder);
        //            }
        //        }
        //        else if (BottomNeighborBorders.LeftNeighbors.Count > 0)
        //        {
        //            if (BottomNeighborBorders.LeftBorderX > this.LeftBorderX)
        //            {
        //                this.BottomNeighbors.Add(BottomNeighborBorders.LeftNeighbors[0]);
        //                //this.NeighborBorders.Add(BottomNeighborBorders, BottomNeighborBorders.TopBorder);
        //                this.NeighborBorders.TryAdd(BottomNeighborBorders.LeftNeighborBorders, BottomNeighborBorders.LeftNeighborBorders.TopBorder);
        //            }
        //        }

        //    }
        //}
        //public void AddLeftNeighborsNeighbors()
        //{
        //    if (this.LeftNeighborBorders != null)
        //    {
        //        this.NeighborBorders.TryAdd(LeftNeighborBorders, LeftNeighborBorders.RightBorder);
        //        if (LeftNeighborBorders.TopNeighbors.Count > 0)
        //        {
        //            if (LeftNeighborBorders.TopBorderY > this.TopBorderY)
        //            {
        //                this.LeftNeighbors.Add(LeftNeighborBorders.TopNeighbors[0]);
        //                this.NeighborBorders.TryAdd(LeftNeighborBorders.TopNeighborBorders, LeftNeighborBorders.TopNeighborBorders.RightBorder);
        //            }
        //        }
        //        else if (LeftNeighborBorders.BottomNeighbors.Count > 0)
        //        {
        //            if (LeftNeighborBorders.BottomBorderY < this.BottomBorderY)
        //            {
        //                this.LeftNeighbors.Add(LeftNeighborBorders.BottomNeighbors[0]);
        //                this.NeighborBorders.TryAdd(LeftNeighborBorders.BottomNeighborBorders, LeftNeighborBorders.BottomNeighborBorders.RightBorder);
        //            }
        //        }
        //    }
        //}
        //private bool IsNotUnder(TableEdgeBorders currentNeighbor, TableEdgeBorders potentialNeighbor)
        //{
        //   // return true;
        //    return currentNeighbor.Table.Bottom > potentialNeighbor.Table.Top;
        //}

        //private bool IsNotAbove(TableEdgeBorders currentNeighbor, TableEdgeBorders potentialNeighbor)
        //{

        //    //return true;
        //    return potentialNeighbor.Table.Top < currentNeighbor.Table.Bottom;
        //}

        //public void oldAddTopBottomNeighborsNeighbors()
        //{
        //    List<Neighbor> newNeighbors = new List<Neighbor>();
        //    foreach (Neighbor neighbor in Neighbors)
        //    {
        //        if(neighbor is TopBottomNeighbor topBottomNeighbor)
        //        {
        //            if(topBottomNeighbor.TopNeighbor.Table.TableNumber == this.Table.TableNumber)
        //            {
        //                foreach (Neighbor rightLeftNeighbor in topBottomNeighbor.BottomNeighbor.Neighbors)
        //                {
        //                    if (rightLeftNeighbor is RightLeftNeighbor rlNeighbor)
        //                    {

        //                        // Check for border overlap with the current table
        //                        if (OverLapsHorizontally(rlNeighbor.RightNeighbor) && IsNotUnder(topBottomNeighbor.BottomNeighbor, rlNeighbor.RightNeighbor))
        //                        {
        //                            TopBottomNeighbor newNeighbor = new TopBottomNeighbor(this, rlNeighbor.RightNeighbor);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                        if (OverLapsHorizontally(rlNeighbor.LeftNeighbor) && IsNotUnder(topBottomNeighbor.BottomNeighbor, rlNeighbor.LeftNeighbor))
        //                        {
        //                            TopBottomNeighbor newNeighbor = new TopBottomNeighbor(this, rlNeighbor.LeftNeighbor);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                    }
        //                }
        //            }
        //            if (topBottomNeighbor.BottomNeighbor.Table.TableNumber == this.Table.TableNumber)
        //            {
        //                foreach (Neighbor rightLeftNeighbor in topBottomNeighbor.TopNeighbor.Neighbors)
        //                {
        //                    if (rightLeftNeighbor is RightLeftNeighbor rlNeighbor)
        //                    {
        //                        if (OverLapsHorizontally(rlNeighbor.RightNeighbor) && IsNotAbove(topBottomNeighbor.TopNeighbor, rlNeighbor.RightNeighbor))
        //                        {
        //                            TopBottomNeighbor newNeighbor = new TopBottomNeighbor(rlNeighbor.RightNeighbor, this);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                        if (OverLapsHorizontally(rlNeighbor.LeftNeighbor) && IsNotAbove(topBottomNeighbor.TopNeighbor, rlNeighbor.LeftNeighbor))
        //                        {
        //                            TopBottomNeighbor newNeighbor = new TopBottomNeighbor(rlNeighbor.LeftNeighbor, this);
        //                            newNeighbors.Add(newNeighbor);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    foreach (var newNeighbor in newNeighbors)
        //    {
        //        AddNeighbor(newNeighbor);
        //        if (newNeighbor is TopBottomNeighbor tbNeighbor)
        //        {
        //            tbNeighbor.TopNeighbor.AddNeighbor(tbNeighbor);
        //            tbNeighbor.BottomNeighbor.AddNeighbor(tbNeighbor);
        //        }
        //    }
        //}
    }
}
