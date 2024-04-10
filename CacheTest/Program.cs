using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;

namespace CacheTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PointClass> points = new List<PointClass>();
            Temp("初始化",() => {
                for (int i = 0; i < 50000; i++)
                {
                    points.Add(new PointClass { Id= "device.6025.YT6022_GP_SECONDWEIGHT_SET"+i.ToString(),
                     Address= "device.6025.YT6022_GP_SECONDWEIGHT_SET" + i.ToString(),
                      DataType="String", QL=0, Ts=DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffff"), VL=i
                    });
                }
            });

            Temp("缓存", () => {
                foreach (var item in points)
                {
                    CacheHelper.Instance.Set(item.Id, item);
                }
            });

            Console.ReadLine();


            //while (true)
            //{
            //    string index = Random.Shared.Next(0, 50000).ToString();
            //    Temp("取"+index, () => {

            //            CacheHelper.Instance.Get<PointClass>("device.6025.YT6022_GP_SECONDWEIGHT_SET" + index);
            //    });
            //}
            Console.WriteLine("Hello, World!");
        }


        static void Temp(string Name,Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();//sw.Start();
            action.Invoke();
            sw.Stop();
            Console.WriteLine(Name+":"+ sw.ElapsedMilliseconds.ToString());
        }
    }
}
