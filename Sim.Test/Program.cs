// See https://aka.ms/new-console-template for more information
//SimOperate sim=new SimOperate(new Basic)
using YSAI.Model.data;
using YSAI.Sim;

public class Program { 
    public static void Main(string[] args)
    {
        List<string> StringArray = new List<string>();
        for (int i = 0; i < 100000; i++)
        {
            StringArray.Add("abc.abcd.abcde"+i.ToString());
        }
        SimOperate sim=new SimOperate(new Basic { Duration=1000, SimType= SimulateType.Random });
        sim.On();
        var addressarray = StringArray.Select(x => new AddressDetails {
         AddressName= x,AddressDataType= YSAI.Model.@enum.DataType.Float
         , AddressType= YSAI.Model.@enum.AddressType.Reality
        }).ToList();
        var address = new Address { AddressArray= addressarray
        };
        sim.Subscribe(address);
        sim.OnDataEvent += Sim_OnDataEvent;
        Console.ReadKey();
    }

    private static void Sim_OnDataEvent(object? sender, EventDataResult e)
    {
        Console.WriteLine(e);
    }
}
