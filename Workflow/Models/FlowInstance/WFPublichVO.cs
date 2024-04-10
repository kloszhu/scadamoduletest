using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models.Enums;
using Workflow.Models.FlowChart;

namespace Workflow.Models.FlowInstance
{
   
    public class WFPublichVO
    {
        /// <summary>
        /// 流程图meta，用于记录运行时流程JSON
        /// </summary>
        public string Meta { get; set; }
        /// <summary>
        /// 流程图对象
        /// </summary>
        public FlowChatVO _flowChart { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public FlowStatus Status { get; set; }
        /// <summary>
        /// 运行时流程图
        /// </summary>
        public List<WFInstanceVO> InstanceNode { get; set; }
        /// <summary>
        /// 下一个节点需要处理的流程
        /// </summary>
        public List<WFInstanceVO> TodoNodes { get; set; }
        /// <summary>
        /// Temp
        /// </summary>
        public List<WFInstanceVO> Temp { get; set; }


    }
}
