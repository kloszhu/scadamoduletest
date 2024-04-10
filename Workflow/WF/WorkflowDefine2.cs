//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Workflow.Models.FlowInstance;
//using Workflow.Step;
//using WorkflowCore.Interface;

//namespace Workflow.WF
//{

//    /// <summary>
//    /// 深度优先图形遍历流程图 暂未实现
//    /// </summary>
//    public class WorkflowDefine2 : IWorkflow<WFPublichVO>
//    {
//        public string Id => "second";

//        public int Version => 1;

//        public void Build(IWorkflowBuilder<WFPublichVO> builder)
//        {
//            builder.UseDefaultErrorBehavior(WorkflowCore.Models.WorkflowErrorHandling.Terminate, TimeSpan.FromSeconds(3))
//            //将状态赋值为运行状态,并给当前节点
//            .StartWith<FlowEntryStep>().Output(data => data.Status, step => step.status)
//            //
//            .While(a => a.Status == Models.Enums.FlowStatus.run).Do(
//                whi =>
//                    whi.StartWith<FlowWhileStartStep>()
//                    .ForEach(fe => fe.TodoNodes)
//                    .Do(
//                        fe => fe.StartWith<FlowWhileStartStep>().Input(step => step.NowInstance, (data, context) => context.Item)
                        
//                    )

//            ).Then<FlowEndStep>();
//        }
//    }
   
//}
