﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TableNeighborManager
    {
        public Table Table { get; set; }
        public Section Section { get; set; }
        public List<Neighbor> Neighbors { get; set; } = new List<Neighbor>();
        public List<Table> RightNeighbors { get; set; } = new List<Table>();
        public List<Table> LeftNeighbors { get; set; } = new List<Table>();
        public List<Table> TopNeighbors { get; set; } = new List<Table>();
        public List<Table> BottomNeighbors { get; set; } = new List<Table>();
        public void AddBottomNeighborsNeighbors()
        {

            if (this.ClosestBottomNeighborBorders != null)
            {
                this.NeighborBorders.TryAdd(ClosestBottomNeighborBorders, ClosestBottomNeighborBorders.TopBorder);
                if (ClosestBottomNeighborBorders.RightNeighbors.Count > 0)
                {
                    if (ClosestBottomNeighborBorders.RightBorderX < this.RightBorderX)
                    {
                        this.BottomNeighbors.Add(ClosestBottomNeighborBorders.RightNeighbors[0]);
                        //this.NeighborBorders.Add(BottomNeighborBorders, BottomNeighborBorders.TopBorder);
                        this.NeighborBorders.TryAdd(ClosestBottomNeighborBorders.RightNeighborBorders, ClosestBottomNeighborBorders.RightNeighborBorders.TopBorder);
                    }
                }
                else if (ClosestBottomNeighborBorders.LeftNeighbors.Count > 0)
                {
                    if (ClosestBottomNeighborBorders.LeftBorderX > this.LeftBorderX)
                    {
                        this.BottomNeighbors.Add(ClosestBottomNeighborBorders.LeftNeighbors[0]);
                        //this.NeighborBorders.Add(BottomNeighborBorders, BottomNeighborBorders.TopBorder);
                        this.NeighborBorders.TryAdd(ClosestBottomNeighborBorders.LeftNeighborBorders, ClosestBottomNeighborBorders.LeftNeighborBorders.TopBorder);
                    }
                }

            }
        }
        public void AddLeftNeighborsNeighbors()
        {
            if (this.ClosestLeftNeighborBorders != null)
            {
                this.NeighborBorders.TryAdd(ClosestLeftNeighborBorders, ClosestLeftNeighborBorders.RightBorder);
                if (ClosestLeftNeighborBorders.TopNeighbors.Count > 0)
                {
                    if (ClosestLeftNeighborBorders.TopBorderY > this.TopBorderY)
                    {
                        this.LeftNeighbors.Add(ClosestLeftNeighborBorders.TopNeighbors[0]);
                        this.NeighborBorders.TryAdd(ClosestLeftNeighborBorders.TopNeighborBorders, ClosestLeftNeighborBorders.TopNeighborBorders.RightBorder);
                    }
                }
                else if (ClosestLeftNeighborBorders.BottomNeighbors.Count > 0)
                {
                    if (ClosestLeftNeighborBorders.BottomBorderY < this.BottomBorderY)
                    {
                        this.LeftNeighbors.Add(ClosestLeftNeighborBorders.BottomNeighbors[0]);
                        this.NeighborBorders.TryAdd(ClosestLeftNeighborBorders.BottomNeighborBorders, ClosestLeftNeighborBorders.BottomNeighborBorders.RightBorder);
                    }
                }
            }
        }

        public List<Edge> GetEdges()
        {
            List<Edge> result = new List<Edge>();
            foreach (Edge edge in this.NeighborBorders.Values)
            {
                if (edge != null)
                {
                    result.Add(edge);
                }

            }
            return result;
        }
        public TableEdgeBorders ClosestTopNeighborBorders { get; set; }
        public TableEdgeBorders ClosestRightNeighborBorders { get; set; }
        public TableEdgeBorders ClosestLeftNeighborBorders { get; set; }
        public TableEdgeBorders ClosestBottomNeighborBorders { get; set; }
        public Dictionary<TableEdgeBorders, Edge> NeighborBorders { get; set; } = new Dictionary<TableEdgeBorders, Edge>();
        public int TopBorderY { get; private set; }
        public int BottomBorderY { get; private set; }
        public int RightBorderX { get; private set; }
        public int LeftBorderX { get; private set; }
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
        public Edge BottomSide { get; set; }
        public Node TopLeft { get; set; }
        public Node TopRight { get; set; }
        public Node BottomRight { get; set; }
        public Node BottomLeft { get; set; }
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
            if (LeftNeighbors.Count > 0)
            {
                LeftBorderX = (this.Table.Left + LeftNeighbors[0].Right) / 2;
            }
            if (TopNeighbors.Count > 0)
            {
                TopBorderY = (this.Table.Top + TopNeighbors[0].Bottom) / 2;
            }
            if (BottomNeighbors.Count > 0)
            {
                BottomBorderY = (this.Table.Bottom + BottomNeighbors[0].Top) / 2;
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
            if (BottomBorderY > 0 && RightBorderX > 0)
            {
                BottomRightNode = new Node(RightBorderX, BottomBorderY);
            }
            if (BottomBorderY > 0 && LeftBorderX > 0)
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
            if (BottomRightNode == null && TopRightNode != null && BottomLeftNode == null)
            {
                BottomRightNode = new Node(TopRightNode.X, this.Table.Bottom + 5);
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
            return Table.TableNumber;
        }
    }
}
