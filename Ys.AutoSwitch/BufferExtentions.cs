using InfluxDB.Client.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSAI.Unility;

namespace Ys.AutoSwitch
{
    public static class BufferExtentions
    {
        public static byte[] ToSerializeArray(this byte[] bytes) {
            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                byte[] result = new byte[bytes.Length + 8];
                var lens=BitConverter.GetBytes((long)bytes.Length);
                Buffer.BlockCopy(lens, 0, result, 0, lens.Length);
                Buffer.BlockCopy(bytes, 0, result, 8, bytes.Length);
                return result;
            }
        }
        public static (long,byte[]) ToDeserializeArray(this byte[] bytes)
        {
            var len = new byte[8];
            Buffer.BlockCopy(bytes, 0, len, 0, 8);
            var lens= BitConverter.ToInt64(len, 0);
            var bts = new byte[lens];
            Buffer.BlockCopy(bytes, 8,bts,0, (int)lens);
            return (lens, bts);
        }
    }
}
