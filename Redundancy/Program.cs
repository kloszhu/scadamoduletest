using Microsoft.Extensions.DependencyInjection;
using Redundancy.Extentions;
internal class Program
{
    private static void Main(string[] args)
    {
        var dependency = 
            Dependency
            .DependencyServices
            .Register()
            .AddLoggings()
            .BuilderProvider();
        var provider= dependency.GetService<>();
    }
}