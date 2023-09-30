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