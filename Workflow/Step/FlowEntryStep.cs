using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models.Enums;
using Workflow.Models.FlowInstance;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Step
{
    public class FlowEntryStep : StepBody
    {
        public FlowStatus status { get; set; }
        public List<WFInstanceVO> InstanceNode { get; set; }

        public List<WFInstanceVO> TodoNodes { get;  set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("FlowEntryStep Enter:");
            if (InstanceNode.Count == 0)
            {
                status = FlowStatus.end;
            }
            else
            {
                status = FlowStatus.run;
                TodoNodes = InstanceNode;
            }
            Console.WriteLine("FlowEntryStep Status[1开始2运行3结束]:"+status);
            return ExecutionResult.Next();
        }
    }
}
