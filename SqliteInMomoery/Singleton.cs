using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteInMomoery
{
    public class Singleton<T> where T: class,new()
    {
        private static T ins;
        /// <summary>
        /// 
        /// </summary>
        public static T Instance {
            get {
                if (ins==null)
                {
                     ins=new T();
                }
                return ins;
            }
        }




    }
}
