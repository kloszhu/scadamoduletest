using Workflow.Models.FlowInstance;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Step
{
    public class FlowWhileStartStep : StepBody
    {
        public List<WFInstanceVO> TodoNodes { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            ///检查流程是否为空
            ///
            Console.WriteLine("FlowWhileStartStep:检查节点是否配置完整"+ TodoNodes.Count);

            return ExecutionResult.Next();
        }
    }
}