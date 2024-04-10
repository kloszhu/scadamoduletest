using Npoi.Mapper;
using Opc.Ua;
using System;
using System.Collections.Generic;
using System.Linq;
using YSAI.Model.data;
using YSAI.OPCUA.KepServer;
namespace YSAI.OPCUA.KepServer
{
    public class KepServerManager
    {
        private static KepServerManager ins;
        public static KepServerManager Instance
        {
            get
            {
                if (ins == null)
                {
                    ins = new KepServerManager();
                    return ins;
                }
                return ins;
            }
        }
        private YSAI.Model.@enum.DataType OpcConvert(string type)
        {
            switch (type.ToLower())
            {
                case "bool":
                case "boolean":
                    return YSAI.Model.@enum.DataType.Bool;
                case "string":
                    return YSAI.Model.@enum.DataType.String;
                case "date":
                    return YSAI.Model.@enum.DataType.Date;
                case "datetime":
                    return YSAI.Model.@enum.DataType.DateTime;
                case "time":
                    return YSAI.Model.@enum.DataType.Time;
                case "float":
                    return YSAI.Model.@enum.DataType.Float;
                case "byte":
                    return YSAI.Model.@enum.DataType.Byte;
                case "char":
                    return YSAI.Model.@enum.DataType.Char;
                case "decimal":
                    return YSAI.Model.@enum.DataType.Decimal;
                case "double":
                    return YSAI.Model.@enum.DataType.Double;
                case "int":
                    return YSAI.Model.@enum.DataType.Int;
                case "int16":
                    return YSAI.Model.@enum.DataType.Int16;
                case "int32":
                    return YSAI.Model.@enum.DataType.Int32;
                case "long":
                    return YSAI.Model.@enum.DataType.Long;
                case "uint":
                    return YSAI.Model.@enum.DataType.Uint;
                case "ulong":
                    return YSAI.Model.@enum.DataType.Ulong;
                case "short":
                    return YSAI.Model.@enum.DataType.Short;
                case "uint32":
                    return YSAI.Model.@enum.DataType.UInt32;
                case "int64":
                    return YSAI.Model.@enum.DataType.Int64;
                case "ushort":
                    return YSAI.Model.@enum.DataType.Ushort;
                default: return YSAI.Model.@enum.DataType.None;
            }
        }
        public NodeId ConvertD(string DataType)
        {
            switch (DataType)
            {
                case "String": return DataTypeIds.String;
                case "Number": return DataTypeIds.Number;
                case "Boolean": return DataTypeIds.Boolean;
                case "Float": return DataTypeIds.Float;
                default:
                    return DataTypeIds.String;
            }
            return DataTypeIds.String;
        }
        public List<AddressDetails> GetAll()
        {
            var mapper = new Mapper("third.xlsx");
            var data = mapper.Take<KepServerOutModels>().ToList();
            return data.Select(a => new AddressDetails
            {
                AddressDataType= OpcConvert(a.Value.DataType),
                 AddressType= Model.@enum.AddressType.Reality,
                 AddressName="ns=2;s="+a.Value.Address,
              
            }).ToList();
        }

    }
}
