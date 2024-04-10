using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Models.FlowInstance;

namespace Workflow.Step
{
    public class Todo
    {
        public void DealWith() {
        }

        public void DealWith(WFInstanceVO todoNode)
        {
            switch (todoNode.type)
            {
                case "":   break;
                default:
                    break;
            }
            Console.WriteLine("------------------------------------------------");
            Console.Write("id" + todoNode.id + "\t\n");
            Console.Write("name" + todoNode.name+"\t\n");
            Console.Write("ico" + todoNode.ico + "\t\n");
            Console.Write("type" + todoNode.type + "\t\n");
            Console.WriteLine("------------------------------------------------");
        }
    }
}
