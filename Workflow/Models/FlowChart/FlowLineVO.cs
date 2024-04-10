using Newtonsoft.Json;

namespace Workflow.Models.FlowChart
{
     //"from": "0yz9tkxate",
     // "to": "b6v61375x"
    public class FlowLineVO
    {
        [JsonProperty("from")]
        public string @from { get; set; }
        [JsonProperty("to")]
        public string @to { get; set; }
    }
}