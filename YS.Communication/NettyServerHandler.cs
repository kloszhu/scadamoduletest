using DotNetty.Buffers;
using DotNetty.Common.Concurrency;
using DotNetty.Transport.Channels;
using System.Text;

namespace YS.Communication
{
    public class NettyServerHandler : SimpleChannelInboundHandler<NetPackage>
    {
     
        public override bool IsSharable => true;

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, NetPackage msg)
        {
            Console.WriteLine(ctx.Channel.Id.AsLongText());
            Console.WriteLine(msg.Handle);
            msg.Heart=new BeatHeart() { Tempspan=DateTimeOffset.UtcNow.Ticks };
            // 序列化Protobuf消息
            //IByteBuffer buffer = Unpooled.Buffer(msg.CalculateSize());
            //msg.WriteTo(buffer.Writer);
            //buffer.Writer.Flush();
            ctx.WriteAndFlushAsync(msg);
        }
    }
}