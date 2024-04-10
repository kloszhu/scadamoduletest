using GodSharp.Opc.Da;
using GodSharp.Opc.Da.Options;
using GodSharp.Opc.Ua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPC.Test
{
    public partial class OptNetApiTest : Form
    {
        IOpcDaClient client;
        public OptNetApiTest()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 发现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var groups = new List<GroupData>
{
    new GroupData
    {
        Name = "default", UpdateRate = 100, ClientHandle = 010, IsSubscribed = true,
        Tags = new List<Tag>
        {
            new Tag("a.a.a", 011),
            new Tag("a.a.b", 012),
            new Tag("a.a.c", 013)
        }
    },
    new GroupData
    {
        Name = "group10", UpdateRate = 100, ClientHandle = 100,IsSubscribed = true,
        Tags = new List<Tag>
        {
            new Tag("a.a.d", 101),
            new Tag("a.a.e", 102),
            new Tag("a.a.f", 103)
        }
    },
    new GroupData
    {
        Name = "group20", UpdateRate = 100, ClientHandle = 200,IsSubscribed = false,
        Tags = new List<Tag>
        {
            new Tag("a.a.g", 201),
            new Tag("a.a.h", 202),
            new Tag("a.b.h", 203)
        }
    }
};

            var server = new ServerData
            {
                //Host = "opcda://localhost",
                ProgId = "Knight.OPC.Server.Demo",
                // initial with data info,after connect will be add to client
                // if this is null,you should add group and tag manually
                Groups = groups,
                 
            };

             client = DaClientFactory.Instance.CreateOpcAutomationClient(new DaClientOptions(
                    server,
                    OnDataChangedHandler,
                    OnShoutdownHandler,
                    OnAsyncReadCompletedHandler,
                    OnAsyncWriteCompletedHandler));
            var connected= client.Connect();
            if (client.Connected)
            {
                MessageBox.Show("连接结果成功");
            }
            else
            {
                MessageBox.Show("连接结果失败");
            }
           

        }

        public static void OnDataChangedHandler(DataChangedOutput output)
        {
            Console.WriteLine($"{output.Data.ItemName}:{output.Data.Value},{output.Data.Quality} / {output.Data.Timestamp}");
        }

        public static void OnAsyncReadCompletedHandler(AsyncReadCompletedOutput output)
        {
            Console.WriteLine(
                $"Async Read {output.Data.Result.ItemName}:{output.Data.Result.Value},{output.Data.Result.Quality} / {output.Data.Result.Timestamp} / {output.Data.Code}");
        }

        public static void OnAsyncWriteCompletedHandler(AsyncWriteCompletedOutput output)
        {
            Console.WriteLine($"Async Write {output.Data.Result.ItemName}:{output.Data.Code}");
        }

        public static void OnShoutdownHandler(Server server, string reason)
        {
            Console.WriteLine(server.Host+reason);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpcUaServerDiscovery discovery = new OpcUaServerDiscovery();
            Console.WriteLine("discovery Discovery");
            string url = "opc:localhost";
            var servers = discovery.Discovery(url);
            foreach (var item in servers)
            {
                foreach (var durl in item.DiscoveryUrls)
                {
                    Console.WriteLine($"{durl}");
                    var endpoints = discovery.GetEndpoints(durl);
                    if (endpoints == null) continue;
                    foreach (var endpoint in endpoints)
                    {
                        Console.WriteLine($"\t- {endpoint.EndpointUrl}/ {endpoint.SecurityMode}/  {endpoint.SecurityPolicyUri}");
                    }
                }
            }
        }
    }
}
