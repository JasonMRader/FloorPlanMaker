using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace FloorplanClassLibrary
{
    public class EdgeCreator
    {
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<Node> Nodes { get; set; } = new List<Node>();
        //public List<Edge> CreateEdges(List<Node> nodes)
        //{
        //    var edges = new List<Edge>();
        //    for (int i = 0; i < nodes.Count; i++)
        //    {
        //        Node startNode = nodes[i];
        //        Node endNode = nodes[(i + 1) % nodes.Count]; // Wrap around to the first node

        //        // Determine border type based on node positions
        //        Edge.Boarder borderType = (Edge.Boarder)DetermineBorderType(startNode, endNode);
        //        edges.Add(new Edge(startNode, endNode, borderType));

        //        // Set node properties
        //        SetNodeProperties(startNode, endNode, borderType);
        //    }

        //    return edges;
        //}
        public List<Edge> CreateBoundingBoxEdges(List<Node> nodes)
        {
            if (nodes == null || nodes.Count != 4)
                throw new ArgumentException("Exactly four nodes are required to create a bounding box.");

            var edges = new List<Edge>();

            
            edges.Add(new Edge(nodes[0], nodes[1],Edge.Boarder.Top));    
            edges.Add(new Edge(nodes[1], nodes[2], Edge.Boarder.Right));  
            edges.Add(new Edge(nodes[2], nodes[3], Edge.Boarder.Bottom)); 
            edges.Add(new Edge(nodes[3], nodes[0], Edge.Boarder.Left));   

           
            nodes[0].IsTopNode = nodes[0].IsLeftNode = true;
            nodes[1].IsTopNode = nodes[1].IsRightNode = true;
            nodes[2].IsBottomNode = nodes[2].IsRightNode = true;
            nodes[3].IsBottomNode = nodes[3].IsLeftNode = true;

            return edges;
        }
        //public void ModifyEdgeAndAddNodes(List<Node> existingNodes, List<Edge> existingEdges, Edge edgeToModify, List<Point> newPoints)
        //{
        //    if (existingNodes == null || existingEdges == null || edgeToModify == null || newPoints == null)
        //        throw new ArgumentNullException("One or more arguments are null.");

        //    // Check if the edge to modify exists in the existing edges
        //    if (!existingEdges.Contains(edgeToModify))
        //        throw new ArgumentException("The edge to modify does not exist in the existing edges.");

        //    // Sort new points based on their position relative to the edge to modify
        //    newPoints.Sort((p1, p2) => edgeToModify.BorderType == Edge.Boarder.Top || edgeToModify.BorderType == Edge.Boarder.Bottom
        //                                ? p1.X.CompareTo(p2.X) : p1.Y.CompareTo(p2.Y));

        //    // List to store new nodes
        //    List<Node> newNodes = new List<Node>();

        //    // Create new nodes and add to the list
        //    foreach (var point in newPoints)
        //    {
        //        Node newNode = new Node(point.X, point.Y);
        //        existingNodes.Add(newNode);
        //        newNodes.Add(newNode);
        //    }

        //    // Modify the edge and create new edges
        //    Node startNode = edgeToModify.StartNode;
        //    foreach (var newNode in newNodes)
        //    {
        //        // Create a new edge from the start node to the new node
        //        Edge newEdge = new Edge(startNode, newNode, edgeToModify.BorderType);
        //        existingEdges.Add(newEdge);

        //        // Update the start node for the next edge
        //        startNode = newNode;
        //    }

        //    // Update the original edge to start from the last new node
        //    int edgeIndex = existingEdges.IndexOf(edgeToModify);
        //    existingEdges[edgeIndex] = new Edge(startNode, edgeToModify.EndNode, edgeToModify.BorderType);

        //    // Set properties of nodes based on their new edges
        //    foreach (var edge in existingEdges)
        //    {
        //        // Update node properties based on the edge
        //        // Example: edge.StartNode.IsTopNode = edge.BorderType == Edge.Boarder.Top;
        //        // Repeat for other properties and nodes
        //    }
        //}


        //private BorderType DetermineBorderType(Node startNode, Node endNode)
        //{
        //    if()
        //    // Add logic to determine the border type based on node coordinates
        //    // For example, if startNode.X == endNode.X, it's a vertical edge (Right or Left)
        //    // If startNode.Y == endNode.Y, it's a horizontal edge (Top or Bottom)
        //}

        //private void SetNodeProperties(Node startNode, Node endNode, BorderType borderType)
        //{
        //    // Add logic to set IsTopNode, IsRightNode, etc., based on the border type and node positions
        //    // For example, if borderType is Top and startNode is the leftmost of the pair, startNode is a top-left node
        //}


    }
}
