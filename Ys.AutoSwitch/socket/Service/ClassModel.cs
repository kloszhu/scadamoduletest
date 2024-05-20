using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.AutoSwitch
{
    public class ClassModel
    {
        public string Uid { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsMain { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int DeadBand { get; set; }
        public bool isEnable { get; set; }
        public int PingTime { get; set; }
        public string Code { get; set; }
    }

   

}
