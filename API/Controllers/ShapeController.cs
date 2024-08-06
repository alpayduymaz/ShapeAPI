using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly ShapeManager.ShapeManager _shapeManager;

        public ShapeController(ShapeManager.ShapeManager shapeManager)
        {
            _shapeManager = shapeManager;
        }

        [HttpPost("createShape")]
        public IActionResult CreateShape(string type)
        {
            var shape = ShapeFactory.ShapeFactory.CreateShape(type);
            _shapeManager.AddShape(shape);
            return Ok(shape);
        }

        [HttpDelete("removeShape")]
        public IActionResult RemoveShape()
        {
            _shapeManager.RemoveShape();
            return Ok();
        }

        [HttpGet("shapes")]
        public IActionResult GetShapes()
        {
            return Ok(_shapeManager.GetShapes());
        }

        [HttpPost("update")]
        public IActionResult UpdateShapes()
        {
            _shapeManager.UpdateShapes();
            return Ok(_shapeManager.GetShapes());
        }
    }
}
