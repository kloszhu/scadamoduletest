using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class MulticastTCPExample
{
   public static void Start()
    {
        // 组播地址和端口
        IPAddress multicastAddress = IPAddress.Parse("192.0.0.1"); // 示例组播地址
        int port = 11000; // 示例端口

        // 创建UDP套接字
        UdpClient udpClient = new UdpClient(port);
        udpClient.EnableBroadcast = true; // 允许广播，对于组播通常不需要设置，但某些情况下可能需要

        // 加入组播组
        udpClient.JoinMulticastGroup(multicastAddress);

        // 发送数据
        string message = "Hello, multicast!";
        byte[] bytesToSend = Encoding.ASCII.GetBytes(message);
        //udpClient.Send(bytesToSend, bytesToSend.Length, multicastAddress, port);
        udpClient.Send(bytesToSend, bytesToSend.Length);
        // 接收数据
        Console.WriteLine("Waiting for multicast message...");
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
        string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
        Console.WriteLine("Received message: " + receivedMessage);

        // 离开组播组
        udpClient.DropMulticastGroup(multicastAddress);

        // 关闭UDP客户端
        udpClient.Close();
    }
}