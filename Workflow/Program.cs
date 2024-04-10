// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Workflow.Models.FlowChart;
using Workflow.Models.FlowInstance;
using Workflow.service;
using Workflow.Util;
using Workflow.WF;
using WorkflowCore.Interface;
///测试树状图
//WFService service = new WFService();
//var jsondata=service.Test2Config();
static IServiceProvider ConfigureServices()
{
    //setup dependency injection
    IServiceCollection services = new ServiceCollection();
    services.AddLogging();
    services.AddWorkflow();
    //services.AddWorkflow(x => x.UseMongoDB(@"mongodb://localhost:27017", "workflow"));
    //services.AddTransient<GoodbyeWorld>();

    var serviceProvider = services.BuildServiceProvider();

    return serviceProvider;
}

var util = new JsonUtil();
var jsonstr=util.ReadFile("test1");
Flow2InstanceService instanceService = new Flow2InstanceService();
var dd = Newtonsoft.Json.JsonConvert.DeserializeObject<FlowChatVO>(jsonstr);
List<WFInstanceVO> result = instanceService.Convert(dd);
Console.WriteLine("测试json解析成功");
Console.WriteLine("-------------------------------------------------------------------------------------------------------");
IServiceProvider serviceProvider = ConfigureServices();

//start the workflow host
var host = serviceProvider.GetService<IWorkflowHost>();
host.RegisterWorkflow<WorkflowDefine,WFPublichVO>();
host.Start();
WFPublichVO vO = new WFPublichVO();
vO.InstanceNode = result;
host.StartWorkflow("first",vO);

Console.ReadLine();
host.Stop();
