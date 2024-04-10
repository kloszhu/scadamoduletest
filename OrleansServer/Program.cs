// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using OrleansGrain;
using System.IO;
using Orleans.Persistence.Redis;
Console.WriteLine("Hello, Server!");
var host = new SiloHostBuilder().UseLocalhostClustering()
   
    .AddSimpleMessageStreamProvider("SMSProvider")
           .AddMemoryGrainStorage("PubSubStore")
           .ConfigureApplicationParts(parts => { parts.AddApplicationPart(typeof(Hello).Assembly).WithReferences(); })
    .Build();
await host.StartAsync();
while (true)
{
  
    Console.ReadLine();
}