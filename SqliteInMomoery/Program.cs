using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteInMomoery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DapperManager.Instance.Connection();
            DapperManager.Instance.Insertable();
            Console.Read();
        }

    }
}
