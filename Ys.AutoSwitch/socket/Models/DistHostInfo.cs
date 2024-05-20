using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ys.AutoSwitch.socket.Models
{
    /// <summary>
    /// 目标信息
    /// </summary>
    public class DistHostInfo
    {
        /// <summary>
        /// UID
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        public string Name { get;  set; }
        public int DeadBand { get;  set; }
        public bool IsEnable { get;  set; }
        public int PingTime { get;  set; }
        public bool Status { get; set; }
        public string Code { get;  set; }
        public bool IsMain { get;  set; }
    }
}
