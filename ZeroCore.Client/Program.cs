// See https://aka.ms/new-console-template for more information
using YSAI.NetMQ;
using YSAI.Unility;

ZeromqUtils utils = new ZeromqUtils();
utils.Init();
while (true)
{
    string input= Console.ReadLine();
    if (input.StartsWith("a"))
    {
        utils.Produce();
    }
}


Console.WriteLine("Hello, World!");
