
using System.Net.Sockets;
using System.Net;
using System.Text;

public class TcpServer
{
    private readonly TcpListener _tcpListener;

    public TcpServer(string ip, int port)
    {
        _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
    }

    public async Task StartListening()
    {
        _tcpListener.Start();
        Console.WriteLine("TCP Server is listening...");

        while (true)
        {
            var client = await _tcpListener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using (var networkStream = client.GetStream())
        {
            var buffer = new byte[1024];
            var bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received message: {message}");

            var responseMessage = "Data received";
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
