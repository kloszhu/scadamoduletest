using System.Collections.Concurrent;
using System.Threading;
using System.Transactions;
using YSAI.Core.@abstract;
using YSAI.Model.data;
using YSAI.Model.@interface;
namespace YSAI.Sim
{
    public class SimOperate : DaqAbstract<SimOperate, Basic>, IDaq
    {

        public int duration { get; set; }
        public SimulateType SimType { get; set; }

        private Dictionary<string, AddressValue> Data;
        private Task SimTask;

        int index = 0;
        private void OrderSim()
        {
            index++;
            foreach (var item in Data)
            {
                item.Value.Time = DateTime.Now.ToString();
                switch (item.Value.AddressDataType)
                {
                    case Model.@enum.DataType.Bool:
                        item.Value.Value = Convert.ToBoolean(index % 2);
                        break;
                    case Model.@enum.DataType.String:
                        item.Value.Value = index.ToString();
                        break;
                    case Model.@enum.DataType.Char:
                        item.Value.Value = GenerateRandomString(1);
                        break;
                    case Model.@enum.DataType.Decimal:
                        item.Value.Value = Convert.ToDecimal(index);
                        break;
                    case Model.@enum.DataType.Double:
                        item.Value.Value = (double)index;
                        break;
                    case Model.@enum.DataType.Float:
                    case Model.@enum.DataType.Single:
                        item.Value.Value = (float)(index);
                        break;
                    case Model.@enum.DataType.Byte:
                    case Model.@enum.DataType.Sbyte:
                        item.Value.Value = (byte)index % 256;
                        break;
                    case Model.@enum.DataType.Int:
                        item.Value.Value = index;
                        break;
                    case Model.@enum.DataType.Int32:
                        item.Value.Value = Convert.ToInt32(index);
                        break;
                    case Model.@enum.DataType.Uint:
                        item.Value.Value = Convert.ToUInt16(index);
                        break;
                    case Model.@enum.DataType.UInt32:
                        item.Value.Value = Convert.ToUInt32(index);
                        break;
                    case Model.@enum.DataType.Long:
                    case Model.@enum.DataType.Int64:
                    case Model.@enum.DataType.Ulong:
                    case Model.@enum.DataType.UInt64:
                        item.Value.Value = Convert.ToInt64(index);
                        break;
                    case Model.@enum.DataType.Short:
                    case Model.@enum.DataType.Int16:
                    case Model.@enum.DataType.Ushort:

                    case Model.@enum.DataType.UInt16:
                        item.Value.Value = (short)index;
                        break;
                    case Model.@enum.DataType.DateTime:

                    case Model.@enum.DataType.Date:
                        item.Value.Value = RandomDateTime();
                        break;
                    case Model.@enum.DataType.Time:
                        item.Value.Value = RandomTime();
                        break;
                    case Model.@enum.DataType.None:
                        item.Value.Value = null;
                        break;
                    default:
                        break;
                }
                item.Value.Value = index;
            }
        }

        private void RandomSim()
        {

            foreach (var item in Data)
            {
                item.Value.Time = DateTime.Now.ToString();
                switch (item.Value.AddressDataType)
                {
                    case Model.@enum.DataType.Bool:
                        item.Value.Value = Convert.ToBoolean(Random.Shared.Next(0, 2));
                        break;
                    case Model.@enum.DataType.String:
                        item.Value.Value = GenerateRandomString(Random.Shared.Next(1, 50));
                        break;
                    case Model.@enum.DataType.Char:
                        item.Value.Value = GenerateRandomString(1);
                        break;
                    case Model.@enum.DataType.Decimal:
                        item.Value.Value = Convert.ToDecimal(100000000 * Random.Shared.NextDouble());
                        break;
                    case Model.@enum.DataType.Double:
                        item.Value.Value = Random.Shared.NextDouble();
                        break;
                    case Model.@enum.DataType.Float:
                    case Model.@enum.DataType.Single:
                        item.Value.Value = (float)((Random.Shared.NextDouble() * 10.0) + 5.0);
                        break;
                    case Model.@enum.DataType.Byte:
                    case Model.@enum.DataType.Sbyte:
                        item.Value.Value = (byte)Random.Shared.Next(0, 256);
                        break;
                    case Model.@enum.DataType.Int:
                        item.Value.Value = Random.Shared.Next();
                        break;
                    case Model.@enum.DataType.Int32:
                        item.Value.Value = Convert.ToInt32(Random.Shared.Next());
                        break;
                    case Model.@enum.DataType.Uint:
                        item.Value.Value = Convert.ToUInt16(Random.Shared.Next());
                        break;
                    case Model.@enum.DataType.UInt32:
                        item.Value.Value = Convert.ToUInt32(Random.Shared.Next());
                        break;
                    case Model.@enum.DataType.Long:
                    case Model.@enum.DataType.Int64:
                    case Model.@enum.DataType.Ulong:

                    case Model.@enum.DataType.UInt64:
                        item.Value.Value = Convert.ToInt64(Random.Shared.Next());
                        break;
                    case Model.@enum.DataType.Short:
                    case Model.@enum.DataType.Int16:
                    case Model.@enum.DataType.Ushort:

                    case Model.@enum.DataType.UInt16:
                        item.Value.Value = (short)Random.Shared.Next(1, 65535);
                        break;
                    case Model.@enum.DataType.DateTime:

                    case Model.@enum.DataType.Date:
                        item.Value.Value = RandomDateTime();
                        break;
                    case Model.@enum.DataType.Time:
                        item.Value.Value = RandomTime();
                        break;
                    case Model.@enum.DataType.None:
                        item.Value.Value = null;
                        break;
                    default:
                        break;
                }

            }
        }

