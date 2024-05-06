//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using NetMQ;
//namespace Zero.Server
//{
//    sealed class Server
//    {
//        public void Run()
//        {
//            Task.Run(() =>
//            {
//            using (NetMQSocket ctx =new NetMQSocket("tcp://127.0.0.1:5556"))
//            {
//                using (var server = ctx.CreateResponseSocket())
//                {
//                    server.Bind("tcp://127.0.0.1:5556");
//while (true)
//                    {
//                        string fromClientMessage = server.ReceiveString();
//                        Console.WriteLine(“From Client: { 0}
//                        running on ThreadId: { 1}“,
//fromClientMessage, Thread.CurrentThread.ManagedThreadId);
//            server.Send(“Hi Back”);
//        }

//    }
//}
//});

//}
//}
//}