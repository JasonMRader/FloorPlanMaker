using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SectionNodeManager
    {
        public SectionNodeManager(Section section)
        {
            this.Section = section;
        }
        public Node rootNode { get; set; }
        public Section Section { get; set; }
        public void AssignHierarchyNumbers()
        {
            int currentNumber = 1;
            Node currentNode = rootNode;

            while (currentNode != null)
            {
                currentNode.HierarchyNumber = currentNumber++;
                currentNode = currentNode.Child;
            }
        }


        public List<Node> Nodes { get; set; } = new List<Node>();
        public Node GetTopLeftNode()
        {
            // If Section or its Tables list is null or empty, return null
            if (Section?.Tables == null || !Section.Tables.Any())
                return null;

            // Find the minimum X value among the TopLeft points
            int minX = Section.Tables.Min(t => t.TopLeft.X);

            // Filter tables with the TopLeft X value equal to minX, then order by Y and take the first one
            var topLeft = Section.Tables.Where(t => t.TopLeft.X == minX).OrderBy(t => t.TopLeft.Y).FirstOrDefault().TopLeft;
            Node tlNode = new Node(topLeft.X, topLeft.Y, Section);
            rootNode = tlNode;
            tlNode.description = "Top Left";
            return tlNode;
        }

        public Node GetTopRightNode()
        {
            if (Section?.Tables == null || !Section.Tables.Any())
                return null;

            // Find the smallest Y value among the TopRight points
            int MaxX = Section.Tables.Max(t => t.TopRight.X);

            // Filter tables with the TopRight Y value equal to minY, then order by X descending and take the first one
            var topRight = Section.Tables.Where(t => t.TopRight.X == MaxX).OrderByDescending(t => t.TopRight.Y).FirstOrDefault().TopRight;
            Node trNode = new Node(topRight.X, topRight.Y, Section);
            trNode.description = "Top Right";
            return trNode;
        }
        public Node GetBottomRightNode()
        {
            if (Section?.Tables == null || !Section.Tables.Any())
                return null;

            // Find the largest Y value among the BottomRight points
            int maxY = Section.Tables.Max(t => t.BottomRight.Y);

            // Filter tables with the BottomRight Y value equal to maxY, then order by X descending and take the first one
            var bottomRight = Section.Tables.Where(t => t.BottomRight.Y == maxY).OrderByDescending(t => t.BottomRight.X).FirstOrDefault().BottomRight;

            return new Node(bottomRight.X, bottomRight.Y, Section);
        }
        public Node GetBottomLeftNode()
        {
            if (Section?.Tables == null || !Section.Tables.Any())
                return null;

            // Find the minimum X value among the BottomLeft points
            int minX = Section.Tables.Min(t => t.BottomLeft.X);

            // Find the maximum Y value among the BottomLeft points
            int maxY = Section.Tables.Max(t => t.BottomLeft.Y);

            // Filter tables to find the one with the BottomLeft point at minX and maxY
            var bottomLeft = Section.Tables
                            .Where(t => t.BottomLeft.X == minX && t.BottomLeft.Y == maxY)
                            .Select(t => t.BottomLeft)
                            .FirstOrDefault();

            if (bottomLeft == null)
                return null;

            Node blNode = new Node(bottomLeft.X, bottomLeft.Y, Section);
            blNode.description = "Bottom Left";
            return blNode;
        }


        public bool IsTopUnblocked(Table table)
        {
            if (table == null || Section?.Tables == null) return false;

            foreach (Table otherTable in Section.Tables)
            {
                if (otherTable == table)
                    continue;

                bool isDirectlyAbove = otherTable.BottomRight.Y <= table.TopLeft.Y && otherTable.TopLeft.Y < table.TopLeft.Y;
                bool isHorizontallyOverlapping = (otherTable.TopLeft.X < table.TopRight.X) && (otherTable.TopRight.X > table.TopLeft.X);

                if (isDirectlyAbove && isHorizontallyOverlapping)
                    return false;
            }

            return true;
        }
        public bool IsBottomUnblocked(Table table)
        {
            if (table == null || Section?.Tables == null) return false;

            foreach (Table otherTable in Section.Tables)
            {
                if (otherTable == table)
                    continue;

                // Check if otherTable is directly below table
                bool isDirectlyBelow = otherTable.TopLeft.Y >= table.BottomRight.Y && otherTable.BottomRight.Y > table.BottomRight.Y;

                // Check if otherTable is horizontally overlapping with table
                bool isHorizontallyOverlapping = (otherTable.TopLeft.X < table.TopRight.X) && (otherTable.TopRight.X > table.TopLeft.X);

                if (isDirectlyBelow && isHorizontallyOverlapping)
                    return false;
            }

            return true;
        }


        public bool IsRightUnblocked(Table table)
        {
            if (table == null || Section?.Tables == null) return false;

            foreach (Table otherTable in Section.Tables)
            {
                if (otherTable == table)
                    continue;

                bool isDirectlyToRight = otherTable.TopLeft.X >= table.TopRight.X;
                bool isVerticallyOverlapping = (otherTable.TopLeft.Y < table.BottomRight.Y) && (otherTable.BottomRight.Y > table.TopRight.Y);

                if (isDirectlyToRight && isVerticallyOverlapping)
                    return false;
            }

            return true;
        }
        public void GenerateNodesForUnblockedTops()
        {
            Node topLeftNode = GetTopLeftNode();
            Node topRightNode = GetTopRightNode();
            if (topLeftNode == null)
                return;

            Node currentNode = topLeftNode;

            // Step 2: Process tables with unblocked tops...
            // (Omitted for brevity, same as before)


            // Step 2: For every table...
            var tablesWithUnblockedTops = Section.Tables
                .Where(table => IsTopUnblocked(table))
                .OrderBy(t => t.TopLeft.X)
                .ThenBy(t => t.TopLeft.Y)
                .ToList();

            foreach (var table in tablesWithUnblockedTops)
            {
                Node newTopLeft = new Node(table.TopLeft.X, table.TopLeft.Y, Section);
                Node newTopRight = new Node(table.TopRight.X, table.TopRight.Y, Section);

                // Check for duplicate nodes
                if (!IsNodeExists(newTopLeft))
                {
                    currentNode.InsertNodeAfter(newTopLeft.X, newTopLeft.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }

                // Check for duplicate nodes, especially TopRightNode
                if (!IsNodeExists(newTopRight) && !(newTopRight.X == topRightNode.X && newTopRight.Y == topRightNode.Y))
                {
                    currentNode.InsertNodeAfter(newTopRight.X, newTopRight.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
            }
            // Ensuring the next node after tops is the topRightNode
            if (!currentNode.Equals(topRightNode))
            {
                if (!IsNodeExists(topRightNode))
                {
                    currentNode.InsertNodeAfter(topRightNode.X, topRightNode.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
                else
                {
                    // If topRightNode exists, find it in the list and set it as the current node
                    currentNode = Nodes.FirstOrDefault(node => node.X == topRightNode.X && node.Y == topRightNode.Y);
                }
            }
            AssignHierarchyNumbers();
        }
        public void GenerateNodesForUnblockedBottoms()
        {
            Node bottomLeftNode = GetBottomLeftNode();
            Node bottomRightNode = GetBottomRightNode();
            if (bottomLeftNode == null)
                return;

            Node currentNode = bottomLeftNode;

            // For every table with an unblocked bottom...
            var tablesWithUnblockedBottoms = Section.Tables
                .Where(table => IsBottomUnblocked(table))
                .OrderBy(t => t.BottomLeft.X)
                .ThenByDescending(t => t.BottomLeft.Y)
                .ToList();

            foreach (var table in tablesWithUnblockedBottoms)
            {
                Node newBottomLeft = new Node(table.BottomLeft.X, table.BottomLeft.Y, Section);
                Node newBottomRight = new Node(table.BottomRight.X, table.BottomRight.Y, Section);

                // Check for duplicate nodes
                if (!IsNodeExists(newBottomLeft))
                {
                    currentNode.InsertNodeAfter(newBottomLeft.X, newBottomLeft.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }

                // Check for duplicate nodes, especially BottomRightNode
                if (!IsNodeExists(newBottomRight) && !(newBottomRight.X == bottomRightNode.X && newBottomRight.Y == bottomRightNode.Y))
                {
                    currentNode.InsertNodeAfter(newBottomRight.X, newBottomRight.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
            }
            // Ensuring the next node after bottoms is the bottomRightNode
            if (!currentNode.Equals(bottomRightNode))
            {
                if (!IsNodeExists(bottomRightNode))
                {
                    currentNode.InsertNodeAfter(bottomRightNode.X, bottomRightNode.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
                else
                {
                    // If bottomRightNode exists, find it in the list and set it as the current node
                    currentNode = Nodes.FirstOrDefault(node => node.X == bottomRightNode.X && node.Y == bottomRightNode.Y);
                }
            }
            AssignHierarchyNumbers();
        }

        public void GenerateNodesForUnblockedTables()
        {
            // Step 1: Get main nodes
            Node topLeftNode = GetTopLeftNode();
            Node topRightNode = GetTopRightNode();
            Node bottomRightNode = GetBottomRightNode();

            if (topLeftNode == null)
                return;

            Node currentNode = topLeftNode;

            // Step 2: Process tables with unblocked tops...
            // (Omitted for brevity, same as before)
            

            // Step 2: For every table...
            var tablesWithUnblockedTops = Section.Tables
                .Where(table => IsTopUnblocked(table))
                .OrderBy(t => t.TopLeft.X)
                .ThenBy(t => t.TopLeft.Y)
                .ToList();

            foreach (var table in tablesWithUnblockedTops)
            {
                Node newTopLeft = new Node(table.TopLeft.X, table.TopLeft.Y, Section);
                Node newTopRight = new Node(table.TopRight.X, table.TopRight.Y, Section);

                // Check for duplicate nodes
                if (!IsNodeExists(newTopLeft))
                {
                    currentNode.InsertNodeAfter(newTopLeft.X, newTopLeft.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }

                // Check for duplicate nodes, especially TopRightNode
                if (!IsNodeExists(newTopRight) && !(newTopRight.X == topRightNode.X && newTopRight.Y == topRightNode.Y))
                {
                    currentNode.InsertNodeAfter(newTopRight.X, newTopRight.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
            }
            // Ensuring the next node after tops is the topRightNode
            if (!currentNode.Equals(topRightNode))
            {
                if (!IsNodeExists(topRightNode))
                {
                    currentNode.InsertNodeAfter(topRightNode.X, topRightNode.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
                else
                {
                    // If topRightNode exists, find it in the list and set it as the current node
                    currentNode = Nodes.FirstOrDefault(node => node.X == topRightNode.X && node.Y == topRightNode.Y);
                }
            }

            // Step 3: Process tables with unblocked rights...
            var tablesWithUnblockedRights = Section.Tables
                .Where(table => IsRightUnblocked(table))
                .OrderBy(t => t.TopRight.X)
                .ThenBy(t => t.TopRight.Y)
                .ToList();

            foreach (var table in tablesWithUnblockedRights)
            {
                Node newTopRight = new Node(table.TopRight.X, table.TopRight.Y, Section);
                Node newBottomRight = new Node(table.BottomRight.X, table.BottomRight.Y, Section);

                // Check for duplicate nodes and ensure proper linking for the newTopRight node
                if (!IsNodeExists(newTopRight))
                {
                    // Only add the newTopRight if it's not the current node
                    if (!currentNode.Equals(newTopRight))
                    {
                        currentNode.InsertNodeAfter(newTopRight.X, newTopRight.Y, Section);
                        currentNode = currentNode.Child;
                        Nodes.Add(currentNode);
                    }
                }

                // Check for duplicate nodes and ensure proper linking for the newBottomRight node
                if (!IsNodeExists(newBottomRight))
                {
                    currentNode.InsertNodeAfter(newBottomRight.X, newBottomRight.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
            }

            // Step 4: Link the last node to bottomRightNode if not already there
            if (!currentNode.Equals(bottomRightNode))
            {
                if (!IsNodeExists(bottomRightNode))
                {
                    currentNode.InsertNodeAfter(bottomRightNode.X, bottomRightNode.Y, Section);
                    Nodes.Add(currentNode.Child);
                }
            }
            AssignHierarchyNumbers();
        }


        public void oldGenerateNodesForUnblockedTables()
        {
            // Step 1: Get main nodes
            Node topLeftNode = GetTopLeftNode();
            Node topRightNode = GetTopRightNode();
            Node bottomRightNode = GetBottomRightNode();

            if (topLeftNode == null)
                return;

            

            // Reference to keep track of where to insert the next node
            Node currentNode = topLeftNode;

            // Step 2: For every table...
            var tablesWithUnblockedTops = Section.Tables
                .Where(table => IsTopUnblocked(table))
                .OrderBy(t => t.TopLeft.X)
                .ThenBy(t => t.TopLeft.Y)
                .ToList();

            foreach (var table in tablesWithUnblockedTops)
            {
                Node newTopLeft = new Node(table.TopLeft.X, table.TopLeft.Y, Section);
                Node newTopRight = new Node(table.TopRight.X, table.TopRight.Y, Section);

                // Check for duplicate nodes
                if (!IsNodeExists(newTopLeft))
                {
                    currentNode.InsertNodeAfter(newTopLeft.X, newTopLeft.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }

                // Check for duplicate nodes, especially TopRightNode
                if (!IsNodeExists(newTopRight) && !(newTopRight.X == topRightNode.X && newTopRight.Y == topRightNode.Y))
                {
                    currentNode.InsertNodeAfter(newTopRight.X, newTopRight.Y, Section);
                    currentNode = currentNode.Child;
                    Nodes.Add(currentNode);
                }
            }

            // Step 3: Link the TopRight and BottomRight nodes
            if (currentNode.X != topRightNode.X || currentNode.Y != topRightNode.Y)
            {
                currentNode.InsertNodeAfter(topRightNode.X, topRightNode.Y, Section);
                currentNode = currentNode.Child;
                Nodes.Add(currentNode);
            }
            currentNode.InsertNodeAfter(bottomRightNode.X, bottomRightNode.Y, Section);
            Nodes.Add(currentNode.Child);
        }

        private bool IsNodeExists(Node node)
        {
            return Nodes.Any(n => n.X == node.X && n.Y == node.Y);
        }
        public string testData { get; set; }
        public List<SectionLine> GenerateEdgesAndSectionLinesFromNodes()
        {
            List<Edge> edges = new List<Edge>();

            for (int i = 0; i < Nodes.Count - 1; i++) // Minus 1 to prevent going out of bounds
            {
                Node parent = Nodes[i];
                Node child = Nodes[i + 1];

                // Check if nodes form a diagonal
                if (parent.X != child.X && parent.Y != child.Y)
                {
                    Node intermediate;
                    if (parent.Parent == null) // Parent does not have its own parent
                    {
                        intermediate = new Node(parent.X, child.Y, Section);
                    }
                    else // Move the parent or child
                    {
                        if (child.Y > parent.Y)
                        {
                            intermediate = new Node(parent.X, child.Y, Section);
                        }
                        else
                        {
                            intermediate = new Node(child.X, parent.Y, Section);
                        }
                    }

                    // Add intermediate to the nodes list
                    Nodes.Insert(i + 1, intermediate);

                    // Now, we add two edges: one from parent to intermediate and another from intermediate to child
                    edges.Add(new Edge(parent, intermediate));
                    edges.Add(new Edge(intermediate, child));

                    // Increment the counter to skip the next node since we already processed it
                    i++;
                }
                else
                {
                    edges.Add(new Edge(parent, child));
                }
            }

            // Create SectionLines from edges
            List<SectionLine> sectionLines = new List<SectionLine>();
            foreach (Edge edge in edges)
            {
                SectionLine line = new SectionLine(edge.StartNode, edge.EndNode);
                if (edge.isVertical)
                {
                    line.Edge = SectionLine.BorderEdge.Left;  // or Right, decide based on your logic
                }
                else if (edge.isHorizontal)
                {
                    line.Edge = SectionLine.BorderEdge.Top;  // or Bottom, decide based on your logic
                }
                sectionLines.Add(line);
            }

            return sectionLines;
        }

       

    }
}
