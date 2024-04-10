using LuaRuleManager.Mode;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaRuleManager
{
    public  class Scripts
    {
		private static List<EntityModel> data = new List<EntityModel>();
		static Scripts ins;
		public static Scripts Instance { get {
                if (ins==null)
                {
					ins = new Scripts();
                }
				return ins;
			} }

		public List<EntityModel> GetData() {
			return data;
		}

		public void AddData(EntityModel entity) {
			data.Add(entity);
		}

		public void ReplaceData(List<EntityModel> list) {
			data = null;
			data = list;
		}



		public void Excute(string ProgramSource) {
			using (var l = new Lua())
			{
				// create an environment, that is associated to the lua scripts
				dynamic g = l.CreateEnvironment<LuaGlobal>();

				// register new functions
				g.WriteR = new Action<string,double>(ins.WriteR);
				g.WriteN = new Action<string, double>(ins.WriteN);
				g.ReadR = new Func<string, double>(ins.ReadR);
				g.ReadN = new Func<string, double>(ins.ReadN);
				var chunk = l.CompileChunk(ProgramSource, "test.lua", new LuaCompileOptions() { DebugEngine = LuaStackTraceDebugger.Default }); // compile the script with debug informations, that is needed for a complete stack trace
				try
				{
					g.dochunk(chunk); // execute the chunk
				}
				catch (Exception e)
				{
					Console.WriteLine("Expception: {0}", e.Message);
					var d = LuaExceptionData.GetData(e); // get stack trace
					Console.WriteLine("StackTrace: {0}", d.FormatStackTrace(0, false));
				}
			}
		}

        private void WriteR(string item,double value)
        {
			var da = data.FirstOrDefault(a => a.Node == item);
            if (data!=null)
            {
				//根据指标ID找到相应的设备点位
				//写值
				//写历史库
				da.RandomValue = value;
            }

		}
		private void WriteN(string item, double value)
		{
			var da = data.FirstOrDefault(a => a.Node == item);
			if (data != null)
			{
				da.NewValue = value;
			}

		}
	
		private double ReadR(string item)
        {
			var da = data.FirstOrDefault(a => a.Node == item);
			return da == null?0: da.RandomValue;
        }
		private double ReadN(string item)
		{
			var da = data.FirstOrDefault(a => a.Node == item);
			return da == null ? 0 : da.RandomValue;
		}


	}
}
