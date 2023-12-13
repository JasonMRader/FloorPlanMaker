using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.AxHost;

namespace FloorplanClassLibrary
{
    internal class SectionNeighborBoundry
    {
        public Section FirstSection { get; set; }
        public SectionBoarders FirstSectionBoarders { get; set; }
        public Section SecondSection { get; set; }
        public SectionBoarders SecondSectionBoarders { get; set; }
        public Edge FirstSectionEdge { get; set; }
        public Edge SecondSectionEdge { get; set; }
        bool isVerticleBoundry { get; set; }
        public SectionNeighborBoundry(Section section1, Section section2, Edge section1Edge, Edge section2Edge)
        {
            FirstSection = section1;
            SecondSection = section2;
            FirstSectionBoarders = section1.SectionBoarders;
            SecondSectionBoarders = section2.SectionBoarders;
            FirstSectionEdge = section1Edge;
            SecondSectionEdge = section2Edge;
            CreateLeftRightBoundry(section1Edge, section2Edge);
        }
        private void CreateLeftRightBoundry(Edge rightEdge, Edge leftEdge)
        {
            int middleX = (rightEdge.VerticleEdgeX() + leftEdge.VerticleEdgeX()) / 2;

            // Calculate the overlapping Y coordinates
            int overlapStartY = Math.Max(rightEdge.VerticleEdgeStartY(), leftEdge.VerticleEdgeStartY());
            int overlapEndY = Math.Min(rightEdge.VerticleEdgeEndY(), leftEdge.VerticleEdgeEndY());
            Node startNode = new Node(middleX, overlapStartY, FirstSection);
            Node endNode = new Node(middleX, overlapEndY, FirstSection);
            FirstSectionEdge = new Edge(startNode, endNode);

        }
        public static Edge CreateLeftRightUnblockedBoundry(Edge rightEdge, Edge leftEdge)
        {
            int middleX = (rightEdge.VerticleEdgeX() + leftEdge.VerticleEdgeX()) / 2;

            // Calculate the overlapping Y coordinates
            int overlapStartY = Math.Max(rightEdge.VerticleEdgeStartY(), leftEdge.VerticleEdgeStartY());
            int overlapEndY = Math.Min(rightEdge.VerticleEdgeEndY(), leftEdge.VerticleEdgeEndY());
            Node startNode = new Node(middleX, overlapStartY, rightEdge.Section);
            Node endNode = new Node(middleX, overlapEndY, rightEdge.Section);
            return new Edge(startNode, endNode);

        }
        public static Edge CreateTopBottomUnblockedBoundary(Edge topEdge, Edge bottomEdge)
        {
            int middleY = (topEdge.HorizontalEdgeY() + bottomEdge.HorizontalEdgeY()) / 2;

            // Calculate the overlapping X coordinates
            int overlapStartX = Math.Max(topEdge.HorizontalEdgeStartX(), bottomEdge.HorizontalEdgeStartX());
            int overlapEndX = Math.Min(topEdge.HorizontalEdgeEndX(), bottomEdge.HorizontalEdgeEndX());

            Node startNode = new Node(overlapStartX, middleY, topEdge.Section);
            Node endNode = new Node(overlapEndX, middleY, topEdge.Section);

            return new Edge(startNode, endNode);
        }



    }
}
