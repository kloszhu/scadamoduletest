using Newtonsoft.Json;

namespace Workflow.Models.FlowChart
{
    public class FlowNodeVO
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("type")]
        public string @type { get; set; }
        [JsonProperty("left")]
        public string left { get; set; }
        [JsonProperty("top")]
        public string top { get; set; }
        [JsonProperty("ico")]
        public string ico { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("iconPosition")]
        public string iconPosition { get; set; }
    }
}