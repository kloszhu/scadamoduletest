using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSAI.Model.data;

namespace YSAI.Sim
{
    public enum SimulateType
    {
        /// <summary>
        /// 值不变
        /// </summary>
        Stay,
        /// <summary>
        /// 随机值
        /// </summary>
        Random, 
        /// <summary>
        /// 顺序值
        /// </summary>
        Order
    }
    public class Basic
    {

        /// <summary>
        /// 间隔
        /// </summary>
        public int Duration { get;  set; }
        /// <summary>
        /// 模拟类型
        /// </summary>
        public SimulateType SimType { get;  set; }= SimulateType.Stay;

        //public Address Addresses { get; set; }

    }
}
