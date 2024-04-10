using System.Net.Sockets;
using System.Net;
using System.Text;

public class UdpReceiver
{
    public static void Main()
    {
        // UDP接收的端口
        int port = 11000; // UDP端口号，根据实际情况修改

        // 创建UdpClient对象并绑定到指定端口
        UdpClient receiver = new UdpClient(port);

        try
        {
            // 阻塞，直到收到数据
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedBytes = receiver.Receive(ref remoteEndPoint);

            // 将接收到的字节转换为字符串
            string receivedData = Encoding.ASCII.GetString(receivedBytes);

            Console.WriteLine("Received: {0} from {1}", receivedData, remoteEndPoint.Address);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            // 关闭UdpClient
            receiver.Close();
        }
    }
}