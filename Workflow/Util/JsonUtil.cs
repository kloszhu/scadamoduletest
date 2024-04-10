using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Util
{
    public class JsonUtil
    {
        public string ReadFile(string filename) {
            string path = Path.Combine(AppContext.BaseDirectory,"json", filename + ".json");
            return File.ReadAllText(path);
        }
    }
}
