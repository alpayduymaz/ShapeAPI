using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
            private readonly WebSocketService.WebSocketService _webSocketService;

            public WebSocketController(WebSocketService.WebSocketService webSocketService)
            {
                _webSocketService = webSocketService;
            }

            [HttpGet("/clients")]
            public IActionResult GetClients() //clientList.js ten buraya get isteği geliyor
            {
                return Ok(_webSocketService.Clients.Values);
            }
    }
}
