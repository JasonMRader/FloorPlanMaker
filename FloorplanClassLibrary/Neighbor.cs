using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public abstract class Neighbor
    {
       
        public int MidPoint { get; set; }
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }   
        public Edge Edge { get; set; }
       
        public int Start { get; protected set; }
        public int End { get; protected set; }

        public abstract Neighbor GetNeighborsOfNeighbors();
        protected abstract void GetStartNode();
        protected abstract void GetEndNode();
        public string table1 { get; set; }
        public string table2 { get; set; }


    }
    public class TopBottomNeighbor : Neighbor
    {
        public TopBottomNeighbor(TableEdgeBorders topNeighbor, TableEdgeBorders bottomNeighbor)
        {
            TopNeighbor = topNeighbor;
            BottomNeighbor = bottomNeighbor;           
            MidPoint = (TopNeighbor.Table.Bottom + BottomNeighbor.Table.Top) / 2;
            GetStartNode();
            GetEndNode();
            //EndNode = new Node(Math.Min(TopNeighbor.RightBorderX, BottomNeighbor.RightBorderX), MidPoint);
            Edge = new Edge(StartNode, EndNode);
            table1 = TopNeighbor.Table.TableNumber;
            table2 = BottomNeighbor.Table.TableNumber;
            //TopNeighbor.Neighbors.Add(this);
            //BottomNeighbor.Neighbors.Add(this);
        }
        public TopBottomNeighbor(int midLocation, int startPoint, int endPoint, TableEdgeBorders topBorder, TableEdgeBorders bottomBorder)
        {
            TopNeighbor = topBorder;
            BottomNeighbor = bottomBorder;
            MidPoint = midLocation;
            StartNode = new Node(MidPoint, startPoint);
            EndNode = new Node(MidPoint, endPoint);
            Edge = new Edge(StartNode, EndNode);
            table1 = TopNeighbor.Table.TableNumber;
            table2 = BottomNeighbor.Table.TableNumber;

        }
        public TableEdgeBorders TopNeighbor { get; set; }
        public TableEdgeBorders BottomNeighbor { get; set; }

        public override Neighbor GetNeighborsOfNeighbors()
        {
            throw new NotImplementedException();

        }
        protected override void GetStartNode()
        {
            if(TopNeighbor.LeftBorderX != -1 && BottomNeighbor.LeftBorderX != -1)
            {
                StartNode = new Node(Math.Max(TopNeighbor.LeftBorderX, BottomNeighbor.LeftBorderX), MidPoint);
            }
            else if(TopNeighbor.LeftBorderX != -1)
            {
                StartNode = new Node(TopNeighbor.LeftBorderX, MidPoint);
            }
            else if (BottomNeighbor.LeftBorderX != -1)
            {
                StartNode = new Node(BottomNeighbor.LeftBorderX, MidPoint);
            }
            else
            {
                StartNode = new Node(Math.Max(TopNeighbor.Table.Left, BottomNeighbor.Table.Left), MidPoint);
            }
            Start = StartNode.X;

        }
        protected override void GetEndNode()
        {
            if (TopNeighbor.RightBorderX != -1 && BottomNeighbor.RightBorderX != -1)
            {
                EndNode = new Node(Math.Min(TopNeighbor.RightBorderX, BottomNeighbor.RightBorderX), MidPoint);
            }
            else if (TopNeighbor.RightBorderX != -1)
            {
                EndNode = new Node(TopNeighbor.RightBorderX, MidPoint);
            }
            else if (BottomNeighbor.RightBorderX != -1)
            {
                EndNode = new Node(BottomNeighbor.RightBorderX, MidPoint);
            }
            else
            {
                EndNode = new Node(Math.Min(TopNeighbor.Table.Right, BottomNeighbor.Table.Right), MidPoint);
            }
            End = EndNode.X;
            
        }
        public override string ToString()
        {
            return "T: " + TopNeighbor.Table.TableNumber + ", B: " + BottomNeighbor.Table.TableNumber;
        }
    }
    public class RightLeftNeighbor : Neighbor
    {
        public RightLeftNeighbor(TableEdgeBorders rightNeighbor, TableEdgeBorders leftNeighbor)
        {
            RightNeighbor = rightNeighbor;
            LeftNeighbor = leftNeighbor;
            MidPoint = (LeftNeighbor.Table.Right + RightNeighbor.Table.Left) / 2;
            //StartNode = new Node(MidPoint, Math.Max(RightNeighbor.TopBorderY, LeftNeighbor.TopBorderY));
            //EndNode = new Node(MidPoint, Math.Min(RightNeighbor.BottomBorderY, LeftNeighbor.BottomBorderY));
            GetStartNode();
            GetEndNode();
            Edge = new Edge(StartNode, EndNode);
            //RightNeighbor.Neighbors.Add(this);
            // LeftNeighbor.Neighbors.Add(this);
            table1 = RightNeighbor.Table.TableNumber;
            table2 = LeftNeighbor.Table.TableNumber;
        }

        public RightLeftNeighbor(int midLocation, int startPoint, int endPoint, TableEdgeBorders rightBorder, TableEdgeBorders leftBorder)
        {
            RightNeighbor = rightBorder; 
            LeftNeighbor = leftBorder;
            MidPoint = midLocation;
            StartNode = new Node(MidPoint, startPoint);
            EndNode = new Node(MidPoint, endPoint);
            Edge = new Edge(StartNode, EndNode);
            table1 = RightNeighbor.Table.TableNumber;
            table2 = LeftNeighbor.Table.TableNumber;
        }

        public TableEdgeBorders RightNeighbor { get; set; }
        public TableEdgeBorders LeftNeighbor { get; set; }
        public override Neighbor GetNeighborsOfNeighbors()
        {
            throw new NotImplementedException();
        }
        protected override void GetStartNode()
        {
            // For right and left neighbors, the start node would be determined by the top borders
            if (RightNeighbor.TopBorderY != -1 && LeftNeighbor.TopBorderY != -1)
            {
                StartNode = new Node(MidPoint, Math.Max(RightNeighbor.TopBorderY, LeftNeighbor.TopBorderY));
            }
            else if (RightNeighbor.TopBorderY != -1)
            {
                StartNode = new Node(MidPoint, RightNeighbor.TopBorderY);
            }
            else if (LeftNeighbor.TopBorderY != -1)
            {
                StartNode = new Node(MidPoint, LeftNeighbor.TopBorderY);
            }
            else
            {
                StartNode = new Node(MidPoint, Math.Max(RightNeighbor.Table.Top, LeftNeighbor.Table.Top));
            }
            Start = StartNode.Y;
        }

        protected override void GetEndNode()
        {
            // For right and left neighbors, the end node would be determined by the bottom borders
            if (RightNeighbor.BottomBorderY != -1 && LeftNeighbor.BottomBorderY != -1)
            {
                EndNode = new Node(MidPoint, Math.Min(RightNeighbor.BottomBorderY, LeftNeighbor.BottomBorderY));
            }
            else if (RightNeighbor.BottomBorderY != -1)
            {
                EndNode = new Node(MidPoint, RightNeighbor.BottomBorderY);
            }
            else if (LeftNeighbor.BottomBorderY != -1)
            {
                EndNode = new Node(MidPoint, LeftNeighbor.BottomBorderY);
            }
            else
            {
                EndNode = new Node(MidPoint, Math.Min(RightNeighbor.Table.Bottom, LeftNeighbor.Table.Bottom));
            }
            End = EndNode.Y;
        }
        public override string ToString()
        {
            return "R: " + RightNeighbor.Table.TableNumber + ", L: " + LeftNeighbor.Table.TableNumber;
        }
    }
}