        public DateTime RandomDateTime()
        {
            Random random = new Random();
            DateTime minDate = new DateTime(2020, 1, 1); // 最小日期
            DateTime maxDate = new DateTime(2023, 12, 31); // 最大日期

            // 计算日期范围的差值（天数）
            int daysDiff = (maxDate - minDate).Days;

            // 生成一个随机天数，加上最小日期
            DateTime randomDate = minDate.AddDays(random.Next(daysDiff + 1));
            return randomDate;
        }

        public DateTime RandomTime()
        {
            Random random = new Random();

            // 获取当前日期
            DateTime today = DateTime.Today;

            // 生成随机的小时、分钟和秒
            int randomHour = random.Next(0, 24); // 0到23小时
            int randomMinute = random.Next(0, 60); // 0到59分钟
            int randomSecond = random.Next(0, 60); // 0到59秒

            // 构造随机时间
            DateTime randomTime = today.AddHours(randomHour).AddMinutes(randomMinute).AddSeconds(randomSecond);
            return randomTime;
        }

        private CancellationToken cancellationToken;

        public string GenerateRandomString(int length)
        {
            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                // 生成一个介于 'a' 和 'z' 之间的随机字符
                chars[i] = (char)(random.Next('a', 'z' + 1));
            }

            return new string(chars);
        }

        /// <summary>
        /// 无惨构造函数
        /// </summary>
        public SimOperate() : base() { }
        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="basics">基础数据</param>
        public SimOperate(Basic basics) : base(basics) { }

        protected override string CN => "模拟驱动";
        protected override string CD => "模拟驱动";

        /// <summary>
        /// 重写虚方法；
        /// 额外添加属性值
        /// </summary>
        protected override List<ParamModel.propertie> AP =>
        new List<ParamModel.propertie>()
        {
            new ParamModel.propertie
            {
                PropertyName = "ServiceName",
                Description = "命名空间",
                Show = false,
                Use = false,
                Default = this.GetType().FullName,
                DataCate = null
            }
        };


        public override OperateResult On()
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                cancellationToken = new CancellationToken();
                Data = new Dictionary<string, AddressValue>();
                duration = basics.Duration;
                SimType = basics.SimType;
                Task.Run(async () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(duration);
                        switch (SimType)
                        {
                            case SimulateType.Random:
                                RandomSim();
                                break;
                            case SimulateType.Order:
                                OrderSim();
                                break;
                        }
                        OnDataEventHandler(this, new EventDataResult { RData = Data });
                    }
                }, cancellationToken);

                return Break(SN, true);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }
        public override OperateResult Off(bool hardClose = false)
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                cancellationToken = new CancellationToken(true);
                return Break(SN, true);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }
        public override OperateResult Read(Address address)
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                ConcurrentDictionary<string, AddressValue> param = new ConcurrentDictionary<string, AddressValue>();
                foreach (var item in address.AddressArray)
                {
                    param.TryAdd(item.AddressName, Data[item.AddressName]);
                }
                return Break(SN, true, RData: param);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }
        public override OperateResult Write<V>(ConcurrentDictionary<string, V> values)
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                foreach (var item in values)
                {
                    Data[item.Key].Value = item.Value;
                }
                return Break(SN, true);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }
        public override OperateResult Subscribe(Address address)
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                foreach (AddressDetails item in address.AddressArray)
                {
                    Data.TryAdd(item.AddressName, new AddressValue
                    {
                        AddressName = item.AddressName,
                        AddressDataType = item.AddressDataType,
                        SN =string.IsNullOrEmpty( item.SN)?Guid.NewGuid().ToString():item.SN,
                        Quality = 1,
                        Time = DateTime.Now.ToString(),
                        Value = null,
                        OriginalValue = null
                    });

                }
                return Break(SN, true);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }
        public override OperateResult UnSubscribe(Address address)
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                foreach (AddressDetails item in address.AddressArray)
                {
                    Data.Remove(item.AddressName);
                }
                return base.UnSubscribe(address);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }
        public override OperateResult GetStatus()
        {
            string SN = Depart(System.Reflection.MethodBase.GetCurrentMethod().Name);
            try
            {
                return Break(SN, true);
            }
            catch (Exception ex)
            {
                return Break(SN, false, ex.Message, Exception: ex);
            }
        }



    }
}
