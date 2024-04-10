// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
//MulticastTCPExample.Start();
Task.Run(() => { UdpSender.Main(); });
Task.Run(() => { UdpReceiver.Main(); });


Console.ReadLine();
