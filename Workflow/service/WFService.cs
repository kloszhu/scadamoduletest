using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models.FlowChart;
using Workflow.Util;

namespace Workflow.service
{
    public class WFService
    {
        JsonUtil util;
        public WFService()
        {
             util = new JsonUtil();
        }

        public FlowChatVO Test1Config()
        {
            return JsonConvert.DeserializeObject<FlowChatVO>( util.ReadFile("test1"));
        }

        public FlowChatVO Test2Config() {
            return JsonConvert.DeserializeObject<FlowChatVO>(util.ReadFile("test2"),new JsonSerializerSettings {  });
        }

    }
}
