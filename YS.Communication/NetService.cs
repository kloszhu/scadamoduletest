using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Channels;
using System.Net;
using DotNetty.Codecs.Protobuf;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace YS.Communication
{

    public class NetService
    {



   


        public ConcurrentDictionary<int,IChannel> channels;

        public async Task Start(int[] port) {
            channels = new ConcurrentDictionary<int,IChannel>();
            IEventLoopGroup bossEventLoop = new MultithreadEventLoopGroup(port.Length);
            IEventLoopGroup workerLoopGroup = new MultithreadEventLoopGroup();
            try
            {
                ServerBootstrap boot = new ServerBootstrap();
                boot.Group(bossEventLoop, workerLoopGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 100)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .Handler(new LoggingHandler("netty server"))
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel => {
                        IPEndPoint ip = (IPEndPoint)channel.LocalAddress;
                        Console.WriteLine(ip.Port);
                        channel.Pipeline.AddLast(new ProtobufEncoder());
                        channel.Pipeline.AddLast(new ProtobufDecoder(NetPackage.Parser));
                        channel.Pipeline.AddLast(new NettyServerHandler());
                    }));
                List<IChannel> list = new List<IChannel>();
                foreach (var item in port)
                {
                    IChannel boundChannel = await boot.BindAsync(item);
                    channels.TryAdd(item, boundChannel);
                    list.Add(boundChannel);
                }
                Console.WriteLine("按任意键退出");
                Console.ReadLine();
                list.ForEach(r =>
                {
                    r.CloseAsync();
                });
                //await boundChannel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await bossEventLoop.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
                await workerLoopGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
            await Task.CompletedTask;
        }
    }
   
}
