using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class Edge
    {
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }
        public bool isVertical { get; set; }
        public bool isHorizontal { get; set; }
        public Point startPoint()
        {
            Point start = new Point(StartNode.X, StartNode.Y);
            return start;
        }
        public Point endPoint()
        {
            return new Point(EndNode.X, EndNode.Y);
        }
        public Edge(Node startNode, Node endNode)
        {
            StartNode = startNode;
            EndNode = endNode;
        }

        public void MoveVerticalEdge(int newX)
        {
           
            StartNode.X = newX;
            EndNode.X = newX;
        }
        public override string ToString()
        {
            return "Start Node: " + StartNode.ToString() + ", End Node: " + EndNode.ToString();
        }
    }
}
