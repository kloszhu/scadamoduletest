using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DotNetty.Codecs.Protobuf;

namespace YS.Communication
{
    internal class NetClient
    {
        public async Task Start(string Host,int Port)
        {
   
            IEventLoopGroup bossEventLoop = new MultithreadEventLoopGroup(1);
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
                IChannel channel=await  boot.ConnectAsync(new IPEndPoint(IPAddress.Parse(Host), Port));
                Console.WriteLine("按任意键退出");
                Console.ReadLine();
                //list.ForEach(r =>
                //{
                //    r.CloseAsync();
                //});
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
