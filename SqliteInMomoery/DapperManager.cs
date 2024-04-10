using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using Dapper;
using System.Threading;

namespace SqliteInMomoery
{
    public class MM {
        public string n { get; set; }
        public string v { get; set; }
        public int h { get; set; }
        public int s { get; set; }
    }
    public class DapperManager:Singleton<DapperManager>
    {
        private static IDbConnection db;

        public void Connection() {
            db = new SQLiteConnection("DataSource=:memory:;Version=3;New=True;");
            db.Open();
        }
        public void Insertable() {
            string created = @"create table a(
n text,
v text,
h INTEGER,
s INTEGER
)";
            string sql = "insert into a(n,v,h,s)values(@n,@v,@h,@s)";
            string update = "update a set n=@n,v=@v,h=@h,s=@s where n=@n";
            string delete = "delete from  a ";
            var list = new List<MM>();
            db.Execute(created);
            ComputeDuration("加载list执行时间：",()=>{

                for (int i = 0; i < 50000; i++)
                {
                    var  p = new MM { n = i.ToString(), v = i.ToString(), h = i, s = DateTime.Now.Millisecond };
                    list.Add(p);

                }

            });

            
            ComputeDuration("执行Insert Sql时间：", () => {

          
                    db.Execute(sql, list);
                

            });
            IEnumerable<MM> pp=null;
            ComputeDuration("查询list执行时间：", () => {
                int vvv = 1000;
               pp = db.Query<MM>("select * from a where h<49000");

            });
            Console.WriteLine(pp.Count().ToString());
            Thread.Sleep(5000);

            ComputeDuration("更新list执行时间：", () => {
                int vvv = 1000;
                foreach (var item in list)
                {
                    //item.n = vvv.ToString();
                    item.v = vvv.ToString();
                    item.h = vvv;
                    item.s = vvv;
                }

            });
            ComputeDuration("执行删除 Sql时间：", () => {


                db.Execute(delete, list);


            });
            ComputeDuration("执行Update Sql时间：", () => {


                db.Execute(update, list);


            });

        }

        public void ComputeDuration(string name, Action action)
        {
            Stopwatch swatch = new Stopwatch();
            swatch.Start();
            action.Invoke();
            swatch.Stop();
            Console.WriteLine($"{name}:执行时间:{swatch.ElapsedMilliseconds}毫秒");
        }

    }
}
