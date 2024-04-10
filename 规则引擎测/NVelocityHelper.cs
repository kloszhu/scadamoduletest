using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Context;
using NVelocity.Exception;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 规则引擎测
{
    /// <summary>
    /// 基于NVelocity的模板文件生成辅助类
    /// </summary>
    public class NVelocityHelper
    {
        private VelocityEngine velocity = null;
        private IContext context = null;

    
        /// 
        /// 构造函数
        ///templatDir模板文件夹路径  
        ///
        public NVelocityHelper()
        {
            Init();
        }

        /// 
        /// 初始化NVelocity模块
        /// <param name="templateDir">模版文件所在的物理文件夹</param>
        /// 模板文件夹路径
        public void Init()
        {
            //创建VelocityEngine实例对象
            velocity = new VelocityEngine();

            //使用设置初始化VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            //props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            //props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templateDir);
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");

            velocity.Init();
            //为模板变量赋值
            context = new VelocityContext();
        }

        /// 给模板变量赋值
        /// 模板变量
        ///模板变量值  
        public void Put(string key, object value)
        {
            if (context == null)
            {
                context = new VelocityContext();
                context.Put(key, value);
            }
            else
            {
                context.Put(key, value);
            }
        }
        public void Put(Dictionary<string, object> dic)
        {

            if (context == null)
            {
                context = new VelocityContext();
            }
            foreach (var item in dic)
            {
                context.Put(item.Key, item.Value);
            }


        }

        public IContext Context
        {
            set { context = value; }
            get { return context; }
        }

        public StringWriter GetResultString(string templatFileName)
        {
            //从文件中读取模板
            Template template = velocity.GetTemplate(templatFileName);
            //合并模板
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            return writer;
        }

        public StringBuilder GetResult(string logTag, string templatFileName) {
           
            StringWriter writer = new StringWriter();
            velocity.Evaluate(context, writer,"", templatFileName);
         
            return writer.GetStringBuilder();
        }

    }
}