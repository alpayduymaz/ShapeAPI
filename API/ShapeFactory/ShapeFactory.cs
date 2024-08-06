using API.Models;

namespace API.ShapeFactory
{
    public static class ShapeFactory
    {
        private static Random random = new Random();
        public static Shape CreateShape(string shapeType)
        {
            var color = $"#{random.Next(0x1000000):X6}";
            var x = random.Next(0, 500);
            var y = random.Next(0, 500);
            int size = random.Next(10, 50);
            int speedX = random.Next(-5, 6); // Random speed in the range of -5 to 5
            int speedY = random.Next(-5, 6); // Random speed in the range of -5 to 5

            switch (shapeType.ToLower())
            {
                case "circle":
                    return new Circle
                    {
                        Id = Guid.NewGuid().ToString(),
                        Radius = random.Next(10, 50),
                        Color = color,
                        Width = size,
                        Height = size,
                        X = x,
                        Y = y,
                        SpeedX = speedX,
                        SpeedY = speedY
                    };
                case "square":
                    return new Square
                    {
                        Id = Guid.NewGuid().ToString(),
                        Color = color,
                        Width = size,
                        Height = size,
                        X = x,
                        Y = y,
                        SpeedX = speedX,
                        SpeedY = speedY
                    };
                case "rectangle":
                    return new Rectangle
                    {
                        Id = Guid.NewGuid().ToString(),
                        Width = random.Next(10, 50),
                        Height = random.Next(10, 50),
                        Color = color,
                        X = x,
                        Y = y,
                        SpeedX = speedX,
                        SpeedY = speedY
                    };
                default:
                    throw new ArgumentException("Invalid shape type");
            }
        }
    }
}
