using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.Model
{
    public enum JsonStatus
    {
        Ok,Error
    }

    public class DsonData {
        public JsonStatus Status { get; set; }
        public string Msg { get; set; }
        public List<Dson> Data { get; set; }
    }

    /// <summary>
    /// 数据库Json格式
    /// </summary>
    public class Dson
    {
        public Guid Id { get; set; }
        public string PKey { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
        public string  @Type { get; set; }
        public int level { get; set; }
        public bool IsNood { get; set; }
        public string group { get; set; }
        public int groupitemIndex { get; set; }
    }
}
