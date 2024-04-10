using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SqlSugar;
using System.Configuration;
using System.Diagnostics;

namespace SqliteInMomoery
{
    public class SqliteManager : Singleton<SqliteManager>
    {
        public static SqlSugarProvider MDB;
        public static SqlSugarProvider FDB;
        public static SqlSugarClient db;

        public void Connection()
        {
             db = new SqlSugarClient(new List<ConnectionConfig>()
            {
                        new ConnectionConfig(){ ConfigId="0", DbType=DbType.Sqlite,  ConnectionString=ConfigurationManager.ConnectionStrings["sqlitememory"].ConnectionString, IsAutoCloseConnection=false },
                        new ConnectionConfig(){ ConfigId="1", DbType=DbType.Sqlite,  ConnectionString=ConfigurationManager.ConnectionStrings["sqlitedb"].ConnectionString, IsAutoCloseConnection=true }

            });
            MDB = db.GetConnection("0");//memory db
            FDB = db.GetConnection("1");// file db

        }

        public void OnTrans(Action action) {
            db.BeginTran();
            action.Invoke();
            db.CommitTran();
        }

        public void InsertBluy() {
            var data = new List<(string, string, int, int)>();
          
          
            ComputeDuration("计算加载数据时间:", () => {

                for (int i = 0; i < 100000; i++)
                {
                    var p=(i.ToString(), i.ToString(), DateTime.Now.Hour, DateTime.Now.Millisecond);
                    var bb = new { n = p.Item1, v = p.Item2, h = p.Item3, s = p.Item4 };
                    MDB.Ado.ExecuteCommand("insert into a(n,v,h,s)value(@n,@v,@h,@s) ", bb);
                }
            });
            var insertb = data.Select(a => new { n = a.Item1, v = a.Item2, h = a.Item3, s = a.Item4 }).ToList();
            ComputeDuration("计算插入数据时间:", () => {

                MDB.Ado.ExecuteCommand("insert into a(n,v,h,s)value(@n,@v,@h,@s) ", insertb);
            });

          
        }


        public void ComputeDuration(string name,Action action) {
            Stopwatch swatch = new Stopwatch();
            swatch.Start();
            action.Invoke();
            swatch.Stop();
            Console.WriteLine($"执行时间:{swatch.ElapsedMilliseconds}毫秒");
        }

    }
}
