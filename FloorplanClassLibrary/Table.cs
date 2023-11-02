namespace FloorplanClassLibrary
{
    public class Table
    {
        public int ID {  get; set; }
        public string? TableNumber { get; set; }
        public int MaxCovers { get; set; }
        public float AverageCovers { get; set; }
        public DiningArea DiningArea { get; set; }
        public int DiningAreaId { get; set; }
        
        public int XCoordinate { get; set; } 
        public int YCoordinate { get; set; }
        private int buffer { get; set; }
        public Point TopLeft { get {  return new Point(XCoordinate - buffer, YCoordinate -5); } }
        public Point TopRight { get { return new Point(XCoordinate + Width, YCoordinate); } }
        public Point BottomRight { get { return new Point(XCoordinate + Width, YCoordinate+Height); } }
        public Point BottomLeft { get { return new Point(XCoordinate, YCoordinate + Height); } }
        public TableShape Shape { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public enum TableShape
        {
            Circle,
            Square,
            Diamond
        }

    }
}