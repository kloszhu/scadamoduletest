using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Step
{
    public class FlowEndStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("FlowEndStep: Status[1开始2运行3结束]:");
            return ExecutionResult.Next();
        }
    }
}