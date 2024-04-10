using Workflow.Models.Enums;
using Workflow.Models.FlowInstance;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.WF
{
    public class FlowCheckIsFinish : StepBody
    {
        public List<WFInstanceVO> Next { get; set; }
        public List<WFInstanceVO> TodoNodes { get; set; }
        public FlowStatus status { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            ///检查流程是否为空
            ///
            if (Next==null||Next.Count == 0)
            {
                status = FlowStatus.end;
            }
            else
            {
                TodoNodes = null;
                TodoNodes = Next;
            }
            
            Console.WriteLine("FlowCheckIsFinish:检查节点是否配置完整,当前节点：status:"+ status);
            return ExecutionResult.Next();
        }
    }
}