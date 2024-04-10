using Workflow.Models.FlowInstance;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Workflow.Step
{
    public class FlowForeachStartStep : StepBody
    {
        Todo todo;
        public FlowForeachStartStep()
        {
            todo = new Step.Todo();
        }
        public List<WFInstanceVO> Next { get; set; }
        public WFInstanceVO TodoNode { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            ///检查流程是否为空
            ///
            Console.WriteLine("FlowForeachStartStep:检查节点是否配置完整,当前节点："+this.TodoNode.type);
            todo.DealWith(this.TodoNode);
            if (this.TodoNode.children!=null)
            {
                if (Next == null)
                {
                    Next = this.TodoNode.children;
                }
                else
                {
                    Next.AddRange(this.TodoNode.children);
                }
            }
            return ExecutionResult.Next();
        }
    }
}