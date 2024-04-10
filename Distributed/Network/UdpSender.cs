using System.Net.Sockets;
using System.Net;
using System.Text;

public class UdpSender
{
    public static void Main()
    {
        // UDP接收方的IP地址和端口
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // 本地IP地址，根据实际情况修改
        int port = 11000; // UDP端口号，根据实际情况修改

        // 创建UdpClient对象
        UdpClient sender = new UdpClient();

        try
        {
            // 要发送的数据
            string message = "Hello, UDP!";
            byte[] bytesToSend = Encoding.ASCII.GetBytes(message);

            // 发送数据到UDP接收方
            sender.Send(bytesToSend, bytesToSend.Length,new IPEndPoint(ipAddress,port));

            Console.WriteLine("Sent: {0}", message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            // 关闭UdpClient
            sender.Close();
        }
    }
}