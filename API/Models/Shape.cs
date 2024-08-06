namespace API.Models
{
    public abstract class Shape
    {
        public string Id { get; set; }
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int SpeedX { get; set; } // Added for movement
        public int SpeedY { get; set; }
    }

}
