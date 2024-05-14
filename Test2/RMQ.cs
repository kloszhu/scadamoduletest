using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2
{
    public static class RMQ
    {
        private static YSAI.RabbitMQ.RabbitMQOperate rmq;
        public static void INIT()
        {
            rmq = new YSAI.RabbitMQ.RabbitMQOperate(new YSAI.RabbitMQ.RabbitMQData.Basics
            {
                ExChangeName = "hello",
                HostName = "192.168.2.221",
                UserName = "ys",
                Password = "ys123456",
                Port = 5672,
                RT = YSAI.Model.@enum.ResponseType.Content
            });
            rmq.On();
        }

        public static void Send(string topic,string content)
        {
            rmq.Produce(topic, content);
        }

    }
}
