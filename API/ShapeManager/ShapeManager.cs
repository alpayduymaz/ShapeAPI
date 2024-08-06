using API.Models;

namespace API.ShapeManager
{

    public class ShapeManager
    {
        private readonly Stack<Shape> _shapes = new Stack<Shape>();

        public void AddShape(Shape shape)
        {
            _shapes.Push(shape);
        }

        public void RemoveShape()
        {
            if (_shapes.Count > 0)
            {
                var shape = _shapes.Pop();    //stack yapısı
                Console.WriteLine($"Removed shape: {shape.Type}");
            }
        }

        public IEnumerable<Shape> GetShapes()
        {
            return _shapes.ToArray();
        }

        public void UpdateShapes()
        {
            foreach (var shape in _shapes)
            {
                shape.X += shape.SpeedX;
                shape.Y += shape.SpeedY;

                if (shape.X < 0 || shape.X > 800 - shape.Width)
                {
                    shape.SpeedX *= -1;
                }
                if (shape.Y < 0 || shape.Y > 600 - shape.Height)
                {
                    shape.SpeedY *= -1;
                }
            }
        }
    }

}
