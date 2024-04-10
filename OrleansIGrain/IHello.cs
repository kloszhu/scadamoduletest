using Orleans;
using System.Collections.Concurrent;
using YSAI.Model.data;

namespace OrleansIGrain
{
    public interface IHello : IGrainWithGuidKey
    {
        Task CreateStream(AddressValue address);
    }
}
