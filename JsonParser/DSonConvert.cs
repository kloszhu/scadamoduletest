using JsonParser.Model;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace JsonParser
{
    public class DSonConvert
    {
        public static DsonData Decode(string json)
        {
            DsonData result = new DsonData();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            if (data is JObject obj)
            {
                result.Status = JsonStatus.Ok;
                result.Data = Dson2Db(obj);
            }
            else if (data is JArray array)
            {
                string group = "X";
                result.Data = new List<Dson>();
                for (int i = 0; i < array.Count; i++)
                {
                    result.Data.AddRange(Dson2Db((JObject)array[i], null, group, 0,i));
                }
             
            }
            else
            {
                return new DsonData { Status = JsonStatus.Error, Msg = json };
            }
            return result;
        }

        public static string Encode(DsonData da)
        {
            if (da.Status == JsonStatus.Ok)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                foreach (var item in da.Data)
                {
                    if (string.IsNullOrEmpty(item.PKey) && item.IsNood == false)
                    {
                        result.Add(item.Key, item.Value);
                    }
                    else if (item.IsNood == true)
                    {
                        result.Add(item.Key, item.Value);
                    }
                }
                return null;
            }
            return null;
        }

        private static Dictionary<string, object> Db2Dson(List<Dson> list)
        {
            var maxlevel = list.Max(a => a.level);
            var result = list.Where(a => a.level == 1);
            
            return null;
        }


        private static List<Dson> Dson2Db(JObject obj, string pkey = null, string group = null, int level = 0,int groupindex=0)
        {
            List<Dson> newdata = new List<Dson>();
            level++;
            foreach (JProperty item in obj.Properties())
            {
                var data = new Dson { Id = Guid.NewGuid(), Key = item.Name, level = level, PKey = pkey, group = group, Type = item.Type.ToString() };
                if (item.Value is JArray array)
                {
                    data.IsNood = true;
                    data.PKey = item.Name;
                    data.level = level;
                    group = group + "X";
                    for (int i = 0; i < array.Count; i++)
                    {
                        groupindex = i;
                        newdata.AddRange(Dson2Db((JObject)array[i], item.Name, group + group, level, groupindex));
                    }
                   
                }
                else if (item.Value is JObject o)
                {
                    data.group = group + group;
                    data.level = level;
                    data.IsNood = true;
                    data.PKey = item.Name;
                    newdata.AddRange(Dson2Db(o, item.Name, item.Name, level));
                }
                else
                {
                    data.level = level;
                    data.IsNood = false;
                    data.Value = item.Value;
                }
                newdata.Add(data);
            }
            return newdata;
        }

        //public static string JsonDeConvert(DsonData dson)
        //{

        //}

        //private static string Db2Json(List<Dson> list)
        //{

        //}
    }
}