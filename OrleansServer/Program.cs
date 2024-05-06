// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using OrleansGrain;
using System.IO;
using Orleans.Persistence.Redis;
using Orleans.Configuration;
Console.WriteLine("Hello, Server!");

var host = new SiloHostBuilder()
    .UseConsulClustering(options => {
       
        options.KvRootFolder = "OrleansCluster";
        options.AclClientToken = "abc";
        options.Address = new Uri("http://127.0.0.1:8600");
    })
    .ConfigureEndpoints(1111,2222)
    .Configure<ClusterOptions>(option => {
        option.ClusterId = "hello";
        option.ServiceId= "hello";
    })
    .AddSimpleMessageStreamProvider("SMSProvider")
           .AddMemoryGrainStorage("PubSubStore")
           .ConfigureApplicationParts(parts => { parts.AddApplicationPart(typeof(Hello).Assembly).WithReferences(); })
    .Build();
await host.StartAsync();
while (true)
{
  
    Console.ReadLine();
}