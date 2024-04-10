using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models.FlowInstance;
using Workflow.Step;
using WorkflowCore.Interface;

namespace Workflow.WF
{

    /// <summary>
    /// 广度优先图形遍历流程图 
    /// </summary>
    public class WorkflowDefine : IWorkflow<WFPublichVO>
    {
        public string Id => "first";

        public int Version => 1;

        public void Build(IWorkflowBuilder<WFPublichVO> builder)
        {
            builder
            //默认如果流程异常，3秒后终止    
            .UseDefaultErrorBehavior(WorkflowCore.Models.WorkflowErrorHandling.Retry, TimeSpan.FromSeconds(3))
            //状态=run,检查流程图是否为空，输出状态为运行时
            .StartWith<FlowEntryStep>()
                .Input(step => step.InstanceNode, data => data.InstanceNode)
                .Output((step, data) => { data.Status = step.status; data.TodoNodes = step.TodoNodes; })
            //当状态在运行时，就继续流转
            .While(a => a.Status == Models.Enums.FlowStatus.run)
            .Do(whi =>
                ///检查是否是整个流程结束，如果结束，跳出，否则继续走。
                    whi.StartWith<FlowWhileStartStep>()
                        .Input(step => step.TodoNodes, data => data.TodoNodes)
                        .Output((step, data) => { data.Temp = null; })
                   ///遍历所有当前处理节点
                   .If(iff=>iff.TodoNodes.Count==0).Do(ifdo=>ifdo.StartWith<FlowNextIsNullStep>())
                    .ForEach(fe => fe.TodoNodes).Do(
                            fe => fe.StartWith<FlowForeachStartStep>()
                            .Input((step, data, context) => { step.TodoNode = (WFInstanceVO)context.Item;step.Next = data.Temp; })
                            .Output((step, data) => { data.Temp = step.Next; })
                    )
                    .Then<FlowCheckIsFinish>()
                    .Input((step, data) => { step.Next = data.Temp;step.status = data.Status; step.TodoNodes = data.TodoNodes; })
                    .Output((step, data) => { data.Temp = step.Next;data.Status = step.status;data.TodoNodes = step.TodoNodes;  })
                )
            .Then<FlowEndStep>().EndWorkflow();
        }
    }

}
