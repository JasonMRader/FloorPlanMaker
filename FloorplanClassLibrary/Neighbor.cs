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
        public abstract Neighbor GetNeighborsOfNeighbors();
        public abstract void GetStartNode();
        public abstract void GetEndNode();


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
        }
        public TableEdgeBorders TopNeighbor { get; set; }
        public TableEdgeBorders BottomNeighbor { get; set; }

        public override Neighbor GetNeighborsOfNeighbors()
        {
            throw new NotImplementedException();
        }
        public override void GetStartNode()
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


        }
        public override void GetEndNode()
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
            
        }
    }
    public class RightLeftNeighbor : Neighbor
    {
        public RightLeftNeighbor(TableEdgeBorders rightNeighbor, TableEdgeBorders leftNeighbor)
        {
            RightNeighbor = rightNeighbor;
            LeftNeighbor = leftNeighbor;
            MidPoint = (LeftNeighbor.Table.Right + RightNeighbor.Table.Left) / 2;
            StartNode = new Node(MidPoint, Math.Max(RightNeighbor.TopBorderY, LeftNeighbor.TopBorderY));
            EndNode = new Node(MidPoint, Math.Min(RightNeighbor.BottomBorderY, LeftNeighbor.BottomBorderY));
            Edge = new Edge(StartNode, EndNode);
        }


        public TableEdgeBorders RightNeighbor { get; set; }
        public TableEdgeBorders LeftNeighbor { get; set; }
        public override Neighbor GetNeighborsOfNeighbors()
        {
            throw new NotImplementedException();
        }
        public override void GetStartNode()
        {
            throw new NotImplementedException();
        }
        public override void GetEndNode()
        {
            throw new NotImplementedException();
        }
    }
}
