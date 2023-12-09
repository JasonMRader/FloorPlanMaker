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
        public Section Section { get; set; }
        public Boarder BoarderType { get; set; }
        public int VerticleEdgeX()
        {
            if (isHorizontal) return -1;
            else
            {
                return this.StartNode.X;
            }
        }
        public int VerticleEdgeStartY()
        {
            if(isHorizontal) return -1;
            else
            {
                return Math.Min(this.StartNode.Y, this.EndNode.Y);
            }
        }
        public int VerticleEdgeEndY()
        {
            if (isHorizontal) return -1;
            else
            {
                return Math.Max(this.StartNode.Y, this.EndNode.Y);
            }
        }
        public int HorizontalEdgeY()
        {
            if (!isHorizontal) return -1;
            else
            {
                return this.StartNode.Y;
            }
        }
        public int HorizontalEdgeStartX()
        {
            if (!isHorizontal) return -1;
            else
            {
                return Math.Min(this.StartNode.X, this.EndNode.X);
            }
        }
        public int HorizontalEdgeEndX()
        {
            if (isHorizontal) return -1;
            else
            {
                return Math.Max(this.StartNode.X, this.EndNode.X);
            }
        }

        public void SetOrientation()
        {
            if (StartNode.Y == EndNode.Y)
            {
                isHorizontal = true;
                isVertical = false;
            }
            else
            {
                isVertical = true;
                isHorizontal = false;
            }
        }
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
            this.Section = startNode.Section;
            SetOrientation();
            
        }
        public Edge(Node startNode, Node endNode, Boarder boarder)
        {
            StartNode = startNode;
            EndNode = endNode;
            this.Section = startNode.Section;
            SetOrientation();
            this.BoarderType = boarder;

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
        public enum Boarder 
        {
            Top,
            Left,
            Right,
            Bottom,
        }
    }
}
