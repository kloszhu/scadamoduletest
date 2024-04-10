using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Step
{
    public class FlowNextIsNullStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        { ///检查流程是否为空
          ///
            Console.WriteLine("FlowNextIsNullStep:检查下一个流程是否为空");
            return ExecutionResult.Next();
        }
    }
}