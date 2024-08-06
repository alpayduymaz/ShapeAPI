using API;
using API.ShapeManager;
using API.WebSocketService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});
builder.Services.AddSingleton<ShapeManager>();
builder.Services.AddSingleton<WebSocketService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");

app.UseWebSockets();

var webSocketService = app.Services.GetRequiredService<WebSocketService>();

app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var clientId = Guid.NewGuid().ToString();
        webSocketService.Clients.TryAdd(webSocket, clientId);

        await HandleWebSocketConnection(webSocket, clientId, webSocketService);
    }
    else
    {
        await next(context);
    }
});

async Task HandleWebSocketConnection(WebSocket webSocket, string clientId, WebSocketService webSocketService)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result;
    var messageBuilder = new StringBuilder();

    while ((result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None)).MessageType != WebSocketMessageType.Close)
    {
        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        messageBuilder.Append(message);

        foreach (var client in webSocketService.Clients.Keys)
        {
            if (client.State == WebSocketState.Open)
            {
                var msg = $"Client {clientId}: {messageBuilder.ToString()}";
                var msgBuffer = Encoding.UTF8.GetBytes(msg);
                await client.SendAsync(new ArraySegment<byte>(msgBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        messageBuilder.Clear();
    }

    webSocketService.Clients.TryRemove(webSocket, out _);
    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
}

app.MapControllers();

var configuration = builder.Configuration;
var protocol = configuration["NetworkProtocol"];

if (protocol == "UDP")
{
    var udpServer = new UdpServer("127.0.0.1", 12345);
    Task.Run(() => udpServer.StartListening());
}
else if (protocol == "TCP")
{
    var tcpServer = new TcpServer("127.0.0.1", 54321);
    Task.Run(() => tcpServer.StartListening());
}

app.Run("http://localhost:5180");