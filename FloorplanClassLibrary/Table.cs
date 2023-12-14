namespace FloorplanClassLibrary
{
    public class Table
    {
        public int ID {  get; set; }
        public string? TableNumber { get; set; } = "000";
        public int MaxCovers { get; set; } = 2;
        public float AverageCovers { get; set; } = 1;
        public DiningArea DiningArea { get; set; }
        public int DiningAreaId { get; set; }
        
        public int XCoordinate { get; set; } 
        public int YCoordinate { get; set; }
        private int buffer { get; set; } = 0;
        public Point TopLeft { get {  return new Point(XCoordinate - buffer, YCoordinate - buffer); } }
       
        public Point TopRight { get { return new Point(XCoordinate + Width + buffer, YCoordinate - buffer); } }
        public Point BottomRight { get { return new Point(XCoordinate + Width + buffer, YCoordinate + Height + buffer); } }
        public Point BottomLeft { get { return new Point(XCoordinate - buffer, YCoordinate + Height + buffer); } }
        public int Left { get { return TopLeft.X; } }
        public int Right { get { return TopRight.X; } }
        public int Top { get { return TopLeft.Y; } }
        public int Bottom { get { return BottomRight.Y; } }
        
        public TableShape Shape { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Point> GetCornerPoints()
        {
            return new List<Point> { TopLeft, TopRight, BottomRight, BottomLeft };
        }
        public enum TableShape
        {
            Circle,
            Square,
            Diamond
        }
        public override string ToString()
        {
            return TableNumber + " TL: " + TopLeft.ToString() + " TR: " + TopRight.ToString() +" BR: " + BottomRight.ToString();
        }

    }
}