// See https://aka.ms/new-console-template for more information
using Sharp7;

Console.WriteLine("Hello, World!");

var s7 =new S7Client();
var conn=s7.ConnectTo("192.168.2.27",1,0);
Console.WriteLine(conn);

Console.ReadKey();
