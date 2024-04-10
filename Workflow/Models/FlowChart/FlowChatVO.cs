using Newtonsoft.Json;

namespace Workflow.Models.FlowChart
{
    public class FlowChatVO
    {
        public string flowName { get; set; }
        [JsonProperty("nodeList", TypeNameHandling = TypeNameHandling.Arrays)]
        public List<FlowNodeVO> @nodeList { get; set; }
        [JsonProperty("lineList", TypeNameHandling = TypeNameHandling.Arrays)]
        public List<FlowLineVO> @lineList { get; set; }
    }

}
