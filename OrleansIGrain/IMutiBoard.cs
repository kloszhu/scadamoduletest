using YSAI.Model.data;
using Orleans;
namespace OrleansIGrain
{
    public interface IMutiBoard:IGrainWithGuidKey
    {
        Task CreateStream(List<AddressValueSimplify> address);
    }
}