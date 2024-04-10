// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(DateTime.Now);
StackExchangeRedisOperate.Init();
for (int i = 0; i < 100000; i++)
{
    Task.Run(() =>
    {
        StackExchangeRedisOperate.AddString(Random.Shared.NextDouble().ToString(), Random.Shared.NextDouble(), null);
    });
}
Console.WriteLine(DateTime.Now);
Console.ReadKey();