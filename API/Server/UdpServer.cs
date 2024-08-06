
using System.Net.Sockets;
using System.Net;
using System.Text;

public class UdpServer
{
    private readonly UdpClient _udpClient;

    public UdpServer(string ip, int port)
    {
        _udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(ip), port));
    }

    public async Task StartListening()
    {
        while (true)
        {
            var result = await _udpClient.ReceiveAsync();
            Console.WriteLine($"Received data from {result.RemoteEndPoint}");

            var responseMessage = "Data received";
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
            await _udpClient.SendAsync(responseBytes, responseBytes.Length, result.RemoteEndPoint);
        }
    }
}