using GodSharp.Opc.Da;
using GodSharp.Opc.Da.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPC.Test
{
    public class OPCClient
    {
        public Dictionary<string, object> handler = new Dictionary<string, object>();
        public IOpcDaClient client;
        public IServerDiscovery discovery;
        public void Sample() {
            // initial with data info
            // The group `Name`, `ClientHandle` is unique and required, `UpdateRate` is required too.
            // The tag `ItemName`, `ClientHandle` is unique and required.
            var groups = new List<GroupData>
{
    new GroupData
    {
        Name = "default", UpdateRate = 100, ClientHandle = 010, IsSubscribed = true,
        Tags = new List<Tag>
        {
            new Tag("Test.Simulator.Booleans.B0001", 011),
            new Tag("Test.Simulator.Numbers.N0001", 012),
            new Tag("Test.Simulator.Characters.C0001", 013)
        }
    },
    new GroupData
    {
        Name = "group1", UpdateRate = 100, ClientHandle = 100,IsSubscribed = true,
        Tags = new List<Tag>
        {
            new Tag("Test.Simulator.Booleans.B0002", 101),
            new Tag("Test.Simulator.Numbers.N0002", 102),
            new Tag("Test.Simulator.Characters.C0002", 103)
        }
    },
    new GroupData
    {
        Name = "group2", UpdateRate = 100, ClientHandle = 200,IsSubscribed = false,
        Tags = new List<Tag>
        {
            new Tag("Test.Simulator.Booleans.B0003", 201),
            new Tag("Test.Simulator.Numbers.N0003", 202),
            new Tag("Test.Simulator.Characters.C0003", 203)
        }
    }
};

            var server = new ServerData
            {
                Host = "localhost",
                ProgId = "Knight.OPC.Server.Demo",
                //Id = Guid.Parse("B57C679B-665D-4BB0-9848-C5F2C4A6A280"),
                 //Name= "Knight.OPC.Server.Demo",
                // initial with data info,after connect will be add to client
                // if this is null,you should add group and tag manually
                Groups = groups
            };

             client = DaClientFactory.Instance.CreateOpcAutomationClient(new DaClientOptions(
                    server,
                    OnDataChangedHandler,
                    OnShoutdownHandler,
                    OnAsyncReadCompletedHandler,
                    OnAsyncWriteCompletedHandler));
            
        }

        public void Discovery() {
          discovery=DaClientFactory.Instance.CreateOpcAutomationServerDiscovery();
           
        }

        private void OnAsyncWriteCompletedHandler(AsyncWriteCompletedOutput obj)
        {
            handler.Add("WriteCompleted", obj.Data);
        }

        private void OnAsyncReadCompletedHandler(AsyncReadCompletedOutput obj)
        {
            handler.Add("ReadCompleted", obj.Data);
        }

        private void OnShoutdownHandler(Server arg1, string arg2)
        {
            handler.Add("Shoutdown", arg2);

        }

        private void OnDataChangedHandler(DataChangedOutput obj)
        {
            handler.Add("DataChanged", obj.Data);
        }
    }
}
