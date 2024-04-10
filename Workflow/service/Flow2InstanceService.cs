using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models.FlowChart;
using Workflow.Models.FlowInstance;
using MapsterMapper;
using Mapster;

namespace Workflow.service
{
    public class Flow2InstanceService
    {
        public List<WFInstanceVO> Convert(FlowChatVO flow) 
        {
            var rootNodes = flow.nodeList.Where(a => !flow.lineList.Any(b => b.to == a.id));
            //找到头结点
            List<WFInstanceVO> nodeTree = rootNodes.Adapt<List<WFInstanceVO>>();
            //遍历
            foreach (var item in nodeTree)
            {
                DiGui(item, flow.nodeList, flow.lineList);
            }
            return nodeTree;
        }
        //.Adapt<List<WFInstanceVO>>()
        private void DiGui(WFInstanceVO Parent, List<FlowNodeVO> nodeList, List<FlowLineVO> lineList)
        {
            var line = lineList.Where(a => a.from == Parent.id).ToList();
            var child = nodeList.Where(a => line.Any(b => b.to == a.id)).Adapt<List<WFInstanceVO>>();
            if (child.Count>0)
            {
                Parent.children = child;
            }
            foreach (var item in child)
            {
                DiGui(item, nodeList, lineList);
            }
            //var child= nodeList.Where(a => lineList.Any(b=>b.from == Parent.id&&b.from==a.id)).Adapt<List<WFInstanceVO>>();
            //if (child.Count>0)
            //{
            //    Parent.children = child;
            //}
            //foreach (var item in child)
            //{
            //    //如果存在from是chield的节点
            //    if (nodeList.Exists(a => lineList.Any(b => b.from == item.id && b.from == a.id)))
            //    {
            //        DiGui(item, nodeList, lineList);
            //    }
            //}
        }
    }
}
