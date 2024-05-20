// See https://aka.ms/new-console-template for more information
using YSAI.Siemens;
using YSAI.Unility;
var s7 = new SiemensOperate(new SiemensData.Basics
{
    Ip = "192.168.2.27",
    Port = 102,
    CType = S7.Net.CpuType.S7200Smart,
    Rack = 0,
    Slot = 0,
    AllOut = true,
     HandleInterval = 2000,
     TaskHandleInterval = 2000,
});
var list = new YSAI.Model.data.Address
{
    AddressArray = new List<YSAI.Model.data.AddressDetails> {
 new YSAI.Model.data.AddressDetails{ AddressDataType= YSAI.Model.@enum.DataType.Bool, AddressName="M15.1" },
 new YSAI.Model.data.AddressDetails{ AddressDataType= YSAI.Model.@enum.DataType.Int, AddressName="DB1.DBW1810.0" },
 new YSAI.Model.data.AddressDetails{ AddressDataType= YSAI.Model.@enum.DataType.Bool, AddressName="M13.0" },
 }
};
s7.On();
s7.OnDataEvent += S7_OnDataEvent;
s7.Subscribe(list);

void S7_OnDataEvent(object? sender, YSAI.Model.data.EventDataResult e)
{
    Console.WriteLine(e.ToJson());
}
Console.ReadLine();
Console.WriteLine("Hello, World!");

