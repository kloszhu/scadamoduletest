using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Models.FlowInstance
{
    public class WFInstanceVO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string @type { get; set; }
        public string left { get; set; }
        public string top { get; set; }
        public string ico { get; set; }
        public string state { get; set; }
        public string iconPosition { get; set; }
        public List<WFInstanceVO> children { get; set; }
    }
    
}
