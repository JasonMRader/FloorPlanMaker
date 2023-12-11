namespace FloorplanClassLibrary
{
    public abstract partial class AdjustmentBox
    {
        public class NeighborBox : AdjustmentBox
        {
            public NeighborBox(SectionBoarders sectionBoarders, SectionBoarders intrudingBoarders, Rectangle rectangle)
            {
                this.SectionBoarders = sectionBoarders;
                this.OtherSectionsBoarders = intrudingBoarders;
                CreateEdgesForRectangle(rectangle);
            }
        }
    }
}