using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            if(endNode.Parent == startNode)
            {
                StartNode = startNode;
                EndNode = endNode;
            }
            else
            {
                StartNode = endNode;
                EndNode = startNode;
            }

           
            this.Section = startNode.Section;
            SetOrientation();
            SetBoarderTypeFromNodes();
            
        }
        public Edge(Node startNode, Node endNode, Boarder boarder)
        {
            StartNode = startNode;
            EndNode = endNode;
            this.Section = startNode.Section;
            SetOrientation();
            this.BoarderType = boarder;

        }
        public static Edge CopyIntruderEdge(Edge intruderEdge)
        {
            Edge edge = new Edge(intruderEdge.StartNode, intruderEdge.EndNode);
            if(intruderEdge.BoarderType == Boarder.Top)
            {
                edge.BoarderType = Boarder.Bottom;
            }
            if (intruderEdge.BoarderType == Boarder.Bottom)
            {
                edge.BoarderType = Boarder.Top;
            }
            if (intruderEdge.BoarderType == Boarder.Right)
            {
                edge.BoarderType = Boarder.Left;
            }
            if (intruderEdge.BoarderType == Boarder.Left)
            {
                edge.BoarderType = Boarder.Left;
            }
            edge.Section = intruderEdge.Section;
            edge.SetOrientation();
            
            return edge;

        }
        public bool VerticalEdgeOverLap(Edge edge)
        {
            bool isOverlapY = (edge.VerticleEdgeStartY() <= this.VerticleEdgeEndY() &&
                                          edge.VerticleEdgeEndY() >= this.VerticleEdgeStartY()) ||
                                         (this.VerticleEdgeStartY() <= edge.VerticleEdgeEndY() &&
                                          this.VerticleEdgeEndY() >= edge.VerticleEdgeStartY());
            return isOverlapY;
        }
        public bool HorizontalEdgeOverLap(Edge edge)
        {
            bool isOverlapX = (edge.HorizontalEdgeStartX() <= this.HorizontalEdgeEndX() &&
                                          edge.HorizontalEdgeEndX() >= this.HorizontalEdgeStartX()) ||
                                         (this.HorizontalEdgeStartX() <= edge.HorizontalEdgeEndX() &&
                                          this.HorizontalEdgeEndX() >= edge.HorizontalEdgeStartX());
            return isOverlapX;
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
        public void SetBoarderTypeFromNodes()
        {
            if(StartNode.isTopNode &&  EndNode.isTopNode)
            {
                this.BoarderType = Boarder.Top;
            }
            if(StartNode.isBottomNode && EndNode.isBottomNode)
            {
                this.BoarderType = Boarder.Bottom;
            }
            if(StartNode.isLeftNode && EndNode.isLeftNode)
            {
                this.BoarderType= Boarder.Left;
            }
            if(StartNode.isRightNode && EndNode.isRightNode)
            {
                this.BoarderType = Boarder.Right;
            }
        }
        public bool isVerticalOverLaping(Edge edgeCompared)
        {
            
            return false;
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
