namespace API.Models
{
   public class Circle : Shape
   {
        public int Radius { get; set; }

        public Circle()
        {
            Type = "Circle";
        }
   }
}
