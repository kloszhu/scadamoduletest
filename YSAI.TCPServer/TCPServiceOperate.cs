//using DotNetty.Codecs;
//using DotNetty.Handlers.Logging;
//using DotNetty.Handlers.Tls;
//using DotNetty.Transport.Bootstrapping;
//using DotNetty.Transport.Channels;
//using DotNetty.Transport.Channels.Sockets;
//using System.Collections.Concurrent;
//using System.Security.Cryptography.X509Certificates;
//using YSAI.Model.data;
//using YSAI.Model.@interface;

//namespace YSAI.TCPServer
//{
//    public class TCPServiceOperate : IDaq, IOn, IOff
//    {
//        public event EventHandler<EventDataResult> OnDataEvent;
//        public event EventHandler<EventInfoResult> OnInfoEvent;
//        ServerBootstrap bootstrap;
//        IChannel bootstrapChannel;
//        MultithreadEventLoopGroup bossGroup;
//        MultithreadEventLoopGroup workerGroup;
//        public OperateResult CreateInstance<T>(T param)
//        {
//             bossGroup = new MultithreadEventLoopGroup(1);
//             workerGroup = new MultithreadEventLoopGroup();

//            var STRING_ENCODER = new StringEncoder();
//            var STRING_DECODER = new StringDecoder();
//            var SERVER_HANDLER = new ServerHandler(OnDataEvent);

//            try
//            {
//                ServerBootstrap bootstrap = new ServerBootstrap();
//                bootstrap
//                    .Group(bossGroup, workerGroup)
//                    .Channel<TcpServerSocketChannel>()
//                    .Option(ChannelOption.SoBacklog, 100)
//                    .Handler(new LoggingHandler(LogLevel.INFO))
//                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
//                    {
//                        IChannelPipeline pipeline = channel.Pipeline;
//                        pipeline.AddLast(new DelimiterBasedFrameDecoder(8192, Delimiters.LineDelimiter()));
//                        pipeline.AddLast(STRING_ENCODER, STRING_DECODER, SERVER_HANDLER);
//                    }));
//            }
//            finally
//            {
                
//            }
//            return new OperateResult
//            {
//                Message = "已经初始化完成",
//                Status = true,
//                RTime = 1,
//                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")
//            };
//        }



//        public Task<OperateResult> CreateInstanceAsync<T>(T param, CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult GetParam(bool getBasicsParam = false)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperateResult> GetParamAsync(bool getBasicsParam = false, CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult GetStatus()
//        {

//        }

//        public Task<OperateResult> GetStatusAsync(CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult Off(bool hardClose = false)
//        {
//            Task.WaitAll(bootstrapChannel.CloseAsync(),bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
//            return new OperateResult { 
//                Status = true 
//                , Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") };
//        }




//        public async Task<OperateResult> OffAsync(bool hardClose = false, CancellationTokenSource? token = null)
//        {
//            await bootstrapChannel.CloseAsync();
//            await bossGroup.ShutdownGracefullyAsync();
//            await workerGroup.ShutdownGracefullyAsync();
//            return new OperateResult
//            {
//                Status = true
//      ,
//                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")
//            };
//        }

//        //public OperateResult On()
//        //{
//        //    bootstrapChannel =  bootstrap.BindAsync(ServerSettings.Port);
//        //}

//        public Task<OperateResult> OnAsync(CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult Read(Address address)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperateResult> ReadAsync(Address address, CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult Subscribe(Address address)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperateResult> SubscribeAsync(Address address, CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult UnSubscribe(Address address)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperateResult> UnSubscribeAsync(Address address, CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }

//        public OperateResult Write<V>(ConcurrentDictionary<string, V> values)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<OperateResult> WriteAsync<V>(ConcurrentDictionary<string, V> values, CancellationTokenSource? token = null)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
