using DotNetty.Transport.Channels;
using YSAI.Model.data;

namespace YSAI.TCPServer
{
    public class ServerHandler : SimpleChannelInboundHandler<string>
    {
        private EventHandler<EventDataResult> onDataEvent;

        public ServerHandler(EventHandler<EventDataResult> onDataEvent)
        {
            this.onDataEvent = onDataEvent;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, string msg)
        {
            onDataEvent.Invoke(ctx.Channel.Id.AsLongText(), new EventDataResult
            {
                Message = "已接收",
                RData = msg,
                Status = true,
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")
            });
        }
    }
}