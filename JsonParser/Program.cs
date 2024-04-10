using JsonParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public static class Program
{
    static void Main()
    {
        var da = DSonConvert.Decode(File.ReadAllText("DataConfig.json"));
        var cc = DSonConvert.Encode(da);
        Console.WriteLine("解析器");
    }
}